using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {
    GetPhase getPhase;
    int phase = 0;
    int turnNumber = 0;
    public int moveInterval = 1; // 何ターン毎に動くか
    public float moveSpeed = 0.3f; // 移動速度

	void Start () 
    {
        this.getPhase = gameObject.GetComponent<GetPhase>();
	}
	
	void Update () 
    {
        this.phase = this.getPhase.GetNowPhase();
        
        if (this.phase == (int)Phase.PresentPhase.Enemy) // 敵フェイズ
        {
            this.turnNumber = this.getPhase.GetTrunNumber("Enemy");
            if (this.turnNumber % this.moveInterval == 0)
            {
                Move();
            }
        }
	}

    void Move()
    {
        transform.Translate(Vector2.up * -1 * this.moveSpeed * Time.deltaTime);
    }
}
