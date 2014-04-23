using UnityEngine;
using System.Collections;

public class PlayerOrb : MonoBehaviour {

    public int FORCE = 500;

    GameObject phaseControl;
    SpriteRenderer renderer;
    Color orbColor;
    bool isMoving = false;
    bool isStoped = false;
    float moveTimer;
    float moveInterval = 3.0f;
    float waitTimer;
    float waitInterval = 3.0f;
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
        this.renderer = GetComponent<SpriteRenderer>();
        this.phaseControl = GameObject.FindWithTag("PhaseControl");
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
                    gameObject.rigidbody2D.isKinematic = true;
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
                        //Destroy(gameObject);
                        this.phaseControl.SendMessage("SetPhase", 2);
                        this.waitTimer = this.waitInterval;
                        this.moveTimer = this.moveInterval;
                        this.isMoving = false;
                        this.isStoped = false;
                    }
                }
            }
        }
    }

    void GetAngle(float a)
    {
        this.angle = a;
        /*
        this.angle = Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg * -1;

        // 角度制限
        if (this.angle < this.minAngle)
        {
            this.angle = this.minAngle;
        }
        else if (this.angle > this.maxAngle)
        {
            this.angle = this.maxAngle;
        }
        //Debug.Log("(GetAngle) angle = " + this.angle + "°");
         * */
    }
    

    void AddForceToOrb(Vector2 power)
    {
        gameObject.rigidbody2D.AddForce(power * FORCE);
        this.isMoving = true;
    }

    void ChangeTransparency(float alpha) // 透明度変更
    {
        Color orbColor = new Color(1, 1, 1, alpha);
        this.renderer.color = orbColor;
    }

    void MoveToFirstPosition()
    {
        //transform.localPosition = this.firstPosition;
        transform.localPosition = Vector2.zero;
        gameObject.rigidbody2D.isKinematic = false;

    }
}

