using NUnit.Framework;
using UnityEngine;
using CodeInUnity.Command;
using CodeInUnity.Commands;

public class CommandsFactoryTest
{
    [Test]
    public void CommandsFactory_Instantiate_RotateToCommand()
    {
        var data = new CommandData()
        {
            type = nameof(RotateToCommand),
            data = "{ \"isAsync\": false, \"totalTime\": 10.0, \"targetPoint\": {\"x\":1,\"y\":2,\"z\":3}, \"rotateSpeedMovement\": 0.1 }",
            priority = 1
        };

        var instance = new CommandsFactory().Instantiate(data) as RotateToCommand;
        Assert.IsNotNull(instance);

        Assert.IsFalse(instance.isAsync);
        Assert.AreEqual(1, instance.priority);
        Assert.AreEqual(10f, instance.TotalTime);
        Assert.AreEqual(new Vector3(1, 2, 3), instance.targetPoint);
        Assert.AreEqual(0.1f, instance.rotateSpeedMovement);
    }
}
