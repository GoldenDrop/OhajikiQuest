using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    Quaternion rotation;
    public int FORCE = 100;
    GameObject catapult;
    bool isMoving = false;

	void Start () 
    {
        this.catapult = GameObject.FindWithTag("Catapult");
	}
	
	void Update () {
	
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
    }
}
