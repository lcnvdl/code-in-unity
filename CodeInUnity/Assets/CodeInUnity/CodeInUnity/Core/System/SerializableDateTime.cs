using System;
using UnityEngine;

namespace CodeInUnity.Core.System
{
  [Serializable]
  public class SerializableDateTime : ISerializationCallbackReceiver
  {
    [SerializeField]
    private string serializedDate;

    private DateTime date;

    public SerializableDateTime()
    {
    }

    public SerializableDateTime(DateTime dateTime)
    {
      date = dateTime;
    }

    public void OnBeforeSerialize()
    {
      serializedDate = date.ToBinary().ToString();
    }

    public void OnAfterDeserialize()
    {
      if (string.IsNullOrEmpty(serializedDate))
      {
        date = DateTime.MinValue;
      }
      else
      {
        long binary;

        if (long.TryParse(serializedDate, out binary))
        {
          date = DateTime.FromBinary(binary);
        }
        else
        {
          date = DateTime.MinValue;
        }
      }
    }

    public long ToBinary()
    {
      return date.ToBinary();
    }

    public override string ToString()
    {
      return date.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public override bool Equals(object obj)
    {
      if (obj is SerializableDateTime other)
      {
        return this.date == other.date;
      }

      if (obj is DateTime date)
      {
        return this.date == date;
      }

      return false;
    }

    public override int GetHashCode()
    {
      return date.GetHashCode();
    }

    //  Operators

    public static bool operator >(SerializableDateTime a, SerializableDateTime b) => a.date > b.date;
    public static bool operator <(SerializableDateTime a, SerializableDateTime b) => a.date < b.date;
    public static bool operator >=(SerializableDateTime a, SerializableDateTime b) => a.date >= b.date;
    public static bool operator <=(SerializableDateTime a, SerializableDateTime b) => a.date <= b.date;
    public static bool operator ==(SerializableDateTime a, SerializableDateTime b) => a?.date == b?.date;
    public static bool operator !=(SerializableDateTime a, SerializableDateTime b) => !(a == b);

    public static bool operator >(SerializableDateTime a, DateTime b) => a.date > b;
    public static bool operator <(SerializableDateTime a, DateTime b) => a.date < b;
    public static bool operator >=(SerializableDateTime a, DateTime b) => a.date >= b;
    public static bool operator <=(SerializableDateTime a, DateTime b) => a.date <= b;
    public static bool operator ==(SerializableDateTime a, DateTime b) => a?.date == b;
    public static bool operator !=(SerializableDateTime a, DateTime b) => !(a == b);

    public static implicit operator DateTime(SerializableDateTime sdt)
    {
      return sdt?.date ?? default;
    }

    public static implicit operator SerializableDateTime(DateTime dt)
    {
      return new SerializableDateTime(dt);
    }

    //  Defaults

    public static readonly SerializableDateTime MinValue = new SerializableDateTime(DateTime.MinValue);
    public static readonly SerializableDateTime MaxValue = new SerializableDateTime(DateTime.MaxValue);
    public static SerializableDateTime Now => new SerializableDateTime(DateTime.Now);
    public static SerializableDateTime UtcNow => new SerializableDateTime(DateTime.UtcNow);
  }
}