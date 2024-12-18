using System;

namespace CodeInUnity.Core.Interfaces
{
  public interface IKeyIndexCache
  {
    bool Exists(string key, int index);
    string Get(string key, int index);
    string GetOrSetAndReturnIfValueDoesNotExists(string key, int index);
    string GetOrSetAndReturnIfValueDoesNotExists(string key, int index, string separator);
    string GetOrSetAndReturnIfValueDoesNotExists(string key, int index, Func<string> generator);
    void Set(string key, int index, string value);
    string SetAndReturn(string key, int index, string value);
  }
}