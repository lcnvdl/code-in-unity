using System;
using System.Collections.Generic;
using CodeInUnity.Core.Events;

namespace CodeInUnity.Core.System
{
    [Serializable]
    public class AsyncRoutine
    {
        public float __timeToExecute = 0f;

        public float timeGap = 1f;

        public int executions = -1;

        public string name;
    }

    [Serializable]
    public class AutoExecutorWithDelay
    {
        public StringUnityEvent onTrigger;

        public StringUnityEvent onFinish;

        public List<AsyncRoutine> routines = new List<AsyncRoutine>();

        public void Update(float dt)
        {
            var toRemove = this.routines.FindAll(m => m.executions == 0);

            foreach (var routine in toRemove)
            {
                this.onFinish.Invoke(routine.name);
                this.routines.Remove(routine);
            }

            foreach (var routine in routines)
            {
                routine.__timeToExecute -= dt;

                if (routine.__timeToExecute <= 0f)
                {
                    this.onTrigger.Invoke(routine.name);

                    if (routine.executions > 0)
                    {
                        routine.executions--;
                    }

                    routine.__timeToExecute += routine.timeGap;
                }
            }
        }
    }
}
