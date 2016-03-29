using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InfoText : MonoBehaviour
{
    public float Speed = 1;

    Queue<string> infoQueue;
    string actual = "";
    bool showing = false;

    Text text;

    // Use this for initialization
    void Start()
    {
        infoQueue = new Queue<string>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!showing && infoQueue.Count > 0)
            StartShow();
        if (showing)
            ExecuteShow();
    }

    public void AddNews(string t)
    {
        infoQueue.Enqueue(t);
    }

    public void ClearNews()
    {
        infoQueue = new Queue<string>();
    }

    void StartShow()
    {
        actual = infoQueue.Dequeue();
        showing = true;
        text.text = actual;
        transform.position = new Vector2(Screen.width + text.preferredWidth / 2, transform.position.y);
    }

    void ExecuteShow()
    {
        float end = -text.preferredWidth / 2;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(end, transform.position.y), Speed * Time.deltaTime);
        if (transform.position.x <= end + 0.1f)
            EndShow();
    }

    void EndShow()
    {
        actual = "";
        text.text = actual;
        showing = false;
    }
}
