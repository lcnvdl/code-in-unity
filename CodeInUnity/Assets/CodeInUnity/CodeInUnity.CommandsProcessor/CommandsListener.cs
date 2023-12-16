using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CodeInUnity.Command;

public class CommandsListener : MonoBehaviour
{
    public bool manualRun = false;

    public bool runOnFixedUpdate = false;

    public bool isPaused = false;

    [SerializeReference]
    public List<BaseCommand> commands = new List<BaseCommand>();

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
        commands.FindAll(m => m.IsFinished).ForEach(m => m.Cancel(reason));
        commands.Clear();
    }

    private void SortAndClearCommands()
    {
        int len = this.commands.Count;

        if (len == 0)
        {
            return;
        }

        //  GC Optimization (0 bytes)
        this.commands.RemoveAll(m => m.IsInFinishedStatus);

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

    private BaseCommand PickAndEnableNextCommand()
    {
        if (commands.Count == 0)
        {
            return null;
        }

        var nextSyncCommand = commands.Find(m => !m.isAsync && !m.IsInFinishedStatus);

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
        if (commands.Count == 0)
        {
            return 0;
        }

        int max = 0;

        foreach (var command in commands)
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
        if (commands.Count == 0)
        {
            return;
        }

        foreach (var cmd in commands)
        {
            if (cmd.isAsync && !cmd.IsPaused)
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
