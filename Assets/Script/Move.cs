using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // transformを取得
        Transform myTransform = this.transform;

        // ワールド座標を基準に、座標を取得
        Vector3 worldPos = myTransform.position;
        float x = worldPos.x;    // ワールド座標を基準にした、x座標が入っている変数
        float y = worldPos.y;    // ワールド座標を基準にした、y座標が入っている変数
        float z = worldPos.z;    // ワールド座標を基準にした、z座標が入っている変数


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = object1.transform.position;
            transform.rotation= object1.transform.rotation;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = object2.transform.position;
            transform.rotation = object2.transform.rotation;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = object3.transform.position;
            transform.rotation = object3.transform.rotation;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = object4.transform.position;
            transform.rotation = object4.transform.rotation;
        }
    }
}
