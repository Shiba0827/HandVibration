using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class TestScript2 : MonoBehaviour
{
    public float vibrationspan;

    IEnumerator Vibration;

    Collider myCollider;

    // Start is called before the first frame update
    void Start()
    {
        UduinoManager.Instance.pinMode(11, PinMode.PWM);
        //StartCoroutine("vibration_pattern");

        myCollider = this.GetComponent<BoxCollider>();

        Vibration = vibration_pattern();
       

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Vibration);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            StopCoroutine(Vibration);
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
