using UnityEngine;
using System.Collections;

public class ExplosionAnimation : MonoBehaviour {

    Animator animator;
    GameObject seManager;


	void Start () 
    {
        this.seManager = GameObject.FindWithTag("SEManager");

        Destroy(gameObject, 1.5f);
        this.animator = GetComponent<Animator>();
        OnTriggerAnimation();
        this.seManager.SendMessage("Play", SE.SE3);

	}

    void OnTriggerAnimation()
    {
        this.animator.SetTrigger("Explosion");
    }
}
