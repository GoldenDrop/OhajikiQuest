using UnityEngine;
using System.Collections;

public class PhaseControl : MonoBehaviour {
    int phase = 0;
    float enemyTime;
    float enemyTimeInterval = 2.5f;
    GameObject magicCircle;

    // それぞれのフェイズ回数
    int playerPhaseNumber = 0;
    int enemyPhaseNumber = 0;


    void Start()
    {
        this.magicCircle = GameObject.FindWithTag("MagicCircle");
        this.phase = 1;
        this.playerPhaseNumber++;
        this.enemyTime = this.enemyTimeInterval;
    }

    public void SetPhase(int p)
    {
        this.phase = p;
        switch (this.phase)
        {
            case 1 :
                this.playerPhaseNumber++;
                Debug.Log("playerPhaseNumber ++");
                break;
            case 2:
                this.enemyPhaseNumber++;
                Debug.Log("enemyPhaseNumber ++ : " + this.enemyPhaseNumber);
                break;
        }
    }

    public int GetPhase()
    {
        int phaseNumber = this.phase;
        return phaseNumber;
    }

    public int GetPlayerPhaseNumber()
    {
        int playerNumber = this.playerPhaseNumber;
        return playerNumber;
    }

    public int GetEnemyPhaseNumber()
    {
        int enemyNumber = this.enemyPhaseNumber;
        return enemyNumber;
    }

    void Update()
    {
        if (this.phase == 2)
        {
            this.enemyTime -= Time.deltaTime;
            if (this.enemyTime < 0)
            {
                this.magicCircle.SendMessage("CreatePlayer");
                this.magicCircle.SendMessage("InitializedPlayer");
                this.phase = 1;
                this.enemyTime = this.enemyTimeInterval;
            }
        }
    }
}
