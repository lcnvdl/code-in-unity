using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeInUnity.Core.Utils
{
  public class ConcatStringsCache
  {
    private static ConcatStringsCache globalInstance;

    public static ConcatStringsCache GlobalInstance => globalInstance ??= new ConcatStringsCache();

    public static ConcatStringsCache RawGlobalInstance => globalInstance;

    public Dictionary<string, Dictionary<string, string>> texts;

    private int capacityLimit;

    public ConcatStringsCache()
    {
      this.texts = new();
      this.capacityLimit = 0;
    }

    public ConcatStringsCache(int capacityLimit)
    {
      this.capacityLimit = capacityLimit;
      this.texts = new(capacityLimit);
    }

    public string Concat(string a, string b)
    {
      Dictionary<string, string> dict;

      if (!this.texts.TryGetValue(a, out dict))
      {
        this.LimitCapacity();

        dict = new Dictionary<string, string>();
        this.texts[a] = dict;
      }

      string concatenated;

      if (!dict.TryGetValue(b, out concatenated))
      {
        concatenated = string.Concat(a, b);
        dict[b] = concatenated;
      }

      return concatenated;
    }

    public string Concat(string a, string b, string c)
    {
      return this.Concat(this.Concat(a, b), c);
    }

    public string Concat(string a, string b, string c, string d)
    {
      return this.Concat(this.Concat(a, b, c), d);
    }

    public string Concat(string a, string b, string c, string d, string e)
    {
      return this.Concat(this.Concat(a, b, c, d), e);
    }

    public void Clear()
    {
      this.texts.Clear();
    }

    private void LimitCapacity()
    {
      try
      {
        if (this.capacityLimit > 0 && this.texts.Count > this.capacityLimit * 1.5)
        {
          this.TrimExcess(this.capacityLimit);
        }
      }
      catch (Exception ex)
      {
        Debug.LogException(ex);
      }
    }

    private void TrimExcess(int capacityLimit)
    {
      while (this.texts.Count >= capacityLimit)
      {
        using (var enumerator = this.texts.GetEnumerator())
        {
          if (!enumerator.MoveNext())
          {
            return;
          }

          this.texts.Remove(enumerator.Current.Key);
        }
      }
    }
  }
}
