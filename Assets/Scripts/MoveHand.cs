using UnityEngine;
using System.Collections;

public class MoveHand : MonoBehaviour {
    public int maxHorizontalAngle, minHorizontalAngle;
    public int horizontalSpeed;
    public bool goLeft;

    public int maxVerticalAngle, minVerticalAngle;
    public int verticalSpeed;
    public bool goUp;

    private int horizontalSideCorrect, vertialSideCorrect;
    private float totalHorizontalAngle, totalVerticalAngle;

    public CaneCollisionScript collisionScript = null;
    public Camera playerCamera = null;
    private Vector3 distanceFromCamera;

    private float cameraBaseAngleY;
    private float stickBaseAngleY;

    public bool automaticMoving = false;

    /* ==== Basic functions ==== */
    void Start () {
        goLeft = true;
        goUp = true;

        totalHorizontalAngle = 0;       
        totalVerticalAngle = 0;

        this.distanceFromCamera = this.transform.position - this.playerCamera.transform.position;
        this.cameraBaseAngleY = playerCamera.transform.rotation.eulerAngles.y;
        this.stickBaseAngleY = this.transform.rotation.eulerAngles.y;
    }
	void Update () {
        /* Move the hand */
        this.Move();
    }

    /* ==== Movement helpers ==== */
    private void Move()
    {
        float horizontalStep = 0.0f;
        float verticalStep = 0.0f;

        if (Input.GetButtonDown("SwitchCaneMode"))
            this.automaticMoving = !this.automaticMoving;

        if (this.automaticMoving)
            this.ComputeAutomaticRotation(ref horizontalStep, ref verticalStep);
        else
            this.ComputeManualRotation(ref horizontalStep, ref verticalStep);

        /* On place le bras au bon endroit (dans le meme "axe" que les yeux) */
        Vector3 cameraEulerAngles = this.playerCamera.transform.rotation.eulerAngles;
        float cameraDifferenceAngleY = cameraEulerAngles.y - this.cameraBaseAngleY;
        Vector3 relativePositionFromCamera = Quaternion.AngleAxis(cameraDifferenceAngleY, Vector3.up) * this.distanceFromCamera;
        this.transform.position = playerCamera.transform.position + relativePositionFromCamera;

        /* Et on applique la rotation de la canne */
        Vector3 stickEulerAngles = this.transform.eulerAngles;
        Vector3 rotationVector = new Vector3(stickEulerAngles.x + verticalStep, cameraEulerAngles.y + totalHorizontalAngle, stickEulerAngles.z);
        this.transform.localRotation = Quaternion.Euler(rotationVector);
    }
    private void ComputeAutomaticRotation(ref float horizontalStep, ref float verticalStep)
    {
        // On vérifie que l'on dépasse pas les valeurs max de rotation
        if (totalHorizontalAngle >= maxHorizontalAngle)
            goLeft = true;
        else if (totalHorizontalAngle <= minHorizontalAngle)
            goLeft = false;

        //On fait de meme pour de bas en haut
        if (totalVerticalAngle >= maxVerticalAngle)
            goUp = true;
        else if (totalVerticalAngle <= minVerticalAngle)
            goUp = false;

        // On définie le sens de rotation de la canne
        if (goLeft)
            horizontalSideCorrect = -1;
        else
            horizontalSideCorrect = 1;

        if (goUp)
            vertialSideCorrect = -1;
        else
            vertialSideCorrect = 1;

        //On calcul l'angle de rotation à ajouter
        horizontalStep = horizontalSideCorrect * horizontalSpeed * Time.deltaTime;
        verticalStep = vertialSideCorrect * verticalSpeed * Time.deltaTime;

        //On met à jour la rotation et l'angle total
        totalHorizontalAngle = totalHorizontalAngle + horizontalStep;
        totalVerticalAngle = totalVerticalAngle + verticalStep;
    }
    private void ComputeManualRotation(ref float horizontalStep, ref float verticalStep)
    {
        float factor, tmpTotalAngle;

        if (Input.GetButton("HorizontalCaneMove"))
        {
            if (Input.GetAxisRaw("HorizontalCaneMove") > 0)
                factor = 1.0f;
            else
                factor = -1.0f;

            horizontalStep = factor * horizontalSpeed * Time.deltaTime;
            tmpTotalAngle = totalHorizontalAngle + horizontalStep;

            if (tmpTotalAngle >= maxHorizontalAngle)
                horizontalStep = maxHorizontalAngle - totalHorizontalAngle;
            else if (tmpTotalAngle <= minHorizontalAngle)
                horizontalStep = minHorizontalAngle - totalHorizontalAngle;
        }
        if (Input.GetButton("VerticalCaneMove"))
        {
            if (Input.GetAxisRaw("VerticalCaneMove") > 0)
                factor = -1.0f;
            else
                factor = 1.0f;

            verticalStep = factor * verticalSpeed * Time.deltaTime;
            tmpTotalAngle = totalVerticalAngle + verticalStep;

            if (tmpTotalAngle >= maxVerticalAngle)
                verticalStep = maxVerticalAngle - totalVerticalAngle;
            else if (tmpTotalAngle <= minVerticalAngle)
                verticalStep = minVerticalAngle - totalVerticalAngle;
        }

        //On met à jour la rotation et l'angle total
        totalHorizontalAngle = totalHorizontalAngle + horizontalStep;
        totalVerticalAngle = totalVerticalAngle + verticalStep;
    }

    /* ==== Collision functions ==== */
    public bool IsColliding()
    {
        if (collisionScript == null)    return false;
        else                          return collisionScript.IsColliding();
    }
}
