using UnityEngine;
using System.Collections;

public class ActivableGoal : AbstractActivable
{
    Goal attachedGoal;

	// Use this for initialization
	void Start ()
    {
        attachedGoal = GetComponent<Goal>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected override bool Activate(AbstractActivator activatorObject, bool state)
    {
        // AbstractActivator activatorObject
        if (this.IsActivationAuthorized(activatorObject))
        {
            attachedGoal.Complete();

            return true;
        }
        return false;
    }
}
