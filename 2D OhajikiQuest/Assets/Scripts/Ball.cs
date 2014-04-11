using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    Quaternion rotation;
    public int FORCE = 500;
    GameObject catapult;
    GameObject phaseControl;
    bool isMoving = false;
    float moveTimer;
    float moveInterval = 3.0f;
    float angle;
    public float limitAngle = 30.0f;
    float minAngle;
    float maxAngle;
    public float minLength = 0.5f;
    public float maxLength = 2.0f;

	void Start () 
    {
        this.catapult     = GameObject.FindWithTag("Catapult");
        this.phaseControl = GameObject.FindWithTag("PhaseControl");
        this.moveTimer    = this.moveInterval;
        this.minAngle = -90 + limitAngle;
        this.maxAngle =  90 - limitAngle;
	}
	
	void Update () 
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
                gameObject.rigidbody2D.fixedAngle = true;
                gameObject.rigidbody2D.fixedAngle = false;
                Destroy(gameObject);
                this.isMoving = false;
                this.phaseControl.SendMessage("SetPhase", 2);
                this.moveTimer = this.moveInterval;
            }
        }
	}

    void GetAngle(Vector2 delta)
    {
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
        Debug.Log("(GetAngle) angle = " + this.angle + "°");
    }

    void Turn()
    {
        this.rotation.eulerAngles = new Vector3(0, 0, this.angle);
        transform.rotation = rotation;
    }

   
    void AddForceToBall(Vector2 delta)
    {
        Vector2 pull = delta.normalized; // ベクトルの長さを1にする

        // 角度制限
        if (this.angle == this.minAngle)
        {
            // 与えるベクトルをminAngleのベクトルに変更する
            pull = new Vector2(Mathf.Cos(this.limitAngle * Mathf.Deg2Rad), Mathf.Sin(this.limitAngle * Mathf.Deg2Rad));
        }
        else if (this.angle == this.maxAngle)
        {
            // 与えるベクトルをmaxAngleのベクトルに変更する
            pull = new Vector2(-Mathf.Cos(this.limitAngle * Mathf.Deg2Rad), Mathf.Sin(this.limitAngle * Mathf.Deg2Rad));
        }

        float vectorLength = delta.magnitude; // 引っ張りの長さを取得
        Debug.Log("(AddForceToBall) delta's VectorLength : " + vectorLength);

        // 長さ制限
        if (vectorLength < this.minLength)
        {
            Debug.Log("(AddForceToBall) Min Length");
            vectorLength = this.minLength;
        }
        else if (vectorLength > this.maxLength)
        {
            Debug.Log("(AddForceToBall) Max Length");
            vectorLength = this.maxLength;
        }

        gameObject.rigidbody2D.AddForce(pull * vectorLength * FORCE);
        this.isMoving = true;
    }
}
