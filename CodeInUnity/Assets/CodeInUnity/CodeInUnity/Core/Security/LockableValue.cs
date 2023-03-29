using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInUnity.Core.Security
{
    public interface ILockableValue
    {
        bool Value { get; }

        void DecreaseLocks();

        void IncreaseLocks();

        void Clear();
    }

    [Serializable]
    public class LockableBoolValue : ILockableValue
    {
        public bool positiveIfLocked = true;

        public int locks = 0;

        public bool Value
        {
            get => this.locks > 0 ? positiveIfLocked : !positiveIfLocked;
        }

        public void Clear()
        {
            locks = 0;
        }

        public void IncreaseLocks()
        {
            locks++;
        }

        public void DecreaseLocks()
        {
            locks--;
        }

        public void DecreaseLocksLimit0()
        {
            if (locks > 0)
            {
                locks--;
            }
        }
    }

    [Serializable]
    public class LockableBoolNegativeValue : ILockableValue
    {
        public int locks = 0;

        public bool Value
        {
            get => this.locks <= 0;
        }

        public void Clear()
        {
            locks = 0;
        }

        public void IncreaseLocks()
        {
            locks++;
        }

        public void DecreaseLocks()
        {
            locks--;
        }

        public void DecreaseLocksLimit0()
        {
            if (locks > 0)
            {
                locks--;
            }
        }
    }

    [Serializable]
    public class LockableBoolPositiveValue : ILockableValue
    {
        public int locks = 0;

        public bool Value
        {
            get => this.locks > 0;
        }

        public void Clear()
        {
            locks = 0;
        }

        public void IncreaseLocks()
        {
            locks++;
        }

        public void DecreaseLocks()
        {
            locks--;
        }

        public void DecreaseLocksLimit0()
        {
            if (locks > 0)
            {
                locks--;
            }
        }
    }
}
