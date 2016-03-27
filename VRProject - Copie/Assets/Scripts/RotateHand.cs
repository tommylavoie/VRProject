using UnityEngine;
using System.Collections;

public class RotateHand : MonoBehaviour {
    [SerializeField] private GameObject Camera ;  
	// Use this for initialization
	
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {

       
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, Camera.transform.localEulerAngles.y, transform.eulerAngles.z));
        // récupérer la transform du stick ici :
        /*  
        Transform stickTransform;
        stickTransform = this.transform.GetChild(0).GetComponent<Transform>();
        */

        
    }
}
