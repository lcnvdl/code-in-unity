using System;
using UnityEngine;

[Serializable]
public class SafeFloat
{
    [SerializeField]
    private float offset;

    [SerializeField]
    private float value;

    public float Value
    {
        get
        {
            return value - offset;
        }
        set
        {
            offset = UnityEngine.Random.Range(-1000, +1000);
            this.value = value + offset;
        }
    }

    public SafeFloat()
    {
    }

    public SafeFloat(float value)
    {
        Value = value;
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

    public static bool operator ==(SafeFloat f1, float f2)
    {
        return f1.Value == f2;
    }

    public static bool operator !=(SafeFloat f1, float f2)
    {
        return f1.Value != f2;
    }

    public static bool operator ==(SafeFloat f1, SafeFloat f2)
    {
        return f1.Value == f2.Value;
    }

    public static bool operator !=(SafeFloat f1, SafeFloat f2)
    {
        return f1.Value != f2.Value;
    }

    public static implicit operator SafeFloat(float value)
    {
        return new SafeFloat(value);
    }

    public static implicit operator float(SafeFloat value)
    {
        return value.Value;
    }

    public static SafeFloat operator +(SafeFloat f1, SafeFloat f2)
    {
        return new SafeFloat(f1.Value + f2.Value);
    }

    public static SafeFloat operator -(SafeFloat f1, SafeFloat f2)
    {
        return new SafeFloat(f1.Value - f2.Value);
    }

    public static SafeFloat operator *(SafeFloat f1, SafeFloat f2)
    {
        return new SafeFloat(f1.Value * f2.Value);
    }

    public static SafeFloat operator /(SafeFloat f1, SafeFloat f2)
    {
        return new SafeFloat(f1.Value / f2.Value);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (obj is float)
        {
            return this == (float)obj;
        }
        else if (obj is SafeFloat)
        {
            return this == (SafeFloat)obj;
        }
        else
        {
            return base.Equals(obj);
        }
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }
}
