using UnityEngine;
using System.Collections;

public class CatapultControl : MonoBehaviour {

    public float speed = 5.0f;
    public GameObject ball_P;
    GameObject ball;
    GameObject phaseControl;
    GameObject fixedIcon;
    PhaseControl phaseComponent;
    bool isFixedCatapult = false;
    bool isMovingBall    = false;
    bool isPulledBall    = false;

    Vector2 firstPoint;
    Vector2 mousePoint;
    Vector2 deltaPoint;



    // フェイズ 0:待機 1:勇者 2:敵 
    int phase = 0;
    void Start()
    {
        this.phaseControl = GameObject.FindWithTag("PhaseControl");
        this.fixedIcon    = GameObject.FindWithTag("FixedIcon");
        CreateBall();
    }

    void Update()
    {
        GetPhase();
        if (this.phase == 1) // 勇者フェイズ
        {
            if (!this.isMovingBall) // Ballが動いてないなら
            {
                if (!this.isPulledBall) // 引っ張り動作中でないなら
                {
                    if (Input.GetMouseButtonDown(1)) // 右クリックしたなら
                    {
                        Debug.Log("Mouse Right Button Down");
                        ChangeFixedCatapult();
                    }
                }

                if (this.isFixedCatapult) // Catapult固定中
                {
                    PullBall();
                }
                else
                {
                    MoveCatapult();
                }
            }
        }
    }

    void GetPhase()
    {
        this.phaseComponent = this.phaseControl.GetComponent<PhaseControl>();
        this.phase = this.phaseComponent.GetPhase();
    }

    void PullBall()
    {
        if (!this.isPulledBall) // 引っ張り動作中でないなら
        {
            if (Input.GetMouseButtonDown(0)) // 左クリック開始
            {
                Debug.Log("Mouse Left Button Down Start");
                this.firstPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("firstPoint(" + this.firstPoint.x + ", " + this.firstPoint.y + ")");
                this.isPulledBall = true;
            }
        }
        else
        {
            if (Input.GetMouseButton(0)) // 左クリック中
            {
                Debug.Log("Mouse Left Button Down");
                this.mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this.deltaPoint = this.firstPoint - this.mousePoint;
                Debug.Log("mousePoint(" + this.mousePoint.x + ", " + this.mousePoint.y + ")");
                Debug.Log("deltaPoint(" + this.deltaPoint.x + ", " + this.deltaPoint.y + ")");
                this.ball.SendMessage("Turn", this.deltaPoint);
            }

            if (Input.GetMouseButtonUp(0)) // 左クリック終了
            {
                Debug.Log("Mouse Left Button Up");
                this.mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                this.deltaPoint = this.firstPoint - this.mousePoint;
                this.ball.SendMessage("AddForceToBall", this.deltaPoint);
                this.isPulledBall = false;
                this.isMovingBall = true;
            }
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
        if (this.isFixedCatapult)
        {
            this.isFixedCatapult = false;
        }
        else
        {
            this.isFixedCatapult = true;
        }
        this.fixedIcon.SendMessage("ChangeText");
    }

    void CreateBall()
    {
        // 子としてBall生成
        this.ball = Instantiate(this.ball_P, new Vector2(0, -2.5f), Quaternion.identity) as GameObject;
        this.ball.transform.parent = gameObject.transform;
    }

    void InitializationCatapult()
    {
        Debug.Log("Initialization Catapult");
        CreateBall();
        this.isMovingBall = false;
        ChangeFixedCatapult();
    }
}
