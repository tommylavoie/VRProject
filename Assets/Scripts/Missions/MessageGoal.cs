using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MessageGoal : Goal
{
    public List<string> Messages;
    public InfoText infoText;

    protected override void Begin()
    {
        base.Begin();
        foreach (string message in Messages)
        {
            infoText.AddNews(message);
        }
    }
}
