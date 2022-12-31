using System;
using UnityEngine;

namespace CodeInUnity.Core.Security
{
    [Serializable]
    public class SafeInt
    {
        [SerializeField]
        private int offset;

        [SerializeField]
        private int value;

        [SerializeField]
        private int hashCode;

        public int Value
        {
            get
            {
                return value - offset;
            }
            set
            {
                offset = -1000 + ((int)DateTime.UtcNow.ToBinary() % 2000);
                this.value = value + offset;
            }
        }

        public SafeInt()
        {
        }

        public SafeInt(int value)
        {
            this.hashCode = DateTime.Now.Ticks.GetHashCode();
            this.Value = value;
        }

        public void Dispose()
        {
            offset = 0;
            value = 0;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool operator ==(SafeInt f1, int f2)
        {
            return f1.Value == f2;
        }
        public static bool operator !=(SafeInt f1, int f2)
        {
            return f1.Value != f2;
        }

        public static bool operator ==(SafeInt f1, SafeInt f2)
        {
            return f1.Value == f2.Value;
        }

        public static bool operator !=(SafeInt f1, SafeInt f2)
        {
            return f1.Value != f2.Value;
        }

        public static implicit operator SafeInt(int value)
        {
            return new SafeInt(value);
        }

        public static implicit operator int(SafeInt value)
        {
            return value.Value;
        }

        public static SafeInt operator +(SafeInt f1, SafeInt f2)
        {
            return new SafeInt(f1.Value + f2.Value);
        }

        public static SafeInt operator -(SafeInt f1, SafeInt f2)
        {
            return new SafeInt(f1.Value - f2.Value);
        }

        public static SafeInt operator *(SafeInt f1, SafeInt f2)
        {
            return new SafeInt(f1.Value * f2.Value);
        }

        public static SafeInt operator /(SafeInt f1, SafeInt f2)
        {
            return new SafeInt(f1.Value / f2.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is int)
            {
                return this == (int)obj;
            }
            else if (obj is SafeInt)
            {
                return this == (SafeInt)obj;
            }
            else
            {
                return GetHashCode() == obj.GetHashCode();
            }
        }

        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}