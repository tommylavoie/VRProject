using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject cam;
    public float Speed = 1f;
    public float Step = 2f;
    
    public MoveStick playerStick = null;
    private Vector3 playerStickRelativePosition = new Vector3(0.0f, 0.0f, 0.0f);

    private bool isColliding = false;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();

        if (this.playerStick != null)
            this.playerStickRelativePosition = this.playerStick.transform.position - this.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateMove();
    }

    void UpdateMove()
    {
        Vector3 moveTo = Vector3.zero;

        /* Vertical move */
        if (Input.GetAxis("Vertical") > 0)
            moveTo += MoveForward();
        else if (Input.GetAxis("Vertical") < 0)
            moveTo += MoveBackward();

        /* Horizontal move */
        if (Input.GetAxis("Horizontal") > 0)
            moveTo += MoveRightward();
        else if (Input.GetAxis("Horizontal") < 0)
            moveTo += MoveLeftward();

        /* Call to moving function */
        if (moveTo != Vector3.zero)
            Move(moveTo);
    }

    Vector3 MoveForward()
    {
        Vector3 moveTo = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        return moveTo;
    }
    Vector3 MoveBackward()
    {
        Vector3 moveTo = new Vector3(-cam.transform.forward.x, 0, -cam.transform.forward.z);
        return moveTo;
    }
    Vector3 MoveLeftward()
    {
        Vector3 moveTo = new Vector3(-cam.transform.right.x, 0, -cam.transform.right.z);
        return moveTo;
    }
    Vector3 MoveRightward()
    {
        Vector3 moveTo = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);
        return moveTo;
    }

    bool IsMovePossible(Vector3 moveTo)
    {
        return !Physics.Raycast(transform.position + this.playerStickRelativePosition, new Vector3(moveTo.x, -0.5f, moveTo.z), 1);
    }

    void Move(Vector3 moveTo)
    {
        Vector3 camPosition = new Vector3(cam.transform.position.x, 0, cam.transform.position.z);
        Vector3 step = new Vector3(0, Step, 0);
        Vector3 movePosition = transform.position;
        bool movePossible = true;

        if (isColliding)
            movePossible = IsMovePossible(moveTo);
        if (movePossible)
            movePosition = Vector3.MoveTowards(transform.position, camPosition + moveTo + step, Time.deltaTime * Speed);
        else
            movePosition = Vector3.MoveTowards(transform.position, camPosition + moveTo, Time.deltaTime * 0.5f);

        transform.position = movePosition;

        if (this.playerStick != null)
        {
            Transform stickTransform = this.playerStick.transform;
            stickTransform.position = movePosition + playerStickRelativePosition;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(this.playerStick != null)
        {
            if (!collision.gameObject.tag.Equals("Floor"))
            {
                this.isColliding = true;

                if (collision.contacts[0].thisCollider.name == this.playerStick.gameObject.name)
                {
                    this.playerStick.goLeft = !this.playerStick.goLeft;
                    this.playerStick.goUp = !this.playerStick.goUp;
                }          
            }
            else
            {
                if (collision.contacts[0].thisCollider.name == this.playerStick.gameObject.name)
                {
                    this.playerStick.goUp = !this.playerStick.goUp;
                }
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.tag.Equals("Floor"))
            this.isColliding = false;
    }
}
