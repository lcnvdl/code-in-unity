using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeInUnity.Core.System.Promises
{
  public class Promise
  {
    private const byte STATUS_NONE = 0;
    private const byte STATUS_REJECTED = 1;
    private const byte STATUS_RESOLVED = 2;

    private byte status = STATUS_NONE;
    private object result = null;

    private List<Action> thens = new List<Action>();
    private List<Action<object>> catches = new List<Action<object>>();
    private List<Action> finallies = new List<Action>();

    private bool thenCalled = false;
    private bool catchCalled = false;
    private bool finallyCalled = false;

    public bool IsSuccess => this.status == STATUS_RESOLVED;

    public bool IsError => this.status == STATUS_REJECTED;

    public bool IsFinished => this.status != STATUS_NONE;

    private Promise()
    {
    }

    public Promise(Action<Action, Action<object>> callbacks)
    {
      callbacks(() => Resolve(), e => Reject(e));
    }

    public static Promise Resolved => new Promise().Resolve();

    public static Promise Rejected => new Promise().Reject(new Exception());

    public IEnumerator ToEnumerator()
    {
      while (this.status == STATUS_NONE)
      {
        yield return new UnityEngine.WaitForEndOfFrame();
      }
    }

    public Promise Then(Action then)
    {
      if (status == STATUS_RESOLVED)
      {
        if (!thenCalled)
        {
          then();
          thenCalled = true;
        }
      }
      else if (status == STATUS_NONE)
      {
        thens.Add(then);
      }

      return this;
    }

    public Promise Finally(Action @finally)
    {
      if (status != STATUS_NONE)
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
      if (status == STATUS_REJECTED)
      {
        if (!catchCalled)
        {
          @catch(result);
          catchCalled = true;
        }
      }
      else if (status == STATUS_NONE)
      {
        catches.Add(@catch);
      }

      return this;
    }

    private Promise Resolve()
    {
      if (status == STATUS_NONE)
      {
        status = STATUS_RESOLVED;

        CallThens();
        CallFinallies();
      }

      return this;
    }

    private Promise Reject(object err)
    {
      if (status == STATUS_NONE)
      {
        status = STATUS_REJECTED;
        result = err;

        CallCatches();
        CallFinallies();
      }

      return this;
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
