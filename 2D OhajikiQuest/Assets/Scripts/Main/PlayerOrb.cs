using UnityEngine;
using System.Collections;

public class PlayerOrb : MonoBehaviour {

    public int FORCE = 500;

    GameObject phaseController;
    SpriteRenderer orbRenderer;
    CircleCollider2D circleCollider;
    Color orbColor;
    bool isMoving = false;
    bool isStoped = false;
    bool isClear  = false;

    float moveTimer;
    float moveInterval = 3.0f;
    float waitTimer;
    float waitInterval = 1.5f;
    float angle;
    public float limitAngle = 30.0f;
    float minAngle;
    float maxAngle;
    public float minLength = 0.5f;
    public float maxLength = 3.5f;
    Quaternion rotation;
    Transform yusha;

    

    void Start()
    {
        this.orbRenderer    = GetComponent<SpriteRenderer>();
        this.circleCollider = GetComponent<CircleCollider2D>();

        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.moveTimer = this.moveInterval;
        this.waitTimer = this.waitInterval;
        this.minAngle = -90 + limitAngle;
        this.maxAngle = 90 - limitAngle;
        ChangeTransparency(0);
        this.yusha = gameObject.transform.Find("Yusha");
        
        
    }

    void Update()
    {
        if (this.isMoving)
        {
            this.moveTimer -= Time.deltaTime;
            float velX = gameObject.rigidbody2D.velocity.x;
            float velY = gameObject.rigidbody2D.velocity.y;
            gameObject.rigidbody2D.velocity = new Vector2(velX * 0.99f, velY * 0.99f);

            if (this.moveTimer < 0)
            {
                gameObject.rigidbody2D.velocity = Vector2.zero;
                if (!this.isStoped)
                {
                    //gameObject.rigidbody2D.velocity = Vector2.zero;
                    gameObject.rigidbody2D.fixedAngle = true;
                    gameObject.rigidbody2D.fixedAngle = false;
                    this.circleCollider.isTrigger = true;
                    this.rotation.eulerAngles = Vector3.zero;
                    transform.rotation = rotation;
                    ChangeTransparency(0);
                    // Downアニメーション開始
                    this.yusha.SendMessage("Down", true);
                    this.isStoped = true;
                }
                else
                {
                    // アニメーションが変わるまで待機
                    this.waitTimer -= Time.deltaTime;
                    //Debug.Log("waitTimer : " + waitTimer + ", " + Time.deltaTime);
                    if (this.waitTimer < 0)
                    {
                        if (!isClear)
                        {
                            this.phaseController.SendMessage("SetPhase", 2);
                        }
                        this.waitTimer = this.waitInterval;
                        this.moveTimer = this.moveInterval;
                        this.isMoving = false;
                        this.isStoped = false;
                        this.isClear = false;
                    }
                }
            }
        }
    }

    void AddForceToOrb(Vector2 power)
    {
        Debug.Log("AddForceToOrb");
        gameObject.rigidbody2D.AddForce(power * FORCE);
        this.isMoving = true;
    }

    void ChangeTransparency(float alpha) // 透明度変更
    {
        Color orbColor = new Color(1, 1, 1, alpha);
        this.orbRenderer.color = orbColor;
    }

    void MoveToFirstPosition()
    {
        transform.localPosition = Vector2.zero;
        this.circleCollider.isTrigger = false;
    }

    void OnClearFlag()
    {
        this.isClear = true;
    }
}

