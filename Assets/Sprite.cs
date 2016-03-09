using UnityEngine;
using System.Collections;
using System;

public class Sprite : MonoBehaviour
{

    public UnityEngine.Sprite Central2;
    public UnityEngine.Sprite Central;
    public UnityEngine.Sprite Peripherique;
    public UnityEngine.Sprite Peripherique2;
    public UnityEngine.Sprite Flou;
    public UnityEngine.Sprite None;

    int count;
    // Use this for initialization
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            count++;
            switch (count)
            {
                case 1:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Central2;
                    break ;
                case 2:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Central;
                    break;
                case 3:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Peripherique;
                    break;
                case 4:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Peripherique2;
                    break;
                case 5:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = Flou;
                    break;
                case 6:
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = None;
                    count = 0;
                    break;
            }
            
        }

    }


    public void ChangeSprite(string name)
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Central2;
    }

}

