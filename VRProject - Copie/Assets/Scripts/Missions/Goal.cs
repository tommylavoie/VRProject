using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    protected bool started = false;
    protected bool completed = false;
    protected bool done = false;
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        if (started)
        {
            OnExecute();
        }
	}

    public virtual void OnStart()
    {
        started = true;
    }

    public virtual void OnExecute()
    {
        if (!done)
        {
            if (!completed)
            {
                Execute();
            }
            else
            {
                OnEnd();
            }
        }
    }

    public virtual void OnEnd()
    {
        done = true;
    }

    virtual protected void Execute()
    {
        
    }

    public void Complete()
    {
        completed = true;
    }

    public bool IsDone()
    {
        return done;
    }
}
