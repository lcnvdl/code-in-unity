using System;
using System.Collections.Generic;
using CodeInUnity.Core.Interfaces;

namespace CodeInUnity.Core.Utils
{
  /// <summary>
  /// Provides a cache for storing and retrieving string values associated with a combination of a string key and an integer index.
  /// 
  /// Useful for efficiently managing and reusing values that are identified by a key-index pair, with support for lazy value generation.
  /// </summary>
  public class KeyIndexCache : IKeyIndexCache
  {
    private Dictionary<string, Dictionary<int, string>> dictionary = new Dictionary<string, Dictionary<int, string>>();

    public bool Exists(string key, int index)
    {
      if (this.dictionary.TryGetValue(key, out Dictionary<int, string> values))
      {
        return values.ContainsKey(index);
      }

      return false;
    }

    public string Get(string key, int index)
    {
      if (this.dictionary.TryGetValue(key, out Dictionary<int, string> values))
      {
        if (values.TryGetValue(index, out string value))
        {
          return value;
        }
      }

      return null;
    }

    public string GetOrSetAndReturnIfValueDoesNotExists(string key, int index, Func<string> generator)
    {
      return this.Get(key, index) ?? this.SetAndReturn(key, index, generator());
    }

    public string GetOrSetAndReturnIfValueDoesNotExists(string key, int index)
    {
      if (this.Exists(key, index))
      {
        return this.dictionary[key][index];
      }

      return this.SetAndReturn(key, index, $"{key}{index}");
    }

    public string GetOrSetAndReturnIfValueDoesNotExists(string key, int index, string separator)
    {
      if (this.Exists(key, index))
      {
        return this.dictionary[key][index];
      }

      return this.SetAndReturn(key, index, $"{key}{separator}{index}");
    }

    public void Set(string key, int index, string value)
    {
      Dictionary<int, string> values;

      if (!this.dictionary.TryGetValue(key, out values))
      {
        values = new Dictionary<int, string>();
      }

      values[index] = value;
    }

    public string SetAndReturn(string key, int index, string value)
    {
      this.Set(key, index, value);
      return value;
    }
  }
}
