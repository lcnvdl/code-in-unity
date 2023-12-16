using System;
using System.Collections.Generic;
using System.Globalization;
using CodeInUnity.Core.Collections;

namespace CodeInUnity.StateMachine
{
  [Serializable]
  public class StateTransition
  {
    private static readonly string[] ops = new string[] { "==", "<", ">", "<=", ">=", "!=" };

    public string query;

    public string toState;

    public bool interruptState = true;

    public bool IsEmpty => string.IsNullOrEmpty(query);

    public StateTransition()
    {
    }

    public StateTransition(string toState, string query)
    {
      this.toState = toState;
      this.query = query;
    }

    public bool Test(SerializableDictionary<string, float> variables, List<string> triggers)
    {
      if (!string.IsNullOrEmpty(this.query))
      {
        string parsedQuery = this.query;

        foreach (string trigger in triggers)
        {
          parsedQuery = parsedQuery.Replace(trigger, "1");
        }

        foreach (var kv in variables)
        {
          parsedQuery = parsedQuery.Replace(kv.Key, kv.Value.ToString());
        }

        parsedQuery = parsedQuery.Replace("\n", string.Empty);
        parsedQuery = parsedQuery.Replace("true", "1");
        parsedQuery = parsedQuery.Replace("false", "0");

        if (!parsedQuery.Contains("||"))
        {
          var ands = new List<bool>();

          this.ParseAndsFromOr(parsedQuery, ands);

          bool value = ands.Count > 0 && ands.TrueForAll(m => m);

          if (value)
          {
            return true;
          }
        }
        else
        {
          foreach (var qor in parsedQuery.Split(new[] { "||" }, StringSplitOptions.None))
          {
            var ands = new List<bool>();

            this.ParseAndsFromOr(qor, ands);

            bool value = ands.Count > 0 && ands.TrueForAll(m => m);

            if (value)
            {
              return true;
            }
          }   //  Foreach "ors"
        }

        return false;
      }

      return false;
    }

    private void ParseAndsFromOr(string qor, List<bool> ands)
    {
      if (!qor.Contains("&&"))
      {
        this.ParseAnds(qor, ands);
        return;
      }

      foreach (var qand in qor.Split(new[] { "&&" }, StringSplitOptions.None))
      {
        this.ParseAnds(qand, ands);
      }
    }

    private void ParseAnds(string qand, List<bool> ands, bool cutOnFirstFalse = true)
    {
      int opsLength = ops.Length;

      for (int i = 0; i < opsLength; i++)
      {
        var op = ops[i];
        var idx = qand.IndexOf(op);
        if (idx != -1)
        {
          bool result;

          float left;

          if (!float.TryParse(qand.Remove(idx).Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out left))
          {
            result = false;
          }
          else
          {
            float right = float.Parse(qand.Substring(idx + op.Length).Trim(), CultureInfo.InvariantCulture);

            result = this.ApplyOperator(op, left, right);
          }

          ands.Add(result);

          //  Optimization
          if (!result && cutOnFirstFalse)
          {
            return;
          }
        }
      }
    }

    private bool ApplyOperator(string op, float left, float right)
    {
      bool result;

      switch (op)
      {
        case "==":
          result = left == right;
          break;
        case "!=":
          result = left != right;
          break;
        case "<":
          result = left < right;
          break;
        case ">":
          result = left > right;
          break;
        case "<=":
          result = left <= right;
          break;
        case ">=":
          result = left >= right;
          break;
        default:
          throw new InvalidOperationException($"Invalid condition operator: {op}");
      }

      return result;
    }
  }
}
