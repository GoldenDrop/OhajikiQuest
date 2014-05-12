using UnityEngine;
using System.Collections;

public class HitAnimation : MonoBehaviour {

    Animator animator;
    GameObject seManager;


	void Start () 
    {
        this.seManager = GameObject.FindWithTag("SEManager");

        Destroy(gameObject, 1);
        this.animator = GetComponent<Animator>();
        OnTriggerAnimation();
        this.seManager.SendMessage("Play", SE.SE7);

	}
	
	void Update () {
	
	}

    void OnTriggerAnimation()
    {
        this.animator.SetTrigger("Hit");
    }
}
