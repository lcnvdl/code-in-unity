using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeInUnity.Core.System
{
    public class Promise<T>
    {
        private byte status = 0;
        private object result = null;

        private List<Action<T>> thens = new List<Action<T>>();
        private List<Action<object>> catches = new List<Action<object>>();
        private List<Action> finallies = new List<Action>();

        private bool thenCalled = false;
        private bool catchCalled = false;
        private bool finallyCalled = false;

        private Promise()
        {
        }

        public Promise(Action<Action<T>, Action<object>> callbacks)
        {
            callbacks(m => Resolve(m), e => Reject(e));
        }

        public Promise<T> Then(Action<T> then)
        {
            if (status == 2)
            {
                if (!thenCalled)
                {
                    then((T)result);
                    thenCalled = true;
                }
            }
            else if (status == 0)
            {
                thens.Add(then);
            }

            return this;
        }

        public Promise<T> Finally(Action @finally)
        {
            if (status != 0)
            {
                if (!finallyCalled)
                {
                    @finally();
                    finallyCalled = true;
                }
            }
            else
            {
                finallies.Add(@finally);
            }

            return this;
        }

        public Promise<T> Catch(Action<object> @catch)
        {
            if (status == 1)
            {
                if (!catchCalled)
                {
                    @catch(result);
                    catchCalled = true;
                }
            }
            else if (status == 0)
            {
                catches.Add(@catch);
            }

            return this;
        }

        private void Resolve(T arg)
        {
            if (status == 0)
            {
                result = arg;
                status = 2;

                CallThens();
                CallFinallies();
            }
        }

        private void Reject(object err)
        {
            if (status == 0)
            {
                result = err;
                status = 1;

                CallCatches();
                CallFinallies();
            }
        }

        private void CallThens()
        {
            if (thens.Any())
            {
                thens.ForEach(m => m((T)result));
                thenCalled = true;
            }
        }

        private void CallFinallies()
        {
            if (finallies.Any())
            {
                finallies.ForEach(m => m());
                finallyCalled = true;
            }
        }

        private void CallCatches()
        {
            if (catches.Any())
            {
                catches.ForEach(m => m(result));
                catchCalled = true;
            }
        }
    }
}
