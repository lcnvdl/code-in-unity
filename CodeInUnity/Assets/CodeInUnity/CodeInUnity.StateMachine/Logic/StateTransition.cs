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

        public bool Test(SerializableDictionary<string, float> variables)
        {
            string parsedQuery = this.query;

            if (!string.IsNullOrEmpty(parsedQuery))
            {
                parsedQuery = parsedQuery.Replace("\n", string.Empty);

                foreach (var kv in variables)
                {
                    parsedQuery = parsedQuery.Replace(kv.Key, kv.Value.ToString());
                    parsedQuery = parsedQuery.Replace("true", "1");
                    parsedQuery = parsedQuery.Replace("false", "0");
                }

                var ands = new List<bool>();

                foreach (var qor in parsedQuery.Split("||"))
                {
                    foreach (var qand in qor.Split("&&"))
                    {
                        for (int i = 0; i < ops.Length; i++)
                        {
                            var op = ops[i];
                            var idx = qand.IndexOf(op);
                            if (idx != -1)
                            {
                                float left = float.Parse(qand.Remove(idx).Trim(), CultureInfo.InvariantCulture);
                                float right = float.Parse(qand.Substring(idx + qand.Length).Trim(), CultureInfo.InvariantCulture);

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

                                ands.Add(result);
                            }
                        }   //  Foreach "ops"
                    }   //  Foreach "ands"

                    bool value = ands.Count > 0 && ands.TrueForAll(m => m);

                    if (value)
                    {
                        return true;
                    }
                }   //  Foreach "ors"

                return false;
            }

            return false;
        }
    }
}
