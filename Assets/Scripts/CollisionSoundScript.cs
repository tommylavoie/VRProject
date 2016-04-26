using UnityEngine;
using System.Collections;

/* Class CollisionSoundScript : used to define a specific sound for a collision type */
public class CollisionSoundScript : MonoBehaviour 
{
	/* Sound variables */
	public AudioClip collisionPlayerClip = null;
	public float startingTime = 0.0f;
	public float durationTime = 0.0f;

    /* ==== Getters ==== */
    public AudioClip GetCollidingSound()
    {
        return this.collisionPlayerClip;
    }

    public float GetStartingTime()
    {
        if (this.startingTime > 0.01f)
            return this.startingTime;
        else
            return 0.0f;
    }
    public float GetEndingTime()
    {
        if (collisionPlayerClip != null)
        {
            if (this.durationTime > 0.01f)
                return Mathf.Min(this.startingTime + this.durationTime, collisionPlayerClip.length);
            else
                return collisionPlayerClip.length;
        }
        else
            return 0.0f;
    }
}