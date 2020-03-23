using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInUnity.Core.System.Promises
{
    public class Promise
    {
        private byte status = 0;
        private object result = null;

        private List<Action> thens = new List<Action>();
        private List<Action<object>> catches = new List<Action<object>>();
        private List<Action> finallies = new List<Action>();

        private bool thenCalled = false;
        private bool catchCalled = false;
        private bool finallyCalled = false;

        private Promise()
        {
        }

        public Promise(Action<Action, Action<object>> callbacks)
        {
            callbacks(() => Resolve(), e => Reject(e));
        }

        public Promise Then(Action then)
        {
            if (status == 2)
            {
                if (!thenCalled)
                {
                    then();
                    thenCalled = true;
                }
            }
            else if (status == 0)
            {
                thens.Add(then);
            }

            return this;
        }

        public Promise Finally(Action @finally)
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

        public Promise Catch(Action<object> @catch)
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

        private void Resolve()
        {
            if (status == 0)
            {
                status = 2;

                CallThens();
                CallFinallies();
            }
        }

        private void Reject(object err)
        {
            if (status == 0)
            {
                status = 1;
                result = err;

                CallCatches();
                CallFinallies();
            }
        }

        private void CallThens()
        {
            if (thens.Any())
            {
                thens.ForEach(m => m());
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
