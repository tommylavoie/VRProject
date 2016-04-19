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

    public CaneTriggerScript triggerScript = null;
    public Camera playerCamera = null;
    private Vector3 distanceFromPlayer;

    /* ==== Basic functions ==== */
    void Start () {
        goLeft = true;
        goUp = true;

        totalHorizontalAngle = 0;       
        totalVerticalAngle = 0;

        this.distanceFromPlayer = this.transform.localPosition;
    }
	void Update () {

        float horizontalStep;
        float verticalStep;

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

        /* On place le bras au bon endroit (dans le meme "axe" que les yeux) */
        float cameraRotationY = playerCamera.transform.localRotation.eulerAngles.y;
        Vector3 rotatedPosition = Quaternion.AngleAxis(cameraRotationY, Vector3.up) * this.distanceFromPlayer;
        this.transform.localPosition = rotatedPosition;

        /* Et on applique la rotation de la canne */
        Vector3 eulerAngles = this.transform.eulerAngles;
        
        Vector3 rotationVector = new Vector3(eulerAngles.x + verticalStep, cameraRotationY + 180 + totalHorizontalAngle, eulerAngles.z);
        this.transform.localRotation = Quaternion.Euler(rotationVector);
    }

    /* ==== Collision functions ==== */
    public bool IsColliding()
    {
        if (triggerScript == null)    return false;
        else                          return triggerScript.IsColliding();
    }
}
