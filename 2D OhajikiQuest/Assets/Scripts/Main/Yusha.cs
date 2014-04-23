using UnityEngine;
using System.Collections;

public class Yusha : MonoBehaviour {

    Animator animator;
	void Start () 
    {
        this.animator = GetComponent<Animator>();

	}
	
	
    void Recites(bool boolean)
    {
        this.animator.SetBool("isRecites", boolean);
    }

    void MoveDown(bool boolean)
    {
        this.animator.SetBool("isMoveDown", boolean);
    }

    void Stand(bool boolean)
    {
        this.animator.SetBool("isStand", boolean);
    }

    void Down(bool boolean)
    {
        this.animator.SetBool("isDown", boolean);
    }

    void OffFlags()
    {
        Recites(false);
        MoveDown(false);
        Stand(false);
        Down(false);
    }
}
