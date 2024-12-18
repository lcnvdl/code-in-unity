using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeInUnity.CommandsProcessor
{
  public class CommandsListener : MonoBehaviour
  {
    public bool manualRun = false;

    public bool runOnFixedUpdate = false;

    public bool isManuallyPaused = false;

    public CommandsListenerSafeModeSettings safeModeSettings = CommandsListenerSafeModeSettings.Default;

    [SerializeField]
    [HideInInspector]
    private int locksForPause = 0;

    [SerializeReference]
    public List<BaseCommand> commands = new List<BaseCommand>();

    private List<BaseCommand> withDependencies = new List<BaseCommand>();

    private List<string> deletedCommandsInternalId = new List<string>();

    public bool IsWorking
    {
      get
      {
        for (int i = 0; i < this.commands.Count; i++)
        {
          if (this.commands[i].IsRunning)
          {
            return true;
          }
        }

        return false;
      }
    }

    public bool HasCommands => this.commands.Count > 0;

    public bool IsPaused => this.locksForPause > 0 || this.isManuallyPaused;

    public bool IsEmpty
    {
      get
      {
        if (this.commands.Count == 0)
        {
          return true;
        }

        for (int i = 0; i < this.commands.Count; i++)
        {
          if (!this.commands[i].IsInFinishedStatus)
          {
            return false;
          }
        }

        return true;
      }
    }

    private void FixedUpdate()
    {
      if (!this.manualRun && this.runOnFixedUpdate)
      {
        this.NextStep(Time.fixedDeltaTime);
      }
    }

    private void Update()
    {
      if (!this.manualRun && !this.runOnFixedUpdate)
      {
        this.NextStep(Time.deltaTime);
      }
    }

    public virtual void NextStep(float dt)
    {
      if (this.IsPaused)
      {
        return;
      }

      SortAndClearCommands();
      HandleAsyncCommands(dt);
      HandleSyncCommands(dt);
    }

    public void AddCommand(BaseCommand cmd)
    {
      cmd.internalOrder = this.NextInternalOrder();
      this.commands.Add(cmd);
    }

    public bool AddUniqueCommand(BaseCommand cmd)
    {
      if (this.HasCommandByInternalId(cmd.internalId))
      {
        return false;
      }

      this.AddCommand(cmd);

      return true;
    }

    public T GetCommandByType<T>() where T : BaseCommand
    {
      return (T)this.commands.Find(m => m is T);
    }

    public BaseCommand GetCommandByInternalId(string id)
    {
      int count = this.commands.Count;

      for (int i = 0; i < count; i++)
      {
        var cmd = this.commands[i];
        if (cmd.internalId == id)
        {
          return cmd;
        }
      }

      return null;
    }

    public BaseCommand GetCommandById(Guid id)
    {
      int count = this.commands.Count;

      for (int i = 0; i < count; i++)
      {
        var cmd = this.commands[i];
        if (cmd.uuid == id)
        {
          return cmd;
        }
      }

      return null;
    }

    public void CancelCommandByInternalId(string id, string reason = null)
    {
      this.commands.Find(m => m.internalId == id)?.Cancel(reason);
    }

    public T GetCommand<T>(string internalId) where T : BaseCommand
    {
      return (T)this.commands.Find(m => m.internalId == internalId);
    }

    public void Pause()
    {
      this.locksForPause++;
    }

    public void Unpause()
    {
      this.locksForPause--;
    }

    public bool HasCommandByInternalId(string id)
    {
      int count = this.commands.Count;

      for (int i = 0; i < count; i++)
      {
        if (this.commands[i].internalId == id)
        {
          return true;
        }
      }

      return false;
    }

    public void CancelAllCommands()
    {
      commands.FindAll(m => !m.IsFinished).ForEach(m => m.Cancel());
      commands.Clear();
    }

    public void CancelAllCommands(string reason)
    {
      commands.FindAll(m => !m.IsFinished).ForEach(m => m.Cancel(reason));
      commands.Clear();
    }

    public CommandsListener SetSafeModeSettings(CommandsListenerSafeModeSettings settings)
    {
      this.safeModeSettings = settings;
      return this;
    }

    private void SortAndClearCommands()
    {
      int len = this.commands.Count;

      if (len == 0)
      {
        return;
      }

      this.withDependencies.Clear();
      this.deletedCommandsInternalId.Clear();

      for (int i = len - 1; i >= 0; i--)
      {
        var command = this.commands[i];

        if (command.IsInFinishedStatus)
        {
          if (this.withDependencies.Count > 0)
          {
            if (!string.IsNullOrEmpty(command.internalId))
            {
              this.deletedCommandsInternalId.Add(command.internalId);
            }
          }

          this.commands.RemoveAt(i);
        }
        else if (command.HasDependencies)
        {
          this.withDependencies.Add(command);
        }
      }

      this.UpdateDependenciesAfterDeleteCommands();

      if (this.commands.Count > 1)
      {
        this.commands.SortNA((a, b) => SortCommands(a, b));
      }
    }

    private void HandleSyncCommands(float dt)
    {
      var nextSyncCommand = this.PickAndEnableNextCommand();

      if (nextSyncCommand != null)
      {
        if (this.safeModeSettings.enabled)
        {
          try
          {
            nextSyncCommand.Step(dt, gameObject);
          }
          catch (Exception ex)
          {
            if (this.safeModeSettings.logErrorsInUnityConsole)
            {
              Debug.LogError($"CommandsListener: error occurred in command '{nextSyncCommand.GetType().Name}'.");
              Debug.LogException(ex);
            }

            nextSyncCommand.Cancel(ex.Message);
          }
        }
        else
        {
          nextSyncCommand.Step(dt, gameObject);
        }

        if (!nextSyncCommand.IsRunning)
        {
          PickAndEnableNextCommand();
        }
      }
    }

    private void HandleAsyncCommands(float dt)
    {
      if (this.commands.Count == 0)
      {
        return;
      }

      bool runInSafeMode = this.safeModeSettings.enabled;

      foreach (var cmd in commands)
      {
        if (cmd.isAsync && !cmd.IsPaused && !cmd.HasDependencies)
        {
          if (cmd.Status == CommandStatus.NotStarted)
          {
            if (cmd.asyncDelayBeforeStart > 0)
            {
              cmd.asyncDelayBeforeStart -= dt;
            }
            else
            {
              cmd.Start(gameObject);
            }
          }

          if (cmd.Status == CommandStatus.Running)
          {
            if (runInSafeMode)
            {
              try
              {
                cmd.Step(dt, gameObject);
              }
              catch (Exception ex)
              {
                if (this.safeModeSettings.logErrorsInUnityConsole)
                {
                  Debug.LogError($"CommandsListener: error occurred in command '{cmd.GetType().Name}'.");
                  Debug.LogException(ex);
                }

                cmd.Cancel(ex.Message);
              }
            }
            else
            {
              cmd.Step(dt, gameObject);
            }
          }
        }
      }
    }


    private void UpdateDependenciesAfterDeleteCommands()
    {
      int count = this.withDependencies.Count;

      if (this.deletedCommandsInternalId.Count > 0)
      {
        for (int i = 0; i < count; i++)
        {
          var cmd = this.withDependencies[i];

          foreach (var deletedId in this.deletedCommandsInternalId)
          {
            cmd.RemoveDependencyByInternalId(deletedId);
          }
        }
      }
      else if (count > 0)
      {
        for (int i = 0; i < count; i++)
        {
          var cmd = this.withDependencies[i];
          this.UpdateDependenciesList(cmd);
        }
      }
    }

    private void UpdateDependenciesList(BaseCommand cmd)
    {
      for (int i = cmd.DependenciesByInternalId.Count - 1; i >= 0; i--)
      {
        string m = cmd.DependenciesByInternalId[i];

        if (!this.HasCommandByInternalId(m))
        {
          cmd.DependenciesByInternalId.RemoveAt(i);
        }
      }
    }

    private BaseCommand PickAndEnableNextCommand()
    {
      if (this.commands.Count == 0)
      {
        return null;
      }

      var nextSyncCommand = this.commands.Find(m => !m.isAsync && !m.IsInFinishedStatus && !m.HasDependencies);

      if (nextSyncCommand != null)
      {
        if (nextSyncCommand.Status == CommandStatus.NotStarted)
        {
          this.PauseAllRunningSyncCommands();

          nextSyncCommand.Start(gameObject);
        }
        else if (nextSyncCommand.IsPaused)
        {
          nextSyncCommand.Unpause();
        }
      }

      return nextSyncCommand;
    }

    private void PauseAllRunningSyncCommands()
    {
      foreach (var cmd in commands)
      {
        if (cmd.IsRunning && !cmd.isAsync)
        {
          cmd.Pause();
        }
      }
    }

    private int NextInternalOrder()
    {
      if (this.commands.Count == 0)
      {
        return 0;
      }

      int max = 0;

      foreach (var command in this.commands)
      {
        if (command.internalOrder > max)
        {
          max = command.internalOrder;
        }
      }

      return max + 1;
    }

    private static int SortCommands(BaseCommand x, BaseCommand y)
    {
      if (x.priority != y.priority)
      {
        return y.priority - x.priority;
      }

      return x.internalOrder - y.internalOrder;
    }
  }
}