using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using System.Collections.Generic;

public class Uduino_Vibration : MonoBehaviour
{
    public int maxPower;
    public int minPower;


    public int PinModeCount1;
    public int PinModeCount2;
    public GameObject cube;

    public GameObject Manager;//オブジェクトマネージャーそのものが入る変数の定義
    public int vibrationCount; //どのパターンかを決めるカウントの変数の定義


    private int valueX, valueY, valueZ;

    private int vibration;
    UduinoManager manager;

    Collider myCollider;

    Manager script; //管理スクリプトの定義


    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("Manager");
        script = Manager.GetComponent<Manager>();

        myCollider = this.GetComponent<BoxCollider>();

        UduinoManager.Instance.pinMode(PinModeCount1, PinMode.PWM);
        UduinoManager.Instance.pinMode(PinModeCount2, PinMode.PWM);
        Debug.Log("テストスクリプトスタートしました");

        //アナログ値を読み込む
        /*
        manager.pinMode(AnalogPin.A3, PinMode.Input);
        manager.pinMode(AnalogPin.A4, PinMode.Input);
        manager.pinMode(AnalogPin.A5, PinMode.Input);
        */


    }

    // Update is called once per frame
    void Update()
    {
        //傾きセンサーのアナログ値をX,Y,Zに代入
        valueX = UduinoManager.Instance.analogRead(AnalogPin.A3);
        valueY = UduinoManager.Instance.analogRead(AnalogPin.A4);
        valueZ = UduinoManager.Instance.analogRead(AnalogPin.A5);

        maxPower = script.status[vibrationCount].maxpower; //最大値の代入
        minPower = script.status[vibrationCount].minpower; //最小値の代入



        if (valueY <= 500)
        {
            Debug.Log("Yの値は550以下");
            myCollider.enabled = true;
        }
        else
        {
            Debug.Log("どちらにも当てはまらない");
            
            myCollider.enabled = false;
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("当たっている");
    }


    private void OnTriggerStay(Collider other)
    {

        Debug.Log("当たっている");

        UduinoManager.Instance.analogWrite(PinModeCount1, 150);
        UduinoManager.Instance.analogWrite(PinModeCount2, 150);

        UduinoManager.Instance.analogWrite(PinModeCount1, minPower);
        UduinoManager.Instance.analogWrite(PinModeCount2, maxPower);

        UduinoManager.Instance.analogWrite(PinModeCount1, minPower);
        UduinoManager.Instance.analogWrite(PinModeCount2, maxPower);

        UduinoManager.Instance.analogWrite(PinModeCount1, 150);
        UduinoManager.Instance.analogWrite(PinModeCount2, 150);

        UduinoManager.Instance.analogWrite(PinModeCount1, minPower);
        UduinoManager.Instance.analogWrite(PinModeCount2, maxPower);

        UduinoManager.Instance.analogWrite(PinModeCount1, minPower);
        UduinoManager.Instance.analogWrite(PinModeCount2, maxPower);

        UduinoManager.Instance.analogWrite(PinModeCount1, 150);
        UduinoManager.Instance.analogWrite(PinModeCount2, 150);

        UduinoManager.Instance.analogWrite(PinModeCount1, 0);
        UduinoManager.Instance.analogWrite(PinModeCount2, 0);



    }

    private void OnTriggerExit(Collider other)
    {

        UduinoManager.Instance.analogWrite(PinModeCount1, 0);
        UduinoManager.Instance.analogWrite(PinModeCount2, 0);
        Debug.Log("触ってない");
    }



}
