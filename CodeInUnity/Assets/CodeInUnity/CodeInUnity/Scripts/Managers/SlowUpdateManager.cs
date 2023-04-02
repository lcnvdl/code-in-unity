using System;
using UnityEngine;

namespace CodeInUnity.Scripts.Managers
{
    public class SlowUpdateManager : MonoBehaviour
    {
        private static bool applicationIsQuitting = false;

        private static SlowUpdateManager instance;

        public static SlowUpdateManager RawInstance => instance;

        public static SlowUpdateManager Instance
        {
            get
            {
                if (instance == null && !applicationIsQuitting)
                {
                    instance = FindAnyObjectByType<SlowUpdateManager>();

                    if (instance == null)
                    {
                        var go = new GameObject("SlowUpdateManager");
                        instance = go.AddComponent<SlowUpdateManager>();
                    }
                }

                return instance;
            }
        }

        public event Action slowQuarterSecUpdate;

        public event Action slowHalfSecUpdate;

        public event Action slowSecUpdate;

        private float slowQuarterSecUpdateDelay = 0f;

        private float slowHalfSecUpdateDelay = 0f;

        private float slowSecUpdateDelay = 0f;
        
        [RuntimeInitializeOnLoadMethod]
        private static void RunOnStart()
        {
            Application.quitting += () => applicationIsQuitting = true;
        }

        private void Start()
        {
            instance = this;
        }

        private void Update()
        {
            if (this.slowQuarterSecUpdateDelay <= 0)
            {
                this.slowQuarterSecUpdate?.Invoke();
                this.slowQuarterSecUpdateDelay += 0.25f;

                if (this.slowHalfSecUpdateDelay <= 0)
                {
                    this.slowHalfSecUpdate?.Invoke();
                    this.slowHalfSecUpdateDelay += 0.5f;

                    if (this.slowSecUpdateDelay <= 0)
                    {
                        this.slowSecUpdate?.Invoke();
                        this.slowSecUpdateDelay += 1f;
                    }
                    else
                    {
                        this.slowSecUpdateDelay -= 0.5f;
                    }
                }
                else
                {
                    this.slowHalfSecUpdateDelay -= 0.25f;
                }
            }
            else
            {
                this.slowQuarterSecUpdateDelay -= Time.deltaTime;
            }
        }
    }
}
