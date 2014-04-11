using UnityEngine;
using System.Collections;

public class BorderLine : MonoBehaviour {

    GameObject gameController;

	void Start () 
    {
        this.gameController = GameObject.FindWithTag("GameController");
	    
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            this.gameController.SendMessage("GameOver");
        }
    }
}
