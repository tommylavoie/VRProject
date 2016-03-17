using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (ActivatorOven))]
public class InteractivableOven : AbstractInteractivable
{
	/* ==== Public variables ==== */
	
	/* ==== Private variables ==== */ 	
	
	/* ==== Start function ==== */
	void Start ()
	{
		this.OnStart();
	}
	
	protected void OnStart()
	{
		base.OnStart();
	}
			
	void Update ()
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
		if(this.IsInteractionAuthorized(callingObject))
		{				
			ActivatorOven activator = this.GetComponent<ActivatorOven>();
			
			if(activator != null)
				activator.OnActivator(this.gameObject, state);	
			else
				Debug.Log("ActivatorOven is null");
			
			return true;
		}		
		return false;
	}
	
	/*protected string InteractionDisplayText(int status)
	{
		
	}*/
}
