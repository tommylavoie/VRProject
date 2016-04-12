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
        //currentSpriteID = -1;
        imageRenderer = this.gameObject.GetComponent<Image>();
        //LoadHandicap();
    }

    void Update()
    {
		/*if(imageRenderer == null)
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
        }*/
    }

    void LoadHandicap()
    {
        int handicap = PlayerPrefs.GetInt("Handicap");
        if (handicap == -1) imageRenderer.enabled = false;
        else
        {
            imageRenderer.enabled = true;
            imageRenderer.sprite = blindnessSpritesList[handicap];
        }
    }

    public static int NONE = -1;
    public static int CENTRAL_LOW = 0;
    public static int CENTRAL_STRONG = 1;
    public static int BLUR = 2;
    public static int PERIPHERAL_LOW = 3;
    public static int PERIPHERAL_STRONG = 4;
}

