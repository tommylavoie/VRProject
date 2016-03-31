using UnityEngine;
using System.Collections;

public class ActivableTV : AbstractActivable
{
    /* ==== Public variables ==== */
    public Goal TVGoal;
    public AudioClip tvAudioClip = null;

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
            if(TVGoal.isStarted())
            {
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                PlayAtPosition(this.transform.position);
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

        audioSource.clip = tvAudioClip;
        audioSource.volume = 0.1f;
        audioSource.spatialBlend = 1.0f;
        audioSource.spatialize = true;
        audioSource.loop = true;

        audioSource.Play();

        return audioSource;
    }
}
