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
            for (int i = this.routines.Count - 1; i >= 0; i--)
            {
                var routine = this.routines[i];

                if (routine.executions == 0)
                {
                    this.onFinish.Invoke(routine.name);
                    this.routines.RemoveAt(i);
                    continue;
                }

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
