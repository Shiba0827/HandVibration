using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class Test : MonoBehaviour
{
    public int maxPower, minPower;

    public float ValueX = 0f;
    public float ValueY = 0f;


    private Vector2 tippoint;
    private Vector2 afterpoint;

    public float span1, span2;

    [Range(0, 200)]
    public int check;

    public int checkcount = 0;
    Collider myCollider;

    UduinoManager manager;


    private float testpower;

    IEnumerator Vibration1;
    private AudioSource sound01;
    private AudioSource sound02;
    private AudioSource sound03;


    // Start is called before the first frame update
    void Start()
    {

        //コライダーの定義
        myCollider = this.GetComponent<BoxCollider>();

        //振動モーターの定義
        UduinoManager.Instance.pinMode(11, PinMode.PWM);
        UduinoManager.Instance.pinMode(12, PinMode.PWM);

        //傾きセンサーコルーチンの開始 
        StartCoroutine("every_1_frame");
        StartCoroutine("every_1_seconds");

        //振動パターンコルーチンの変数定義
        Vibration1 = vibration_pattern1();

        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];
        sound03 = audioSources[2];

    }

    // Update is called once per frame
    void Update()
    {
        ValueX = UduinoManager.Instance.analogRead(AnalogPin.A3);
        ValueY = UduinoManager.Instance.analogRead(AnalogPin.A4);

        //傾きの条件分岐
        if (Vector2.Distance(tippoint, afterpoint) >= check)
        {

            myCollider.enabled = true;

            checkcount++;
        }
        else
        {
          myCollider.enabled = false;
            UduinoManager.Instance.analogWrite(12, 0);
            StopCoroutine(Vibration1);
        }

        //モーター非常停止用のボタン
        if (Input.GetMouseButton(0))
        {
            UduinoManager.Instance.analogWrite(11, 0);
            StopCoroutine(Vibration1);
        }



    }

    //1フレーム毎に傾きの座標を取得するコルーチン
    IEnumerator every_1_frame()
    {
        while (true)
        {
            yield return null;
            Vector2 pos1 = new Vector2(ValueX, ValueY);
            tippoint = pos1;
        }
    }

    //1秒毎に傾きの座標を取得するコルーチン
    IEnumerator every_1_seconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Vector2 pos2 = new Vector2(ValueX, ValueY);
            afterpoint = pos2;
        }
    }

    //振動パターンのコルーチン
    IEnumerator vibration_pattern1()
    {
        while (true)
        {
  

            testpower = Mathf.FloorToInt(Vector2.Distance(tippoint, afterpoint));
           /*
            if (testpower - check <= 20){
                sound01.PlayOneShot(sound01.clip);
                Debug.Log("Clip1");
            }
            else if (testpower - check <= 50) {
                sound02.PlayOneShot(sound02.clip);
                Debug.Log("Clip2");
            }
            else if(testpower - check <= 20){
                sound03.PlayOneShot(sound03.clip);
                Debug.Log("Clip3");
            }*/
            

            UduinoManager.Instance.analogWrite(11, minPower);
            yield return new WaitForSeconds(span1);
            Debug.Log(span1+"待つ");


            UduinoManager.Instance.analogWrite(11, maxPower);
            yield return new WaitForSeconds(span2);
            Debug.Log(span2+"待つ");

            UduinoManager.Instance.analogWrite(11, minPower);
            yield return new WaitForSeconds(span1);
            Debug.Log(span1+"待つ");


        }

    }


   


    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Vibration1);
        if (testpower - check <= 20)
        {
            sound01.PlayOneShot(sound01.clip);
            Debug.Log("Clip1");
        }
        else if (testpower - check <= 50)
        {
            sound02.PlayOneShot(sound02.clip);
            Debug.Log("Clip2");
        }
        else if (testpower - check <= 20)
        {
            sound03.PlayOneShot(sound03.clip);
            Debug.Log("Clip3");
        }

        Debug.Log("触れている");

    }


    private void OnTriggerStay(Collider other)
    {
       

    }

    private void OnTriggerExit(Collider other)
    {

        StopCoroutine(Vibration1);
        UduinoManager.Instance.analogWrite(11, 0);
        sound01.Stop();
        sound02.Stop();
        sound03.Stop();

    }
}
