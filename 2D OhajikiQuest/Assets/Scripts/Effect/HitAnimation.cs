using UnityEngine;
using System.Collections;

public class HitAnimation : MonoBehaviour {

    Animator animator;

	void Start () 
    {
        Destroy(gameObject, 1);
        this.animator = GetComponent<Animator>();
        OnTriggerAnimation();
	}
	
	void Update () {
	
	}

    void OnTriggerAnimation()
    {
        this.animator.SetTrigger("Hit");
    }
}
