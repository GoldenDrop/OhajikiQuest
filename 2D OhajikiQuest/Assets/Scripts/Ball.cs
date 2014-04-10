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
        Vector2 pull = delta.normalized;
        // 角度制限
        if (this.angle == this.minAngle)
        {
            Debug.Log("(AddForceToBall) MinLimitAngle");
            pull = new Vector2(Mathf.Cos(this.limitAngle * Mathf.Deg2Rad), Mathf.Sin(this.limitAngle * Mathf.Deg2Rad));
        }
        else if (this.angle == this.maxAngle)
        {
            Debug.Log("(AddForceToBall) MinLimitAngle");
            pull = new Vector2(-Mathf.Cos(this.limitAngle * Mathf.Deg2Rad), Mathf.Sin(this.limitAngle * Mathf.Deg2Rad));
        }
        Debug.Log("(AddForceToBall) pull(" + pull.x + ", " + pull.y + ")" + pull.magnitude);
        float vectorLength = delta.magnitude;
        Debug.Log("(AddForceToBall) delta's VectorLength : " + vectorLength);
        gameObject.rigidbody2D.AddForce(pull * FORCE);// * vectorLength * FORCE);
        this.isMoving = true;
    }
}
