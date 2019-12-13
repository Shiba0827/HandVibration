using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class Test2 : MonoBehaviour{

    public int maxPower, minPower, PinModeCount1, PinModeCount;//Arduinoに与える力と接続ピンの定義

    public GameObject Manager;//オブジェクトマネージャーそのものが入る変数の定義
    public int vibrationCount; //どのパターンかを決めるカウントの変数の定義

    public float ValueX = 0f;
    public float ValueY = 0f;


    private Vector2 tippoint;
    private Vector2 afterpoint;


    public float span = 1f;
    public float vibrationspan;

    [Range(0, 200)]
    public int check;

    public int checkcount = 0;
    Collider myCollider;

    UduinoManager manager;

    Manager script; //管理スクリプトの定義

    [SerializeField]
    private int testpower;

    public int checktime;

    IEnumerator Vibration;

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

        //傾きセンサーコルーチンの開始
        StartCoroutine("every_1_frame");
        StartCoroutine("every_1_seconds");

        //振動パターンコルーチンの変数定義
        Vibration = vibration_pattern();


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
            //StopCoroutine(Vibration);
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
            yield return new WaitForSeconds(2);
            Vector2 pos2 = new Vector2(ValueX, ValueY);
            afterpoint = pos2;
        }
    }



    //振動パターンのコルーチン
    IEnumerator vibration_pattern(){
        while (true){
            
            
            UduinoManager.Instance.analogWrite(11, 0);
            UduinoManager.Instance.analogWrite(12, 0);
           testpower = Mathf.FloorToInt(Vector2.Distance(tippoint, afterpoint));
            Debug.Log("testpowerは" + testpower + "{0}秒経過" + vibrationspan);

            this.audioSource.volume = testpower;

            UduinoManager.Instance.analogWrite(11, 255);
            // UduinoManager.Instance.analogWrite(12, testpower);
            yield return null;
            
            if(checktime<=vibrationspan)
            {
                checktime++;
            }
            else
            {
                StopCoroutine(Vibration);
                UduinoManager.Instance.analogWrite(11, 0);
            }
               
        }

    }

    private void OnTriggerEnter(Collider other){
        //StartCoroutine(Vibration);
        audioSource.Play();
    }


    private void OnTriggerStay(Collider other){
        StartCoroutine(Vibration);
        
    }

    private void OnTriggerExit(Collider other){
        StopCoroutine(Vibration);
        UduinoManager.Instance.analogWrite(11, 0);
        audioSource.Stop();
    }

}
