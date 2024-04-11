using CodeInUnity.Commands;
using CodeInUnity.CommandsProcessor;
using NUnit.Framework;
using UnityEngine;

public class CommandListenerTest
{
    [Test]
    public void CommandListener_NextStep_ShouldWorkFine()
    {
        var go = new GameObject();

        try
        {
            var listener = go.AddComponent<CommandsListener>();
            listener.manualRun = true;

            int counter = 0;

            var command = new MagicCommand()
            {
                action = (self, dt, obj) =>
                {
                    self.FinishCommand();
                    counter++;
                }
            };

            listener.AddCommand(command.Clone());
            listener.AddCommand(command.Clone());
            listener.AddCommand(command.Clone());

            Assert.IsFalse(listener.IsEmpty);
            Assert.IsFalse(listener.IsWorking);
            Assert.AreEqual(0, counter);

            //  Run command 1
            listener.NextStep(1f);
            Assert.IsTrue(listener.IsWorking);
            Assert.AreEqual(1, counter);

            //  Run command 2
            listener.NextStep(1f);
            Assert.IsTrue(listener.IsWorking);
            Assert.AreEqual(2, counter);

            listener.AddCommand(new MagicCommand()
            {
                priority = 100,
                action = (self, dt, obj) =>
                {
                    self.FinishCommand();
                    counter = 0;
                }
            });

            //  Run command 4
            listener.NextStep(1f);
            Assert.IsTrue(listener.IsWorking);
            Assert.AreEqual(0, counter);

            //  Run command 3
            listener.NextStep(1f);
            Assert.AreEqual(1, counter);
            Assert.IsFalse(listener.IsWorking);
            Assert.IsTrue(listener.IsEmpty);
        }
        finally
        {
            GameObject.DestroyImmediate(go);
        }
    }
}
