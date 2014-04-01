using UnityEngine;
using System.Collections;

public class MouseOperation : MonoBehaviour {

    bool isPulled = false; // 引っ張りの操作中かどうか
	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック開始
        {
            Debug.Log("Mouse 0 Down Start");
            this.isPulled = true;
        }

        if (this.isPulled) // 引っ張り操作中なら
        {
            if (Input.GetMouseButton(0)) // 左クリック中
            {
                Debug.Log("Mouse 0 Down");

            }

            if (Input.GetMouseButtonUp(0)) // 左クリック終了
            {
                Debug.Log("Mouse 0 Up");
                this.isPulled = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1)) // 右クリックしたなら
            {
                Debug.Log("Mouse 1 Down");
            }
        }
	}
}
