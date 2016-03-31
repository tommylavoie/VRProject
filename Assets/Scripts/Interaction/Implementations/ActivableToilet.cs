using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivableToilet : AbstractActivable
{
    /* ==== Public variables ==== */
    public Goal ToiletGoal;
    public AudioClip toiletAudioClip = null;

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
            if (toiletAudioClip != null && ToiletGoal.isStarted())
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

        audioSource.clip = toiletAudioClip;
        audioSource.volume = 1.0f;
        audioSource.spatialBlend = 1.0f;
        audioSource.spatialize = true;

        audioSource.Play();
        Destroy(tmpGameObject, toiletAudioClip.length);

        return audioSource;
    }
}
