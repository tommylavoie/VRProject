using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ActivatorTV))]
public class InteractivableTV : AbstractInteractivable
{
    /* ==== Public variables ==== */

    /* ==== Private variables ==== */

    /* ==== Start function ==== */
    void Start()
    {
        this.OnStart();
    }

    protected void OnStart()
    {
        base.OnStart();
    }

    void Update()
    {
        this.OnUpdate();
    }

    protected void OnUpdate()
    {
        base.OnUpdate();
    }

    /* ==== IInteractivable function ==== */
    protected override bool Interact(GameObject callingObject, bool state)
    {
        if (this.IsInteractionAuthorized(callingObject))
        {
            ActivatorTV activator = this.GetComponent<ActivatorTV>();

            if (activator != null)
                activator.OnActivator(this.gameObject, state);
            else
                Debug.Log("ActivatorTV is null");

            return true;
        }
        return false;
    }

    /*protected string InteractionDisplayText(int status)
	{
		
	}*/
}
