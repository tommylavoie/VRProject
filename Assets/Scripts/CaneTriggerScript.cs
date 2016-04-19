using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaneTriggerScript : MonoBehaviour
{
    List<Collider> collidersTriggering = new List<Collider>();

    public MoveHand moveHandScript = null;

    /* ==== Collision functions ==== */
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(Time.time + " : Began triggering '" + collider.gameObject.name + "'");

        /* The cane either collides with the floor ... */
        if (collider.gameObject.tag.Equals("Floor"))
        {
            moveHandScript.goUp = !moveHandScript.goUp;
        }
        /* Or with an object */
        else
        {
            moveHandScript.goLeft = !moveHandScript.goLeft;
            moveHandScript.goUp = !moveHandScript.goUp;
        }

        this.collidersTriggering.Add(collider);
    }
    void OnTriggerExit(Collider collider)
    {
        this.collidersTriggering.Remove(collider);
    }

    public bool IsColliding(){
        return (collidersTriggering.Count > 0);
    }
}
