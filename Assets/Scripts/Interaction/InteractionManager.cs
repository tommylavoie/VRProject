using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionManager : MonoBehaviour 
{
	/*
	 * TODO : 
	 *		- Move the UI things in a manager 
	 *		- Test the raycast range when eveything will be in place (can bug with small or on-ground objects)
	 */
		
	enum InteractionState 
	{
		NotInteracting, 
		PendingInteracting,
		Holding
	};
	
	/* ==== Public variables ==== */	
	public Transform holdingTransform = null;
	
	/* ==== Private variables ==== */ 	
	private InteractionState interactionState = InteractionState.NotInteracting;
	public Text interactTextField = null;
	
	private AbstractInteractivable currentInteractionObject = null;	
	private float startInteractionTime = 0.0f;

    private LayerMask raycastLayerMask;

    public SphereManipulator hapticManager;

    /* ==== Start function ==== */
    void Start () 
	{
        this.raycastLayerMask = ~((1 << 8) | (1 << 9) | (1 << 10));
    }
	
	/* ==== Update function ==== */
	void Update () 
	{		
		Camera fpsCamera = Camera.main;
		if(fpsCamera != null)
		{
            bool interactTextFieldVisibility = false;

            RaycastHit hitInfo;
            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * 2.5f, Color.green);
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hitInfo, 2.5f, this.raycastLayerMask)) 
			{
				AbstractInteractivable interactivableObject = hitInfo.collider.gameObject.GetComponent<AbstractInteractivable>();
				if(interactivableObject != null)
                {
                    /* Get the interact buttons states */
                    bool interactButtonPressed = Input.GetButtonDown("Interact");

                    if (this.hapticManager != null)
                        interactButtonPressed = (interactButtonPressed || this.hapticManager.IsLeftButtonPressed());

                    /* Start the tests */
                    if (this.interactionState == InteractionState.NotInteracting)
                    {
                        if (interactButtonPressed)
                        {
                            currentInteractionObject = interactivableObject;
							
							if(interactivableObject.holdingTimeForActivation > 0.01f)
							{								
								this.interactionState = InteractionState.PendingInteracting;
								this.startInteractionTime = Time.time;								
							}
							else
							{
								this.LaunchTheInteraction();
							}								
						}		
						else
                        {
                            interactTextFieldVisibility = true;	
							
							if(interactivableObject.holdingTimeForActivation > 0.01f)
							{								
								interactTextField.text = "Hold the left button to Interact";			
							}
							else
							{
								interactTextField.text = "Press the left button to Interact";									
							}		
						}	
					}
					else if(this.interactionState == InteractionState.PendingInteracting)
                    {
                        if (interactivableObject == currentInteractionObject)
						{
							if(interactButtonPressed)		
							{
								if((this.startInteractionTime + interactivableObject.holdingTimeForActivation) < Time.time)
								{
									this.LaunchTheInteraction();								
									interactTextFieldVisibility = false;			
								}		
								else
								{
									interactTextFieldVisibility = true;		
									interactTextField.text = "Keep the left button to Interact";											
								}															
							}						
							else
							{
								StopTheInteraction();
							}	
						}
						else
						{
							StopTheInteraction();
						}
					}
					else
                    {
                        if (interactButtonPressed)		
						{
							StopTheInteraction();															
						}						
						else
						{
							interactTextFieldVisibility = true;		
							interactTextField.text = "Press the left button to Release";	
						}	
					}				
				}
				else
				{
					StopTheInteraction();
				}
			}
			
			this.SetInteractTextFieldVisibility(interactTextFieldVisibility);
		}				
	}
	
	private bool LaunchTheInteraction()
	{
		bool hasInteract = this.currentInteractionObject.OnInteract(this.gameObject, true);			
		if(hasInteract)
		{
			if(this.currentInteractionObject.isHoldingInteraction)
			{
				this.interactionState = InteractionState.Holding;										
			}			
		}
		return hasInteract;
	}
	private void StopTheInteraction()
	{
		bool hasInteract = true;
		
		if(this.interactionState == InteractionState.Holding)
			hasInteract = this.currentInteractionObject.OnInteract(this.gameObject, false);			
		
		if(hasInteract)
		{
			this.currentInteractionObject = null;
			this.interactionState = InteractionState.NotInteracting;
		}
	}

	public Transform GetHoldingTransform()
	{
		return this.holdingTransform;
	}
	
	/* ==== UI functions ==== */
	private void SetInteractTextFieldVisibility(bool visibility)
	{			
		if(this.interactTextField != null)
			this.interactTextField.enabled  = visibility;
	}
}
