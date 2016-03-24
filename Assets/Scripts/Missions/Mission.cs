using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour
{
    public GoalComposite goals;

    void Start()
    {
        goals.OnStart();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(goals.IsDone())
        {
            OnEnd();
        }
	}

    public void OnEnd()
    {

    }
}
