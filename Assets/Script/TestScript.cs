using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;


public class TestScript : MonoBehaviour
{
    public float X = 0f;
    public float Y = 0f;
    private float tippointX = 0f;
    private float tippointY = 0f;
    private float afterpointX = 0f;
    private float afterpointY = 0f;

    private Vector2 tippoint;
    private Vector2 afterpoint;
    private Vector2 difference;

    public float span =1f;

    [Range(0, 200)]
    public int check;

    public int checkcount=0;
    Collider myCollider;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Logging");
        StartCoroutine("CountTime");
        StartCoroutine("StopTime");



        myCollider = this.GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
         X= UduinoManager.Instance.analogRead(AnalogPin.A3);
        Y = UduinoManager.Instance.analogRead(AnalogPin.A4);



        //Debug.Log(Vector2.Distance(tippoint, afterpoint));

        if(Vector2.Distance(tippoint, afterpoint) >= check)
        {
           Debug.Log("傾きの差は"+ check +"以上です。");
            myCollider.enabled = true;
            checkcount++;
        }
        else
        {
            
            myCollider.enabled = false;
        }

    }

    IEnumerator Logging()
    {
        while (true)
        {
            yield return null;
            Vector2 pos1 = new Vector2(X, Y);
            tippoint = pos1;


        }
    }

    IEnumerator CountTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(span);
            Vector2 pos2 = new Vector2(X, Y);
            afterpoint = pos2;
            

        }
    }

    IEnumerator StopTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(20);
            Debug.Break();


        }
    }


}
