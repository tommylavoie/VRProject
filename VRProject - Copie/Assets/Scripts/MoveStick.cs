using UnityEngine;
using System.Collections;

public class MoveStick : MonoBehaviour {
    public int maxAngle, minAngle;
    public int rotationSpeed;
    public Vector3 startRotation;

    public bool goLeft;
    int rotationSideCorrect;
    float angleTotal;

    // Use this for initialization
    void Start () {
        goLeft = true;
        transform.rotation = Quaternion.Euler(startRotation);
        angleTotal = 0;
	}
	
	// Update is called once per frame
	void Update () {

        float step;
        float newRotationY;
        

        // On vérifie que l'on dépasse pas les valeurs max 
        if ( angleTotal >= maxAngle)
        {
            goLeft = true;
        }

        else if (angleTotal<=minAngle)
        {
            goLeft = false;
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
            
        
        //On calcul l'angle de rotation à ajouter
        step = rotationSideCorrect * rotationSpeed * Time.deltaTime;

        //On met à jour la rotation et l'angle total
        angleTotal = angleTotal + step;
        newRotationY = transform.eulerAngles.y + step;

        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, newRotationY, transform.eulerAngles.z));
	}

    void Test( int a)
    {
        a =+ 1;
    }
}
