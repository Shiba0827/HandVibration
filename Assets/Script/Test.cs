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
    IEnumerator Vibration2;

    public AudioClip audioClip1;
    private AudioSource audioSource;





    // Start is called before the first frame update
    void Start()
    {

        audioSource = gameObject.GetComponent<AudioSource>();

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
        Vibration2 = vibration_pattern2();




    }

    // Update is called once per frame
    void Update()
    {
        ValueX = UduinoManager.Instance.analogRead(AnalogPin.A3);
        ValueY = UduinoManager.Instance.analogRead(AnalogPin.A4);

        //傾きの条件分岐
        if (Vector2.Distance(tippoint, afterpoint) >= check)
        {
            //Debug.Log("傾きの差は" + check + "以上です。");
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
            //testpower = Mathf.FloorToInt(Vector2.Distance(tippoint, afterpoint));

            testpower = Mathf.FloorToInt(Vector2.Distance(tippoint, afterpoint));
            this.audioSource.volume = testpower / 1000;

            UduinoManager.Instance.analogWrite(11, minPower);
            yield return new WaitForSeconds(span1);
            Debug.Log("0.2秒待つ");


            UduinoManager.Instance.analogWrite(11, maxPower);
            yield return new WaitForSeconds(span2);
            Debug.Log("0.5秒待つ");

            UduinoManager.Instance.analogWrite(11, minPower);
            yield return new WaitForSeconds(span1);
            Debug.Log("0.2秒待つ");


        }

    }


    //振動パターンのコルーチン
    IEnumerator vibration_pattern2()
    {
        while (true)
        {
            //testpower = Mathf.FloorToInt(Vector2.Distance(tippoint, afterpoint));

            testpower = Mathf.FloorToInt(Vector2.Distance(tippoint, afterpoint));
            this.audioSource.volume = testpower / 1000;

           
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Vibration1);
        Debug.Log("触れている");
        audioSource.Play();
    }


    private void OnTriggerStay(Collider other)
    {


    }

    private void OnTriggerExit(Collider other)
    {
        // StopCoroutine(Vibration1);
        StopCoroutine(Vibration1);
        UduinoManager.Instance.analogWrite(11, 0);
        audioSource.Stop();
    }
}
