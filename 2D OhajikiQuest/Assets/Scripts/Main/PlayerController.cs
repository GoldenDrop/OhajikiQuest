using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    public float speed = 5.0f;
    public GameObject playerPrefab;
    GameObject playerOrb;
    GameObject magicPowerBar;
    Transform yusha;
    Transform infomation;
    GetPhase getPhase;

    bool isMoving = false;
    bool isPulled = false;
    bool isRecites = false;

    Vector2 firstPoint;
    Vector2 mousePoint;
    Vector2 deltaPoint;

    public float limitAngle = 30.0f;
    float minAngle;
    float maxAngle;
    public float minLength = 0.5f;
    public float maxLength = 3.5f;

    // フェイズ 0:待機 1:Player 2:敵 
    int phase = 0;


    void Start()
    {
        this.getPhase = gameObject.GetComponent<GetPhase>();
        this.magicPowerBar = GameObject.FindWithTag("MagicPowerBar");
        this.infomation = gameObject.transform.Find("Infomation");
        CreatePlayer();
        this.yusha = gameObject.transform.Find("PlayerOrb(Clone)/Yusha");
        

        this.minAngle = -90 + limitAngle;
        this.maxAngle = 90 - limitAngle;
    }

    void Update()
    {
        this.phase = this.getPhase.GetNowPhase();
        if (this.phase == 1) //Playerフェイズ
        {
            if (!this.isMoving) // Playerが動いてないなら
            {
                if (!this.isPulled) // 引っ張り動作中でないなら
                {
                    StartPull();
                }
                else // 引っ張り動作中
                {
                    if (Input.GetMouseButtonDown(1)) // 右クリックしたなら
                    {
                        Debug.Log("Mouse Right Button Down");
                        this.magicPowerBar.SendMessage("ReleasedPower");
                        float turnAngle = 0;
                        this.infomation.SendMessage("Turn", turnAngle);
                        float infoAlpha = 0;
                        this.infomation.SendMessage("ChangeTransparency", infoAlpha);
                        InitializedPlayer();
                    }
                    Pull();
                    EndPull();
                }
            }
        }
    }

    void StartPull()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリック開始
        {
            Debug.Log("Mouse Left Button Down Start");
            this.isPulled = true;
            this.firstPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("firstPoint(" + this.firstPoint.x + ", " + this.firstPoint.y + ")");
            // Recitesアニメーション開始
            this.yusha.SendMessage("Recites", true);
            // PlayerOrb表示開始
            float orbAlpha = 0.4f;
            this.playerOrb.SendMessage("ChangeTransparency", orbAlpha);
            // Infomation 表示　
            float infoAlpha = 1.0f;
            this.infomation.SendMessage("ChangeTransparency", infoAlpha);
        }
    }

    void Pull()
    {
        if (Input.GetMouseButton(0)) // 左クリック中
        {
            Debug.Log("Mouse Left Button Down");
            this.mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.deltaPoint = this.firstPoint - this.mousePoint;
            //Debug.Log("mousePoint(" + this.mousePoint.x + ", " + this.mousePoint.y + ")");
            //Debug.Log("deltaPoint(" + this.deltaPoint.x + ", " + this.deltaPoint.y + ")");
            float turnAngle = GetAngle(this.deltaPoint);   // 先に角度を計算する
            this.infomation.SendMessage("Turn", turnAngle);
            float magicPower = GetMagnitude(this.deltaPoint) / (this.maxLength - this.minLength);
            //Debug.Log("MagicPower : " + magicPower);
            this.magicPowerBar.SendMessage("ChangeMagicPower", magicPower);// MagicPowerBarに値を送る
        }
    }

    void EndPull()
    {
        if (Input.GetMouseButtonUp(0)) // 左クリック終了
        {
            Debug.Log("Mouse Left Button Up");
            this.isPulled = false;
            this.isMoving = true;
            float infoAlpha = 0;
            this.infomation.SendMessage("ChangeTransparency", infoAlpha);
            // Standアニメーション開始
            this.yusha.SendMessage("Stand", true);
            this.mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.deltaPoint = this.firstPoint - this.mousePoint;
            Vector2 power = GetDirection(this.deltaPoint) * GetMagnitude(this.deltaPoint); //  方向 * 長さ
            this.playerOrb.SendMessage("AddForceToOrb", power);
            this.magicPowerBar.SendMessage("ReleasedPower");
        }
    }
    

    void CreatePlayer()
    {
        // 子としてPlayer生成
        this.playerOrb = Instantiate(this.playerPrefab, new Vector2(10.0f, -3.5f), Quaternion.identity) as GameObject;
        this.playerOrb.transform.parent = gameObject.transform;
    }

    void InitializedPlayer()
    {
        Debug.Log("Initialization Player");
        this.playerOrb.SendMessage("MoveToFirstPosition");
        this.yusha.SendMessage("OffFlags");
        this.isMoving = false;
        this.isPulled = false;
    }



    float GetAngle(Vector2 delta)
    {
        float angle = Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg * -1;

        // 角度制限
        if (angle < this.minAngle)
        {
            angle = this.minAngle;
        }
        else if (angle > this.maxAngle)
        {
            angle = this.maxAngle;
        }
        Debug.Log("(GetAngle) angle = " + angle + "°");
        return angle;
    }

    // ベクトルの長さ
    float GetMagnitude(Vector2 delta)
    {
        float magnitude = delta.magnitude; // 引っ張りの長さを取得
        // 長さ制限
        if (magnitude < this.minLength)
        {
            magnitude = this.minLength;
        }
        else if (magnitude > this.maxLength)
        {
            magnitude = this.maxLength;
        }
        Debug.Log("GetMagnitude magnitude : " + magnitude);
        return magnitude;
    }

    Vector2 GetDirection(Vector2 delta)
    {
        Vector2 direction = delta.normalized; // ベクトルの長さを1にする
        float angle = GetAngle(delta);

        // 角度制限
        if (angle == this.minAngle)
        {
            // 与えるベクトルをminAngleのベクトルに変更する 計算上長さは1
            direction = new Vector2(Mathf.Cos(this.limitAngle * Mathf.Deg2Rad), Mathf.Sin(this.limitAngle * Mathf.Deg2Rad));
        }
        if (angle == this.maxAngle)
        {
            // 与えるベクトルをmaxAngleのベクトルに変更する　計算上長さは1
            direction = new Vector2(-Mathf.Cos(this.limitAngle * Mathf.Deg2Rad), Mathf.Sin(this.limitAngle * Mathf.Deg2Rad));
        }
        if (angle == 0)
        {
            // 与えるベクトルを(1, 0)に変更する
            direction = Vector2.up;
        }
        
        return direction;
    }
}
