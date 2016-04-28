using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    /* General variables */
    private Rigidbody rb;
    public GameObject cam;
    public float Speed = 1f;
    public float Step = 2f;

    public SphereManipulator hapticManager;

    /* Hand/Stick variables */
    public MoveHand playerHand = null;
    public CaneCollisionScript caneCollisionScript = null;
    public Transform stickEndEffector = null;

    /* Relative position variables */
    private Vector3 playerHandRelativePosition = new Vector3(0.0f, 0.0f, 0.0f);

    /* Collision resolving variables (not really used ftm) */
    private Vector3 previousPosition;
    private Vector3 previousStickRelativePosition;
    public bool hasToReturnToPreviousPosition = false;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        this.previousStickRelativePosition = this.playerHand.transform.position - this.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Debug.Log(caneCollisionScript.IsColliding());
        if (!hasToReturnToPreviousPosition)
        {
            this.previousPosition = this.transform.position;
            UpdateMove();

            /* Temporal fix for a bug (flying player) */
            Vector3 fixedPosition = this.transform.position;
            fixedPosition.y = 1.02f;
            this.transform.position = fixedPosition;
        }
    }

    public void ReturnToPreviousPosition(Vector3 stickTranslationToPreventCollision)
    {
        /*this.transform.position = this.transform.position + (this.previousPosition - this.transform.position);
        this.hasToReturnToPreviousPosition = true;*/
    }
    private void UpdateMove()
    {
        Vector3 moveTo = Vector3.zero;
        this.previousStickRelativePosition = this.playerHand.transform.position - this.transform.position;

        bool isForwardDown = (Input.GetAxis("Vertical") > 0);
        bool isBackwardDown = (Input.GetAxis("Vertical") < 0);

        if (this.hapticManager != null)
        {
            isForwardDown = (isForwardDown || this.hapticManager.IsTopButtonDown());
            isBackwardDown = (isBackwardDown || this.hapticManager.IsBotButtonDown());
        }

        /* Vertical move */
        if (isForwardDown)
            moveTo += MoveForward();
        else if (isBackwardDown)
            moveTo += MoveBackward();
        
        /* Call to moving function */
        if (moveTo != Vector3.zero)
            Move(moveTo);
    }

    private Vector3 MoveForward()
    {
        Vector3 cameraEulerAngles = this.cam.transform.rotation.eulerAngles;
        Vector3 rotatedForward = Quaternion.AngleAxis(cameraEulerAngles.y, Vector3.up) * Vector3.forward;
        
        return rotatedForward;
    }
    private Vector3 MoveBackward()
    {
        Vector3 cameraEulerAngles = this.cam.transform.rotation.eulerAngles;
        Vector3 rotatedBackward = Quaternion.AngleAxis(cameraEulerAngles.y, Vector3.up) * (-1.0f * Vector3.forward);

        return rotatedBackward;
    }
    private Vector3 MoveLeftward()
    {
        Vector3 cameraEulerAngles = this.cam.transform.rotation.eulerAngles;
        Vector3 rotatedLeftward = Quaternion.AngleAxis(cameraEulerAngles.y, Vector3.up) * (-1.0f * Vector3.right);

        return rotatedLeftward;
    }
    private Vector3 MoveRightward()
    {
        Vector3 cameraEulerAngles = this.cam.transform.rotation.eulerAngles;
        Vector3 rotatedRightward = Quaternion.AngleAxis(cameraEulerAngles.y, Vector3.up) * Vector3.right;
        
        return rotatedRightward;
    }

    private bool IsMovePossible(Vector3 cameraPosition, Vector3 moveTo)
    {
        bool moveToRaycast = !Physics.Raycast(this.transform.position, moveTo, 1.0f);

        bool stickForwardRaycast = !Physics.Raycast(stickEndEffector.position, stickEndEffector.up, 0.005f);
        bool stickTopRaycast = !Physics.Raycast(stickEndEffector.position, -1.0f * stickEndEffector.forward, 0.055f);
        bool stickRightRaycast = !Physics.Raycast(stickEndEffector.position, stickEndEffector.right, 0.055f);
        bool stickBotRaycast = !Physics.Raycast(stickEndEffector.position, stickEndEffector.forward, 0.055f);
        bool stickLeftRaycast = !Physics.Raycast(stickEndEffector.position, -1.0f * stickEndEffector.right, 0.055f);

        /* If we move backwward : only the player can collide */
        if (Input.GetAxis("Vertical") < 0)
            return moveToRaycast;
        /* Otherwise it means we move forward : only the stick can collide */
        else
            return stickForwardRaycast && stickTopRaycast && stickBotRaycast && stickRightRaycast && stickLeftRaycast;
    }

    private void Move(Vector3 moveTo)
    {
        Vector3 camPosition = new Vector3(cam.transform.position.x, 0, cam.transform.position.z);
        Vector3 step = new Vector3(0, Step, 0);
        Vector3 movePosition = transform.position;

        bool movePossible = IsMovePossible(camPosition, moveTo);

        if (movePossible)
            movePosition = Vector3.MoveTowards(transform.position, camPosition + moveTo + step, Time.fixedDeltaTime  * Speed);

        transform.position = movePosition;
    }
}
