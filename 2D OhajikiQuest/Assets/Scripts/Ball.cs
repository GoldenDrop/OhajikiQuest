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


	void Start () 
    {
        this.catapult     = GameObject.FindWithTag("Catapult");
        this.phaseControl = GameObject.FindWithTag("PhaseControl");
        this.moveTimer    = this.moveInterval;
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

    void Turn(Vector2 delta)
    {
        float angle = Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg * -1;
        Debug.Log("angle = " + angle + "°");
        this.rotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = rotation;
    }

   
    void AddForceToBall(Vector2 delta)
    {
        float multiple = Mathf.Sqrt(delta.x * delta.x + delta.y * delta.y);
        gameObject.rigidbody2D.AddForce(delta * FORCE);
        this.isMoving = true;
    }
}
