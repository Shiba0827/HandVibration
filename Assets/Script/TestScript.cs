using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;


public class TestScript : MonoBehaviour
{
    public float X = 0f;
    public float Y = 0f;
   

    private Vector2 tippoint;
    private Vector2 afterpoint;


    public float span =1f;
    public float vibrationspan;

    [Range(0, 200)]
    public int check;

    public int checkcount=0;
    Collider myCollider;

    IEnumerator Vibration;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Logging");
        StartCoroutine("CountTime");
     


        myCollider = this.GetComponent<BoxCollider>();
        UduinoManager.Instance.pinMode(11, PinMode.PWM);

        Vibration = vibration_pattern();

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
            StartCoroutine(Vibration);
            checkcount++;
        }
        else
        {
            
            myCollider.enabled = false;
            StopCoroutine(Vibration);
        }

        if(Input.GetMouseButton(0))
        {
            UduinoManager.Instance.analogWrite(11, 0);
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

  

    IEnumerator vibration_pattern()
    {


        while (true)
        {
            UduinoManager.Instance.analogWrite(11, 0);
            yield return new WaitForSeconds(vibrationspan);
            Debug.LogFormat("{0}秒経過", vibrationspan);
            UduinoManager.Instance.analogWrite(11, 255);
            yield return new WaitForSeconds(vibrationspan);
            if (100 < vibrationspan) break;
        }

    }

}
