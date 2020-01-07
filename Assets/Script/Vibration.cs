using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class Vibration : MonoBehaviour
{
    IEnumerator Vibration1;

    [Range(0, 200)]
    public int test;


    private AudioSource sound01;
    private AudioSource sound02;
    private AudioSource sound03;

    // Start is called before the first frame update
    void Start()
    {
        Vibration1 = vibration_pattern1();
        UduinoManager.Instance.pinMode(11, PinMode.PWM);
        AudioSource[] audioSources = GetComponents<AudioSource>();
        sound01 = audioSources[0];
        sound02 = audioSources[1];
        sound03 = audioSources[2];

    }

    // Update is called once per frame
    void Update()
    {
       

        if(Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Vibration1);
            if (test <= 100)
            {
                 sound01.PlayOneShot(sound01.clip);

                Debug.Log("Clip1");
            }
            else if (test <= 200)
            {
                sound02.PlayOneShot(sound02.clip);
                Debug.Log("Clip2");
            }
            else if (test <= 250)
            {
                sound03.PlayOneShot(sound03.clip);
                Debug.Log("Clip3");
            }
        }
        else
        {
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(Vibration1);
            sound01.Stop();
            sound02.Stop();
            sound03.Stop();
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
