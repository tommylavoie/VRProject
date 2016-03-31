using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ActivatorToilet))]
public class InteractivableToilet : AbstractInteractivable
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
            ActivatorToilet activator = this.GetComponent<ActivatorToilet>();

            if (activator != null)
                activator.OnActivator(this.gameObject, state);
            else
                Debug.Log("ActivatorToilet is null");

            return true;
        }
        return false;
    }

    /*protected string InteractionDisplayText(int status)
	{
		
	}*/
}