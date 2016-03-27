using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

[RequireComponent (typeof (Image))]
public class BlindnessRenderer : MonoBehaviour
{
    public List<UnityEngine.Sprite> blindnessSpritesList = new List<UnityEngine.Sprite>();

    private int currentSpriteID;
	private Image imageRenderer = null;
	
    void Start()
    {
        currentSpriteID = -1;
        
    }

    void Update()
    {
		if(imageRenderer == null)
			imageRenderer = this.gameObject.GetComponent<Image>();
		
        if (Input.GetKeyDown("space"))
        {
            currentSpriteID++;
			
			if(currentSpriteID >= blindnessSpritesList.Count)
				currentSpriteID = -1;
			
			if(currentSpriteID != -1)
			{
				imageRenderer.enabled = true;     				
				imageRenderer.sprite = blindnessSpritesList[currentSpriteID];
			}
			else
			{
				imageRenderer.enabled = false;        				
			}    
        }
    }
}

