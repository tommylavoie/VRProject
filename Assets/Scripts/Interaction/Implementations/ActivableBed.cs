using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivableBed : AbstractActivable
{
    /* ==== Public variables ==== */
    public Goal BedGoal;
    public AudioClip bedAudioClip = null;

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
            if (bedAudioClip != null && BedGoal.isStarted())
                PlayAtPosition(this.transform.position);

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

        audioSource.clip = bedAudioClip;
        audioSource.volume = 1.0f;
        audioSource.spatialBlend = 1.0f;
        audioSource.spatialize = true;

        audioSource.Play();
        Destroy(tmpGameObject, bedAudioClip.length);

        return audioSource;
    }
}
