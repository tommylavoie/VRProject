using UnityEngine;
using System.Collections;

public class FalconFollowing : MonoBehaviour
{
    public Transform handTranform = null;
    public Transform baseTranform = null;
    public Transform endOfHandTranform = null;
    public Transform targetTransform = null;

    void Update () {
        /*this.transform.position = baseTranform.position;*/
        //this.transform.rotation = endOfHandTranform.rotation;

        handTranform.transform.LookAt(targetTransform.position);

        Vector3 translation = baseTranform.position - this.transform.position;
        targetTransform.Translate(translation);
        this.transform.position = baseTranform.position;
    }
}
