using CodeInUnity.Commands;
using CodeInUnity.CommandsProcessor;
using NUnit.Framework;
using UnityEngine;

public class DelayCommandTest
{
    [Test]
    public void DelayCommand_ShouldWorkFine()
    {
        var go = new GameObject();
        var listener = go.AddComponent<CommandsListener>();
        listener.manualRun = true;

        var command = new DelayCommand(2f);
        listener.AddCommand(command);

        listener.NextStep(0.5f);
        Assert.AreEqual(0.25f, command.Progress);
        Assert.IsTrue(listener.IsWorking);
        Assert.IsFalse(command.IsFinished);

        listener.NextStep(0.5f);
        Assert.AreEqual(0.5f, command.Progress);
        Assert.IsTrue(listener.IsWorking);
        Assert.IsFalse(command.IsFinished);

        listener.NextStep(0.5f);
        Assert.AreEqual(0.75f, command.Progress);
        Assert.IsTrue(listener.IsWorking);
        Assert.IsFalse(command.IsFinished);

        listener.NextStep(0.5f);
        Assert.AreEqual(1f, command.Progress);
        Assert.IsFalse(listener.IsWorking);
        Assert.IsTrue(command.IsFinished);
    }
}
