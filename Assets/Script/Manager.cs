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
        public int maxpower;
        public int minpower;

    }

    public vibration[] status;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 1に移動
        if (Input.GetKey(KeyCode.V))
        {
            SceneManager.LoadScene("Blocks1");
            Debug.Log("Blocks1を読み込みました。");
        }
        // 2に移動
        if (Input.GetKey(KeyCode.B))
        {
            SceneManager.LoadScene("Blocks2");
            Debug.Log("Blocks2を読み込みました。");
        }
        // 3に移動
        if (Input.GetKey(KeyCode.N))
        {
            SceneManager.LoadScene("Blocks3");
            Debug.Log("Blocks3を読み込みました。");
        }
        // 4に移動
        if (Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene("Blocks4");
            Debug.Log("Blocks4を読み込みました。");
        }
    }
}
