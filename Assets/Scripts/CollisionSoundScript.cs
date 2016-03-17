using UnityEngine;
using System.Collections;

/* Class CollisionSoundScript : used to define a specific sound for a collision type */
public class CollisionSoundScript : MonoBehaviour 
{
	/* Sound variables */
	public AudioClip collisionPlayerClip = null;
	public float startingTime = 0.0f;
	public float durationTime = 0.0f;

	/* Core functions */
	void Start () 
	{	
	}	
	void Update () 
	{
	}

	/* Collision functions */
	void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
		{
			if(collisionPlayerClip != null)
			{
				Vector3 averageContactPosition = new Vector3(0, 0, 0);
				
				foreach (ContactPoint contact in collision.contacts) {
					averageContactPosition += contact.point;
				}
				averageContactPosition /= collision.contacts.Length;
				
				PlayAtPosition(averageContactPosition);
			}				
		}
    }
	
    void OnCollisionExit(Collision collision)
    {
    }
	
	/* Sounds functions */
	private AudioSource PlayAtPosition(Vector3 playPosition)
	{
		GameObject tmpGameObject =  new GameObject("TmpAudio");
		AudioSource audioSource = tmpGameObject.AddComponent<AudioSource>();
		
		tmpGameObject.transform.position = playPosition;
		
		float start = 0.0f;
		if(startingTime < collisionPlayerClip.length)
			audioSource.time = start = startingTime;	
		
		float duration = durationTime;
		if(durationTime < 0.01f || (collisionPlayerClip.length - start) < duration)
			duration = collisionPlayerClip.length - start;
		
		audioSource.clip = collisionPlayerClip;		
		audioSource.volume = 1.0f;
		audioSource.spatialBlend = 1.0f;
		audioSource.spatialize = true;

		audioSource.Play();
		Destroy(tmpGameObject, duration);

		return audioSource;
	}
}
