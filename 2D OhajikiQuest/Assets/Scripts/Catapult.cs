using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

    public float speed = 5.0f;
    bool isFixed = false;

	void Start () 
    {
        ChangeFixedCatapult();
	}
	
	void Update () 
    {
        if (this.isFixed) // カタパルトが固定されているなら
        {
            MoveCatapult();
        }
	}

    void MoveCatapult()
    {
        float xAxis = Input.GetAxis("Horizontal");
        if (Mathf.Sign(xAxis) < 0)
        {
            if (transform.position.x < -1.5f)
            {
                xAxis = 0;
            }
        }
        else if (Mathf.Sign(xAxis) > 0)
        {
            if (transform.position.x > 1.5f)
            {
                xAxis = 0;
            }
        }
        transform.Translate(Vector2.right * xAxis * this.speed * Time.deltaTime);
    }

    void ChangeFixedCatapult()
    {
        if (isFixed)
        {
            this.isFixed = false;
        }
        else
        {
            this.isFixed = true;
        }
    }
}
