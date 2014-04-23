using UnityEngine;
using System.Collections;

public class PhaseControl : MonoBehaviour {
    int phase = 0;
    float enemyTime;
    float enemyTimeInterval = 2.5f;
    GameObject magicCircle;
    GameObject gui;
    GameObject title;
    Transform turn;

    // それぞれのターン回数
    int playerTurn = 0;
    int enemyTrun  = 0;
    int totalTurn  = 0;


    void Start()
    {
        this.magicCircle = GameObject.FindWithTag("MagicCircle");
        this.title = GameObject.FindWithTag("Title");
        this.gui = GameObject.FindWithTag("GUI");
        this.turn = this.gui.transform.Find("BottomBord/TURN");
        SetPhase(3);
        this.enemyTime = this.enemyTimeInterval;
    }

    public void SetPhase(int p)
    {
        this.phase = p;
        switch (this.phase)
        {
            case 1 :
                this.playerTurn++;
                this.totalTurn++;
                this.turn.SendMessage("UpdteTurn", playerTurn);
                Debug.Log("playerTurn ++");
                Debug.Log("totalTurn ++");
                break;
            case 2:
                this.enemyTrun++;
                Debug.Log("enemyTrun ++ : " + this.enemyTrun);
                break;
            case 3:
                this.title.SendMessage("UpDatePhase", this.phase);
                break;
        }
    }

    public int GetPhase()
    {
        int phaseNumber = this.phase;
        return phaseNumber;
    }

    public int GetPlayerTrunNumber()
    {
        int playerTurnNumber = this.playerTurn;
        return playerTurnNumber;
    }

    public int GetEnemyTurnNumber()
    {
        int enemyTurnNumber = this.enemyTrun;
        return enemyTurnNumber;
    }

    void Update()
    {
        if (this.phase == 2)
        {
            this.enemyTime -= Time.deltaTime;
            if (this.enemyTime < 0)
            {
                this.magicCircle.SendMessage("InitializedPlayer");
                SetPhase(1);
                this.enemyTime = this.enemyTimeInterval;
            }
        }
    }
}
