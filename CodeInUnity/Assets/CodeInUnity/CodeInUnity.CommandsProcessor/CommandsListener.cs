using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeInUnity.CommandsProcessor
{
  public class CommandsListener : MonoBehaviour
  {
    public bool manualRun = false;

    public bool runOnFixedUpdate = false;

    public bool isPaused = false;

    [SerializeReference]
    public List<BaseCommand> commands = new List<BaseCommand>();

    private List<BaseCommand> withDependencies = new List<BaseCommand>();

    private List<string> deletedCommands = new List<string>();

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
      if (this.isPaused)
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
      if (this.HasCommand(cmd.internalId))
      {
        return false;
      }

      this.AddCommand(cmd);

      return true;
    }

    public T GetCommand<T>() where T : BaseCommand
    {
      return (T)this.commands.Find(m => m is T);
    }

    public BaseCommand GetCommand(string id)
    {
      return this.commands.Find(m => m.internalId == id);
    }

    public void CancelCommand(string id, string reason = null)
    {
      this.commands.Find(m => m.internalId == id)?.Cancel(reason);
    }

    public T GetCommand<T>(string id) where T : BaseCommand
    {
      return (T)this.commands.Find(m => m.internalId == id);
    }

    public bool HasCommand(string id)
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

    public void CancelCommands(string reason)
    {
      commands.FindAll(m => !m.IsFinished).ForEach(m => m.Cancel(reason));
      commands.Clear();
    }

    private void SortAndClearCommands()
    {
      int len = this.commands.Count;

      if (len == 0)
      {
        return;
      }

      this.withDependencies.Clear();
      this.deletedCommands.Clear();

      for (int i = len - 1; i >= 0; i--)
      {
        var command = this.commands[i];

        if (command.IsInFinishedStatus)
        {
          if (this.withDependencies.Count > 0)
          {
            this.deletedCommands.Add(command.uuid);
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
        nextSyncCommand.Step(dt, gameObject);

        if (!nextSyncCommand.IsRunning)
        {
          PickAndEnableNextCommand();
        }
      }
    }

    private void UpdateDependenciesAfterDeleteCommands()
    {
      int count = this.withDependencies.Count;

      if (this.deletedCommands.Count > 0)
      {
        for (int i = 0; i < count; i++)
        {
          var cmd = this.withDependencies[i];

          foreach (var deletedId in this.deletedCommands)
          {
            cmd.RemoveDependency(deletedId);
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
      cmd.Dependencies.RemoveAll(m => !this.HasCommand(m));
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

    private void HandleAsyncCommands(float dt)
    {
      if (this.commands.Count == 0)
      {
        return;
      }

      foreach (var cmd in commands)
      {
        if (cmd.isAsync && !cmd.IsPaused && !cmd.HasDependencies)
        {
          if (cmd.Status == CommandStatus.NotStarted)
          {
            cmd.Start(gameObject);
          }

          if (cmd.Status == CommandStatus.Running)
          {
            cmd.Step(dt, gameObject);
          }
        }
      }
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