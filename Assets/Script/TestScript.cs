using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;


public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //振動モーターの定義
        UduinoManager.Instance.pinMode(11, PinMode.PWM);

    }

    // Update is called once per frame
    void Update()
    {
        float T = 1.0f;
        float f = 2.0f / T;
        float sin1 = Mathf.Sin(1*Mathf.PI*f*Time.time);
        float sin2 = Mathf.Sin(100 * Mathf.PI * f * Time.time);

        int vibration1 = Mathf.FloorToInt(sin1);
        int vibration2 = Mathf.FloorToInt(sin2);
        UduinoManager.Instance.analogWrite(11, vibration1);
        UduinoManager.Instance.analogWrite(12, vibration2);

        Debug.Log(vibration1);
        Debug.Log(vibration2);
    }
}
