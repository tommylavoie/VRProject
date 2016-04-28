using UnityEngine;
using System.Collections;

public class SphereManipulator : MonoBehaviour {

	public int falcon_num = 0;
	public bool[] button_states = new bool[4];

    bool [] curr_buttons = new bool[4];
	public Vector3 constantforce;

	public Transform hapticTip;
	public Transform godObject;
	public float godObjectMass;

	private float minDistToMaxForce = 0.0005f;
	private float maxDistToMaxForce = 0.009f;
	public float hapticTipToWorldScale;
	
	private float savedHapticTipToWorldScale;
	
	public bool useMotionCompensator;
	
	private bool haveReceivedTipPosition = false;
	private int receivedCount = 0;

    /* Variable for Z fixing */
    private float hapticTipBaseLocalPosZ;
    private float godObjectBaseLocalPosZ;

    /* Pressed buttons */
    private bool[] buttonPreviousState = new bool[4];
    private bool[] buttonJustPressed = new bool[4];

    // Use this for initialization
    void Start () {
        this.hapticTipBaseLocalPosZ = this.hapticTip.localPosition.z;
        this.godObjectBaseLocalPosZ = this.godObject.localPosition.z;

        savedHapticTipToWorldScale = hapticTipToWorldScale;
		
		FalconUnity.setForceField(falcon_num,constantforce);
		
		Vector3 tipPositionScale = new Vector3(1,1,-1);
		tipPositionScale *= hapticTipToWorldScale;
		
		FalconUnity.updateHapticTransform(falcon_num, transform.position, transform.rotation, tipPositionScale, useMotionCompensator, 1/60.0f);
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < 4; i++)
        {
            if (buttonPreviousState[i] == button_states[i])
                buttonJustPressed[i] = false;
            else
            {
                if(button_states[i])
                    buttonJustPressed[i] = true;
                buttonPreviousState[i] = button_states[i];
            }
        }

		
		if (! haveReceivedTipPosition ) {
			Vector3 posTip2;
			bool result = FalconUnity.getTipPosition(falcon_num, out posTip2);
			if(!result){
//				Debug.Log("Error getting tip position");
				return;
			}
			receivedCount ++;
			
			if (receivedCount < 25 && (posTip2.x == 0 && posTip2.y == 0 &&posTip2.z == 0)) {
				return;
			}
			
			hapticTip.position = posTip2;
			
			godObject.position = posTip2;
			
			Debug.Log ("Initialized with tip position: " + posTip2);
			FalconUnity.setSphereGodObject(falcon_num,godObject.localScale.x * godObject.GetComponent<SphereCollider>().radius, godObjectMass,godObject.position, minDistToMaxForce * hapticTipToWorldScale, maxDistToMaxForce * hapticTipToWorldScale);
			
			haveReceivedTipPosition = true;
		}
		
		Vector3 tipPositionScale = new Vector3(1,1,-1);
		tipPositionScale *= hapticTipToWorldScale;
		
		if (savedHapticTipToWorldScale != hapticTipToWorldScale) {
			FalconUnity.setSphereGodObject(falcon_num,godObject.localScale.x * godObject.GetComponent<SphereCollider>().radius, godObjectMass,godObject.position, minDistToMaxForce * hapticTipToWorldScale, maxDistToMaxForce * hapticTipToWorldScale);
			savedHapticTipToWorldScale = hapticTipToWorldScale;
			
		}
			
		FalconUnity.updateHapticTransform(falcon_num, transform.position, transform.rotation, tipPositionScale, useMotionCompensator, Time.deltaTime);

        Vector3 posGod;
		bool res = FalconUnity.getGodPosition(falcon_num, out posGod);
		if(!res){
//			Debug.Log("Error getting god tip position");
			return;
		}
		Vector3 posTip;
		res = FalconUnity.getTipPosition(falcon_num, out posTip);
		if(!res){
//			Debug.Log("Error getting tip position");
			return;
		}
		hapticTip.position = posTip;
		
		godObject.position = posGod;
		godObject.rotation = new Quaternion(0,0,0,1);

        /* ================================================================= */
        this.SetLocalPosZ(this.hapticTip, this.hapticTipBaseLocalPosZ);
        this.SetPositiveLocalPosZ(this.godObject);
        /* ================================================================= */

        //	FalconUnity.setForceField(falcon_num,force);
    }


    void LateUpdate() {
	
		bool res = FalconUnity.getFalconButtonStates(falcon_num, out curr_buttons);

		
		if(!res){
//			Debug.Log("Error getting button states");
			return;
		}
		//go through the buttons, seeing which are pressed
		for(int i=0;i<4;i++){
			if(button_states[i] && button_states[i] != curr_buttons[i]){
				buttonPressed(i);
			}
			else if(button_states[i] && button_states[i] != curr_buttons[i]){
				buttonReleased(i);
			}
			button_states[i] = curr_buttons[i];
		}
	}

    private void SetLocalPosZ(Transform t, float value)
    {
        Vector3 newPos = t.localPosition;
        newPos.z = value;
        t.localPosition = newPos;
    }
    private void SetPositiveLocalPosZ(Transform t)
    {
        if (t.localPosition.z < 0)
            this.SetLocalPosZ(t, 0.0f);
    }
	
	public void buttonPressed(int i){
		
		switch(i){
		case 0:	
			break;
		case 1: 
			break;
		case 2:
			
			break;
		case 3:
			break;
			
		}
	}
    public void buttonReleased(int i){
		
		switch(i){
		case 0:			
			break;
		case 1: 
			break;
		case 2:
			
			break;
		case 3:
			break;
			
		}
	}

    /* Buttons pressed (last frame only) */
    public bool IsTopButtonPressed()
    {
        return this.buttonJustPressed[2];
    }
    public bool IsRightButtonPressed()
    {
        return this.buttonJustPressed[3];
    }
    public bool IsBotButtonPressed()
    {
        return this.buttonJustPressed[0];
    }
    public bool IsLeftButtonPressed()
    {
        return this.buttonJustPressed[1];
    }

    /* Buttons down */
    public bool IsTopButtonDown()
    {
        return this.button_states[2];
    }
    public bool IsRightButtonDown()
    {
        return this.button_states[3];
    }
    public bool IsBotButtonDown()
    {
        return this.button_states[0];
    }
    public bool IsLeftButtonDown()
    {
        return this.button_states[1];
    }
}
