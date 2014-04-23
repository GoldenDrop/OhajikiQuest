using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
    GetPhase getPhase;
    int phase = 0;
    int turnNumber = 0;
    public int moveInterval = 1; // 何ターン毎に動くか

	void Start () 
    {
        this.getPhase = gameObject.GetComponent<GetPhase>();
	}
	
	void Update () 
    {
        this.phase = this.getPhase.GetNowPhase();
        
        //Debug.Log("Enemy Get Phase : " + this.phase);
        if (this.phase == 2) // 敵フェイズ
        {
            this.turnNumber = this.getPhase.GetTrunNumber("Enemy");
            //Debug.Log("GetEnemyPhaseNumber : " + this.phaseNumber);
            if (this.turnNumber % this.moveInterval == 0)
            {
                Move();
            }
        }
	}

    void Move()
    {
        transform.Translate(new Vector2(0, -0.005f));
    }
}
