using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace CodeInUnity.Testing.Helpers.Testing
{
    public static class AssertExt
    {
        public static void ShouldFail<T>(Action action) where T : Exception
        {
            try
            {
                action();
            }
            catch (T)
            {
                return;
            }

            Assert.Fail("Should fail");
        }

        public static void ShouldFail(Action action, Func<Exception, bool> validate)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (validate(ex))
                    return;

                throw;
            }

            Assert.Fail("Should fail");
        }

        public static void CountFields(Type type, int quantity)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            Assert.AreEqual(quantity, fields.Length, type.Name + " has " + fields.Length + " fields instead of " + quantity);
        }

        public static void EqualLines(string a, string b, bool compareLength = true)
        {
            var sa = a.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');
            var sb = b.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');

            if (compareLength)
            {
                Assert.AreEqual(sa.Length, sb.Length);
            }

            for (int i = 0; i < sa.Length; i++)
            {
                if (sa[i].Trim().Equals("$$ignore"))
                {
                    continue;
                }

                Assert.AreEqual(sa[i], sb[i]);
            }
        }

        public static void AllDifferentValues(object a, object b)
        {
            Test(a, b, (va, vb, name) => AssertEquality(va, vb, name, false));
        }
        
        public static void AreDeepEquals(object a, object b)
        {
            Assert.AreEqual(a == null ? null : UnityEngine.JsonUtility.ToJson(a), b == null ? null : UnityEngine.JsonUtility.ToJson(b));
        }

        public static void AreNotDeepEquals(object a, object b)
        {
            Assert.AreNotEqual(a == null ? null : UnityEngine.JsonUtility.ToJson(a), b == null ? null : UnityEngine.JsonUtility.ToJson(b));
        }

        public static void AnyDifferentValues(object a, object b)
        {
            bool success = false;

            Test(a, b, (va, vb, name) =>
            {
                if (success)
                    return;

                if (!Compare(va, vb, name))
                {
                    success = true;
                }
            });
        }

        public static void SameValues(object a, object b)
        {
            Test(a, b, (va, vb, name) => AssertEquality(va, vb, name, true));
        }

        private static void Test(object a, object b, Action<object, object, string> action)
        {
            var ta = a.GetType();
            var tb = b.GetType();
            var flags = BindingFlags.Instance | BindingFlags.Public;

            foreach (var fa in ta.GetFields(flags))
            {
                var fb = tb.GetField(fa.Name, flags);
                if (fb == null)
                {
                    continue;
                }

                action(fa.GetValue(a), fb.GetValue(b), fa.Name);
            }

            foreach (var fa in ta.GetProperties(flags))
            {
                var fb = tb.GetProperty(fa.Name, flags);
                if (fb == null || !fa.CanRead || !fb.CanRead)
                {
                    continue;
                }

                action(fa.GetValue(a), fb.GetValue(b), fa.Name);
            }
        }

        //public static void TestSerialization<T>() where T : UG_ISerializable, new()
        //{
        //    var a = new T();
        //    var b = new T();
        //    var c = new T();

        //    SameValues(a, b);
        //    SameValues(a, c);

        //    UGM.Tests.Helpers.Randomizer.Randomize(a);

        //    AnyDifferentValues(a, b);
        //    AnyDifferentValues(a, c);

        //    var node = a.Serialize();
        //    b.Deserialize(node);

        //    AssertExt.SameValues(a, b);
        //    AssertExt.AnyDifferentValues(a, c);

        //    UGM.Tests.Helpers.Randomizer.Randomize(a);
        //    UGM.Tests.Helpers.Randomizer.Randomize(b);
        //    AssertExt.AnyDifferentValues(a, b);

        //    node = b.Serialize();
        //    a.Deserialize(node);

        //    AssertExt.SameValues(a, b);
        //}

        //public static void TestClonable<T>() where T : class, IClonable<T>, new()
        //{
        //    TestClonable<T>(new T());
        //}

        //public static void TestClonable<T>(T instance) where T : class, IClonable<T>
        //{
        //    IClonable<T> a = instance;
        //    UGM.Tests.Helpers.Randomizer.Randomize(a);

        //    var b = a.Clone();
        //    Assert.IsNotNull(b);
        //    AssertExt.SameValues(a, b);

        //    UGM.Tests.Helpers.Randomizer.Randomize(b);
        //    AssertExt.AnyDifferentValues(a, b);
        //}

        private static bool Compare(object a, object b, string name)
        {
            if (a is float && b is float)
            {
                a = ((float)a).ToString();
                b = ((float)b).ToString();
            }
            else if (a is IEnumerable && b is IEnumerable)
            {
                var la = (a as IEnumerable).Cast<object>().ToList();
                var lb = (b as IEnumerable).Cast<object>().ToList();

                if (!Compare(la.Count, lb.Count, name))
                    return false;

                for (int i = 0; i < la.Count; i++)
                {
                    if (!Compare(la[i], lb[i], name))
                    {
                        return false;
                    }
                }

                return true;
            }

            return a == b;
        }

        private static void AssertEquality(object a, object b, string name, bool mustBeEquals)
        {
            if (a is float && b is float)
            {
                a = ((float)a).ToString();
                b = ((float)b).ToString();
            }
            else if (a is IEnumerable && b is IEnumerable)
            {
                var la = (a as IEnumerable).Cast<object>().ToList();
                var lb = (b as IEnumerable).Cast<object>().ToList();

                AssertEquality(la.Count, lb.Count, name, mustBeEquals);

                for (int i = 0; i < la.Count; i++)
                {
                    AssertEquality(la[i], lb[i], name, mustBeEquals);
                }

                return;
            }
            else
            {
                Assert.AreEqual(UnityEngine.JsonUtility.ToJson(movement), UnityEngine.JsonUtility.ToJson(clone));
            }
            //else if (a is UG_ISerializable && b is UG_ISerializable)
            //{
            //    var la = (a as UG_ISerializable);
            //    var lb = (b as UG_ISerializable);

            //    a = la.Serialize();
            //    b = lb.Serialize();
            //}

            if (mustBeEquals)
            {
                Assert.AreEqual(a, b, name + " have differences");
            }
            else
            {
                Assert.AreNotEqual(a, b, name + " are equals");
            }
        }
    }
}
