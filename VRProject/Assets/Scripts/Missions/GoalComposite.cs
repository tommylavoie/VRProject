using UnityEngine;
using System.Collections.Generic;

public class GoalComposite : Goal
{
    public List<Goal> goals = new List<Goal>();

    private Goal currentGoal;
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
	}

    override public void OnStart()
    {
        base.OnStart();
        StartNextGoal();
        if (currentGoal == null)
            Complete();
    }

    override protected void Execute()
    {
        base.Execute();
        if (currentGoal != null)
        {
            if (currentGoal.IsDone())
            {
                StartNextGoal();
            }
        }
        else
            Complete();
    }

    private Goal GetNextGoal()
    {
        foreach (Goal goal in goals)
        {
            if (!goal.IsDone())
                return goal;
        }
        return null;
    }

    private void StartNextGoal()
    {
        currentGoal = GetNextGoal();
        if (currentGoal != null)
            currentGoal.OnStart();
    }
}
