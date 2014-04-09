using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
    GetPhase getPhase;
    int phase = 0;

	void Start () 
    {
        this.getPhase = gameObject.GetComponent<GetPhase>();
	}
	
	void Update () 
    {
        this.phase = this.getPhase.GetPhaseNumber();
        //Debug.Log("Enemy Get Phase : " + this.phase);
        if (this.phase == 2) // 敵フェイズ
        {
            Move();
        }
	}

    void Move()
    {
        transform.Translate(new Vector2(0, -0.005f));
    }
}
