using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class Test2 : MonoBehaviour{

    public int maxPower, minPower, PinModeCount1, PinModeCount;//Arduinoに与える力と接続ピンの定義

    public GameObject Manager;//オブジェクトマネージャーそのものが入る変数の定義
    Manager script; //管理スクリプトの定義

    public int vibrationCount; //どのパターンかを決めるカウントの変数の定義

    public float ValueX = 0f;
    public float ValueY = 0f;


    private Vector2 tippoint;
    private Vector2 afterpoint;


    public float span = 1f;
    public float span1,span2;

    [Range(0, 200)]
    public int check;

    public int checkcount = 0;
    Collider myCollider;

    UduinoManager manager;

    

    [SerializeField]
    private float testpower;

    public int checktime=0;

    IEnumerator Vibration1;
    IEnumerator Vibration2;

    public AudioClip audioClip1;
    private AudioSource audioSource;


    


    // Start is called before the first frame update
    void Start(){

        audioSource = gameObject.GetComponent<AudioSource>();

        //コライダーの定義
        myCollider = this.GetComponent<BoxCollider>();

        //振動モーターの定義
        UduinoManager.Instance.pinMode(11, PinMode.PWM);
        UduinoManager.Instance.pinMode(12, PinMode.PWM);

        Manager = GameObject.Find("Manager");
        script = Manager.GetComponent<Manager>();

       

       /* Debug.Log("このオブジェクトの振動の強さはの最低は" + minPower + "です");
        Debug.Log("このオブジェクトの振動の強さはの最大は" + maxPower + "です");
        Debug.Log("このオブジェクトの振動の最低振動感覚は" + span1 + "です");
        Debug.Log("このオブジェクトの振動の最大振動感覚は" + span2 + "です");
        */

        //傾きセンサーコルーチンの開始
        StartCoroutine("every_1_frame");
        StartCoroutine("every_1_seconds");

        //振動パターンコルーチンの変数定義
        Vibration1 = vibration_pattern1();
        Vibration2 = vibration_pattern2();

        


    }

    // Update is called once per frame
    void Update(){
        ValueX = UduinoManager.Instance.analogRead(AnalogPin.A3);
        ValueY = UduinoManager.Instance.analogRead(AnalogPin.A4);

        //傾きの条件分岐
        if (Vector2.Distance(tippoint, afterpoint) >= check){
            //Debug.Log("傾きの差は" + check + "以上です。");
            myCollider.enabled = true;
            
            checkcount++;
        }
        else{
            myCollider.enabled = false;
            UduinoManager.Instance.analogWrite(12, 0);
            StopCoroutine(Vibration1);
        }

        //モーター非常停止用のボタン
        if (Input.GetMouseButton(0)){
            UduinoManager.Instance.analogWrite(11, 0);
        }

        

    }

    //1フレーム毎に傾きの座標を取得するコルーチン
    IEnumerator every_1_frame(){
        while (true){
            yield return null;
            Vector2 pos1 = new Vector2(ValueX, ValueY);
            tippoint = pos1;
        }
    }

    //1秒毎に傾きの座標を取得するコルーチン
    IEnumerator every_1_seconds(){
        while (true){
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

            yield return new WaitForSeconds(span1);

            UduinoManager.Instance.analogWrite(11, minPower);
            yield return null;

            UduinoManager.Instance.analogWrite(11, maxPower);
            yield return null;

            yield return new WaitForSeconds(span1);
            yield return null;

            UduinoManager.Instance.analogWrite(11, minPower);
            yield return null;
            yield return new WaitForSeconds(span1);
            yield return null;

            UduinoManager.Instance.analogWrite(11,0);
            yield return null;


            yield return null;

      

        }

    }


    //振動パターンのコルーチン
    IEnumerator vibration_pattern2()
    {
        while (true)
        {
            //testpower = Mathf.FloorToInt(Vector2.Distance(tippoint, afterpoint));

            UduinoManager.Instance.analogWrite(11, 0);

            yield return new WaitForSeconds(1.0f);

            UduinoManager.Instance.analogWrite(11, minPower);

            yield return new WaitForSeconds(1.0f);

            yield return null;



        }

    }



    /*

    //振動パターンのコルーチン
    IEnumerator vibration_pattern2(){
        while (true){
            
            
            UduinoManager.Instance.analogWrite(12, 0);
           testpower = Mathf.FloorToInt(Vector2.Distance(tippoint, afterpoint));
            Debug.Log("testpowerは" + testpower + "{0}秒経過" + span2);

            this.audioSource.volume = testpower;

            UduinoManager.Instance.analogWrite(12, 255);
  
            yield return null;
            
            if(checktime<=span2)
            {
                checktime++;
            }
            else
            {
                Debug.Log("動作している");
                StopCoroutine(Vibration2);
                UduinoManager.Instance.analogWrite(12, 0);
            }
               
        }

    }

    */


    private void OnTriggerEnter(Collider other){
       StartCoroutine(Vibration1);
       audioSource.Play();
    }


    private void OnTriggerStay(Collider other){
       

    }

    private void OnTriggerExit(Collider other){
       // StopCoroutine(Vibration1);
        StopCoroutine(Vibration1);
        UduinoManager.Instance.analogWrite(11, 0);
        audioSource.Stop();
    }

}
