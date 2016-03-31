using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivableDoor : AbstractActivable
{
    /* ==== Public variables ==== */
    public AudioClip doorAudioClip = null;

    /* ==== Private variables ==== */

    /* ==== Start function ==== */
    void Start()
    {
        this.OnStart();
    }

    protected void OnStart()
    {
        base.OnStart();
    }

    void Update()
    {
        this.OnUpdate();
    }

    protected void OnUpdate()
    {
        base.OnUpdate();
    }

    /* ==== IActivable function ==== */
    protected override bool Activate(AbstractActivator activatorObject, bool state)
    {
        // AbstractActivator activatorObject
        if (this.IsActivationAuthorized(activatorObject))
        {
            if (doorAudioClip != null)
            {
                PlayAtPosition(this.transform.position);
                OpenDoor();
            }

            return true;
        }
        return false;
    }

    /* Sounds functions */
    private AudioSource PlayAtPosition(Vector3 playPosition)
    {
        GameObject tmpGameObject = new GameObject("TmpAudio");
        AudioSource audioSource = tmpGameObject.AddComponent<AudioSource>();

        tmpGameObject.transform.position = playPosition;

        audioSource.clip = doorAudioClip;
        audioSource.volume = 1.0f;
        audioSource.spatialBlend = 1.0f;
        audioSource.spatialize = true;

        audioSource.Play();
        Destroy(tmpGameObject, doorAudioClip.length);

        return audioSource;
    }

    private void OpenDoor()
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
    }
}
