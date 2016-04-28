using UnityEngine;
using System.Collections;

public class Mission : MonoBehaviour
{
    public GoalComposite goals;
    public InfoText infoText;

    private bool done = false;

    void Start()
    {
        goals.OnStart();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(goals.IsDone() && !done)
        {
            OnEnd();
        }
	}

    public void OnEnd()
    {
        infoText.AddNews("Terminé!");
        infoText.AddNews("Appuyez sur Espace pour retourner au menu.");
        done = true;
    }
}
