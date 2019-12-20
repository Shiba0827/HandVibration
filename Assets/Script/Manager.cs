using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    [System.Serializable]
    public struct vibration
    {
        public string Name;
        public int maxpower,minpower;
        public float span1, span2;

        public GameObject Point1, Point2, Point3, Point4;
    }

    //public vibration[] status;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // 1に移動
        if (Input.GetKeyDown(KeyCode.V))
        {
            
         
            Debug.Log("Point1を読み込みました。");
        }
        // 2に移動
        if (Input.GetKeyDown(KeyCode.B))
        {
          
            Debug.Log("Point2を読み込みました。");
        }
        // 3に移動
        if (Input.GetKeyDown(KeyCode.N))
        {
          
            Debug.Log("Point3を読み込みました。");
        }
        // 4に移動
        if (Input.GetKeyDown(KeyCode.M))
        {
           
            Debug.Log("Point4を読み込みました。");
        }
    }
}
