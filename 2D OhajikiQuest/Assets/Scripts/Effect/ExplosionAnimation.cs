using UnityEngine;
using System.Collections;

public class ExplosionAnimation : MonoBehaviour {

    Animator animator;

	void Start () 
    {
        Destroy(gameObject, 1.5f);
        this.animator = GetComponent<Animator>();
        OnTriggerAnimation();
	}

    void OnTriggerAnimation()
    {
        this.animator.SetTrigger("Explosion");
    }
}
