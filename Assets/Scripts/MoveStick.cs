using UnityEngine;
using System.Collections;

public class MoveStick : MonoBehaviour {
    public Vector3 startRotation;

    public int maxRotationAngle, minRotationAngle;
    public int rotationSpeed;
    public bool goLeft;

    int rotationSideCorrect;
    float angleRotationTotal;


    public int maxUpAngle, minUpAngle;
    public int upSpeed;
    public bool goUp;
    
    int upSideCorrect;
    float angleUpTotal;


    // Use this for initialization
    void Start () {
        goLeft = true;
        angleRotationTotal = 0;
       
        goUp = true;
        angleUpTotal = 0;

        transform.rotation = Quaternion.Euler(startRotation);
	}
	
	// Update is called once per frame
	void Update () {

        float stepRotation;
        float newRotationY;

        float stepUp;
        float newRotationX;


        // On vérifie que l'on dépasse pas les valeurs max de rotation
        if (angleRotationTotal >= maxRotationAngle)
        {
            goLeft = true;
        }

        else if (angleRotationTotal <= minRotationAngle)
        {
            goLeft = false;
        }

        //On fait de meme pour de bas en haut
        if (angleUpTotal >= maxUpAngle)
        {
            goUp = true;
        }

        else if (angleUpTotal <= minUpAngle)
        {
            goUp = false;
        }



        // On définie le sens de roation de la canne
        if (goLeft)
        {
            rotationSideCorrect = -1;
        }
        else
        {
            rotationSideCorrect = 1;
        }

        if (goUp)
        {
            upSideCorrect = -1;
        }
        else
        {
            upSideCorrect = 1;
        }

        //On calcul l'angle de rotation à ajouter
        stepRotation = rotationSideCorrect * rotationSpeed * Time.deltaTime;
        stepUp = upSideCorrect * upSpeed * Time.deltaTime;


        //On met à jour la rotation et l'angle total
        angleRotationTotal = angleRotationTotal + stepRotation;
        newRotationY = transform.eulerAngles.y + stepRotation;

        angleUpTotal = angleUpTotal + stepUp;
        newRotationX = transform.eulerAngles.x + stepUp;

        transform.rotation = Quaternion.Euler(new Vector3(newRotationX, newRotationY, transform.eulerAngles.z));
    }
}
