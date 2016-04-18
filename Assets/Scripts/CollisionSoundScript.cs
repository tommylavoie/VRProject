using UnityEngine;
using System.Collections;

/* Class CollisionSoundScript : used to define a specific sound for a collision type */
public class CollisionSoundScript : MonoBehaviour 
{
	/* Sound variables */
	public AudioClip collisionPlayerClip = null;
	public float startingTime = 0.0f;
	public float durationTime = 0.0f;

	/* ==== Basic functions ==== */
	void Start () 
	{	
	}	
	void Update () 
	{
	}

    /* ==== Getters ==== */
    public AudioClip GetCollidingSound()
    {
        return this.collisionPlayerClip;
    }

    public float GetStartingTime()
    {
        return this.startingTime;
    }
    public float GetEndingTime()
    {
        if (collisionPlayerClip != null)
            return Mathf.Min(this.startingTime + this.durationTime, collisionPlayerClip.length);
        else
            return 0.0f;
    }
    public float GetDuration()
    {
        if (collisionPlayerClip != null)
            return Mathf.Min(this.durationTime, collisionPlayerClip.length-this.startingTime);
        else
            return 0.0f;
    }
}