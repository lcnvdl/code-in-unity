using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CodeInUnity.Command;

public class CommandsListener : MonoBehaviour
{
    public bool manualRun = false;

    public bool runOnFixedUpdate = false;

    public bool isPaused = false;

    public List<BaseCommand> commands = new List<BaseCommand>();

    public bool IsWorking => this.commands.Any(m => m.IsRunning);

    public bool HasCommands => this.commands.Count > 0;

    public bool IsEmpty => this.commands.Count == 0 || this.commands.All(m => m.IsInFinishedStatus);

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
        cmd.internalOrder = commands.Any() ? (commands.Max(m => m.internalOrder) + 1) : 0;
        commands.Add(cmd);
    }

    public void CancelCommands()
    {
        commands.Clear();
    }

    private void SortAndClearCommands()
    {
        if (this.commands.Count == 0)
        {
            return;
        }

        commands = commands
            .Where(m => !m.IsInFinishedStatus)
            .OrderByDescending(m => m.priority)
            .ThenBy(m => m.internalOrder)
            .ToList();
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
        var nextSyncCommand = commands.FirstOrDefault(m => !m.isAsync && !m.IsInFinishedStatus);

        if (nextSyncCommand != null)
        {
            if (nextSyncCommand.Status == CommandStatus.NotStarted)
            {
                commands.FindAll(m => m.IsRunning && !m.isAsync).ForEach(m => m.Pause());

                nextSyncCommand.Start(gameObject);
            }
            else if (nextSyncCommand.IsPaused)
            {
                nextSyncCommand.Unpause();
            }
        }

        return nextSyncCommand;
    }

    private void HandleAsyncCommands(float dt)
    {
        var asyncCommands = commands.Where(m => m.isAsync && !m.IsPaused);

        foreach (var cmd in asyncCommands)
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
