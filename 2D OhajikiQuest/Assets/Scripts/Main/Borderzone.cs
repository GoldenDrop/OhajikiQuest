using UnityEngine;
using System.Collections;

public class Borderzone : MonoBehaviour {

    GameObject gameController;


	void Start () 
    {
        this.gameController = GameObject.FindWithTag("GameController");
	}
	

    void OnCollisionEnter2D(Collision2D hit)
    {
        Debug.Log("Borderzone hit");
        if (hit.gameObject.tag == "Enemy")
        {
            this.gameController.SendMessage("GameOver");
        }
        
    }
}
