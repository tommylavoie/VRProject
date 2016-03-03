using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject cam;
    public float speed = 1f;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetAxis("Vertical") > 0)
        {
            MoveForward();
        }
	}

    void MoveForward()
    {
        Vector3 moveTo = cam.transform.position + new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        Vector3 movePosition = Vector3.MoveTowards(transform.position, moveTo, Time.deltaTime*speed);
        transform.position = movePosition;
        //if(rb.velocity.x < 1 && rb.velocity.z < 1)
            //rb.AddForce(new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z)*10f);
    }
}
