using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaneCollisionScript : MonoBehaviour
{
    List<GameObject> objectColliding = new List<GameObject>();

    public MoveHand moveHandScript = null;
    public PlayerController playerController = null;

    void Update()
    {
    } 

    /* ==== Collision functions ==== */
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter");

        GameObject colliderObject = collision.gameObject;

        /* Find the average collision point */
        Vector3 averagePosition = new Vector3(0.0f, 0.0f, 0.0f);
        foreach (ContactPoint contact in collision.contacts)
        {
            averagePosition += contact.point;
        }
        averagePosition /= collision.contacts.Length;

        /* Find the distance from thhis point to the collider's AABB */
        Vector3 closestPoint;

        BoxCollider boxCollider = colliderObject.GetComponent<BoxCollider>();
        if (boxCollider != null)
            closestPoint = boxCollider.ClosestPointOnBounds(averagePosition);
        else
        {
            MeshCollider meshCollider = colliderObject.GetComponent<MeshCollider>();
            closestPoint = meshCollider.ClosestPointOnBounds(averagePosition);
        }

        /* And translate the stick to stop the collision */
        Vector3 stickTranslationToPreventCollision = -2 * (closestPoint - averagePosition);
        playerController.ReturnToPreviousPosition(stickTranslationToPreventCollision);

        /* The cane either collides with the floor or an object */
        if (colliderObject.tag.Equals("Floor"))
            moveHandScript.goUp = !moveHandScript.goUp;
        else
        {
            moveHandScript.goLeft = !moveHandScript.goLeft;
            moveHandScript.goUp = !moveHandScript.goUp;
        }

        /* Play the sound if there is a special collision sound for this collider */
        CollisionSoundScript soundScript = colliderObject.GetComponent<CollisionSoundScript>();
        if (soundScript != null)
        {
            AudioClip clip = soundScript.GetCollidingSound();
            if (clip != null)
                this.PlayAtPosition(clip, transform.GetChild(0).position, soundScript.GetStartingTime(), soundScript.GetEndingTime());
        }

        this.objectColliding.Add(colliderObject);
    }
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit");
        this.objectColliding.Remove(collision.gameObject);
    }

    public bool IsColliding()
    {
        return (objectColliding.Count > 0);
    }

    /* ==== Sounds functions ==== */
    private AudioSource PlayAtPosition(AudioClip clip, Vector3 playPosition, float start, float end)
    {
        GameObject tmpGameObject = new GameObject("TmpAudio");
        AudioSource audioSource = tmpGameObject.AddComponent<AudioSource>();

        tmpGameObject.transform.position = playPosition;

        audioSource.time = start;
        audioSource.clip = clip;
        audioSource.volume = 0.8f;
        audioSource.spatialBlend = 1.0f;
        audioSource.spatialize = true;

        audioSource.Play();        
        Destroy(tmpGameObject, end - start);

        return audioSource;
    }
}
