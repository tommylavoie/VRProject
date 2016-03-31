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
        Begin();
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
        End();
        done = true;
    }

    virtual protected void Begin()
    {

    }

    virtual protected void Execute()
    {
        
    }

    virtual protected void End()
    {

    }

    public void Complete()
    {
        completed = true;
    }

    public bool isStarted()
    {
        return started;
    }

    public bool IsDone()
    {
        return done;
    }
}
