using UnityEngine;
using System.Collections;

public class PlayerOrbController : MonoBehaviour {

    public float speed = 5.0f;
    public GameObject playerPrefab;
    GameObject playerOrb;
    GameObject magicCircle;
    Transform infomation;
    GetPhase getPhase;

    bool isMoving = false;
    bool isPulled = false;
    bool isRecites = false;

    Vector2 firstPoint;
    Vector2 mousePoint;
    Vector2 deltaPoint;


    // フェイズ 0:待機 1:勇者 2:敵 
    int phase = 0;
    void Start()
    {
        this.getPhase = gameObject.GetComponent<GetPhase>();
        this.magicCircle = GameObject.FindWithTag("MagicCircle");
        this.infomation = this.magicCircle.transform.Find("Infomation");
        CreatePlayer();
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
            // Recitesアニメーション開始 playerオブジェクトの子オブジェクトのYushaオブジェクトへアクセス

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
            this.infomation.SendMessage("GetAngle", this.deltaPoint);   // 先に角度を計算する
            this.infomation.SendMessage("Turn");
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
            this.mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.deltaPoint = this.firstPoint - this.mousePoint;
            this.playerOrb.SendMessage("GetAngle", this.deltaPoint);   // 先に角度を計算する
            this.playerOrb.SendMessage("AddForceToOrb", this.deltaPoint);
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
        this.isMoving = false;
        this.isPulled = false;
        //this.infomation.SendMessage("ChangeTransparency", 0);
        
    }
}
