using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent (typeof (SpriteRenderer))]
public class BlindnessRenderer : MonoBehaviour
{
    public List<UnityEngine.Sprite> blindnessSpritesList = new List<UnityEngine.Sprite>();

    private int currentSpriteID;
	private SpriteRenderer spriteRenderer = null;
	
    void Start()
    {
        currentSpriteID = -1;
    }

    void Update()
    {
		if(spriteRenderer == null)
			spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
		
        if (Input.GetKeyDown("space"))
        {
            currentSpriteID++;
			
			if(currentSpriteID >= blindnessSpritesList.Count)
				currentSpriteID = -1;
			
			if(currentSpriteID != -1)
			{
				spriteRenderer.enabled = true;     				
				spriteRenderer.sprite = blindnessSpritesList[currentSpriteID];
			}
			else
			{
				spriteRenderer.enabled = false;        				
			}    
        }
    }
}

