using UnityEngine;
using System.Collections;

public class CaneCollisionScript : MonoBehaviour
{

	/* ==== Basic functions ==== */
	void Start (){	
	}
	void Update (){	
	}

    /* ==== Collision functions ==== */
    void OnTriggerEnter(Collider collider)
    {
        CollisionSoundScript collisionSoundScript = collider.gameObject.GetComponent<CollisionSoundScript>() as CollisionSoundScript;
        if (collisionSoundScript != null)
		{
            /*Vector3 averageContactPosition = this.DetermineCollisionAveragePoint(collision.contacts);
            this.PlayAtPosition(collisionSoundScript, averageContactPosition);*/
		}		
    }

    /* ==== Various helpers ==== */
    private Vector3 DetermineCollisionAveragePoint(ContactPoint[] contactPoints)
    {
        Vector3 averageContactPosition = new Vector3(0, 0, 0);
        
        int nbPoints = contactPoints.Length;
        for (int i = 0; i < nbPoints; i++)
        {
            averageContactPosition += contactPoints[i].point;
        }

        return (averageContactPosition / (float)nbPoints);
    }

    private void PlayAtPosition(CollisionSoundScript soundScript, Vector3 playPosition){
        AudioClip collidingSound = soundScript.GetCollidingSound();
        if (collidingSound != null)
        {
            /* Set time related variabless */
            float startingTime = soundScript.GetStartingTime();
            float endingTime = soundScript.GetEndingTime();
            float duration = soundScript.GetDuration();

            /* Create the audio source and set it */
            GameObject tmpGameObject = new GameObject("TmpAudio");
            AudioSource audioSource = tmpGameObject.AddComponent<AudioSource>();

            tmpGameObject.transform.position = playPosition;

            audioSource.clip = collidingSound;
            audioSource.volume = 1.0f;
            audioSource.spatialBlend = 1.0f;
            audioSource.spatialize = true;

            /* Start the playing and add a destroy timer */
            audioSource.Play();
            Destroy(tmpGameObject, duration);
        }
    }

}
