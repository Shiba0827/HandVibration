using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class Vibration : MonoBehaviour
{
    IEnumerator Vibration1;

    // Start is called before the first frame update
    void Start()
    {
        Vibration1 = vibration_pattern1();
        UduinoManager.Instance.pinMode(11, PinMode.PWM);

    }

    // Update is called once per frame
    void Update()
    {
       

        if(Input.GetMouseButton(1))
        {
            StartCoroutine(Vibration1);
        }

        if (Input.GetMouseButton(0))
        {
            StopCoroutine(Vibration1);
        }
    }

    //振動パターンのコルーチン
    IEnumerator vibration_pattern1()
    {
        while (true)
        {

           
                UduinoManager.Instance.analogWrite(11, 50);
                yield return new WaitForSeconds(0.4f);
            Debug.Log("0.2秒待つ");
          
                
                UduinoManager.Instance.analogWrite(11,60);
            yield return new WaitForSeconds(0.4f);
            Debug.Log("0.5秒待つ");

           

        }
    }
}
