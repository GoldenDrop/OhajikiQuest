using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

    public float speed = 5.0f;
    public GameObject ball_P;
    GameObject ball;
    bool isFixed = false;

	void Start () 
    {
        CreateBall();
	}
	
	void Update () 
    {
        if (!this.isFixed) // カタパルトが固定さていないなら
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

    void CreateBall()
    {
        // 子としてBall生成
        this.ball = Instantiate(this.ball_P, new Vector2(0, -2.5f), Quaternion.identity) as GameObject;
        this.ball.transform.parent = gameObject.transform;
    }
}
