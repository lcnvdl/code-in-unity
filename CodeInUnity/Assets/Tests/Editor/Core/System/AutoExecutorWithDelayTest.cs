using System.Collections.Generic;
using CodeInUnity.Core.System;
using NUnit.Framework;

namespace Tests
{
  public class AutoExecutorWithDelayTest
  {
    [Test]
    public void Update_ShouldInvokeOnTrigger_WhenTimeToExecuteReachesZero()
    {
      var executor = new AutoExecutorWithDelay();
      var triggered = new List<string>();
      executor.onTrigger.AddListener(name => triggered.Add(name));
      executor.routines.Add(new AsyncRoutine { name = "routine", __timeToExecute = 1f, timeGap = 1f, executions = -1 });

      executor.Update(1f);

      CollectionAssert.AreEqual(new[] { "routine" }, triggered);
    }

    [Test]
    public void Update_ShouldNotInvokeOnTrigger_BeforeTimeToExecuteElapses()
    {
      var executor = new AutoExecutorWithDelay();
      var triggered = new List<string>();
      executor.onTrigger.AddListener(name => triggered.Add(name));
      executor.routines.Add(new AsyncRoutine { name = "routine", __timeToExecute = 1f, timeGap = 1f, executions = -1 });

      executor.Update(0.5f);

      CollectionAssert.IsEmpty(triggered);
    }

    [Test]
    public void Update_ShouldRemoveRoutineAndInvokeOnFinish_WhenExecutionsReachZero()
    {
      var executor = new AutoExecutorWithDelay();
      var finished = new List<string>();
      executor.onFinish.AddListener(name => finished.Add(name));
      executor.routines.Add(new AsyncRoutine { name = "routine", __timeToExecute = 1f, timeGap = 1f, executions = 1 });

      executor.Update(1f); // executions goes from 1 to 0
      executor.Update(0f); // routine with executions == 0 is now removed

      CollectionAssert.AreEqual(new[] { "routine" }, finished);
      Assert.IsEmpty(executor.routines);
    }

    [Test]
    public void Update_ShouldNotSkipRoutines_WhenMultipleAreRemovedInTheSameFrame()
    {
      var executor = new AutoExecutorWithDelay();
      var finished = new List<string>();
      executor.onFinish.AddListener(name => finished.Add(name));
      executor.routines.Add(new AsyncRoutine { name = "a", executions = 0 });
      executor.routines.Add(new AsyncRoutine { name = "b", executions = 0 });
      executor.routines.Add(new AsyncRoutine { name = "c", executions = 0 });

      executor.Update(0f);

      CollectionAssert.AreEquivalent(new[] { "a", "b", "c" }, finished);
      Assert.IsEmpty(executor.routines);
    }
  }
}
