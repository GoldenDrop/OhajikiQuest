using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    int enemyCount;
    GameObject stageController;
    bool clearFloag = false;
    float waitTimer;
    float waitTime = 3.5f;
    GameObject phaseController;
    GameObject systemMessage;
    string eventFlag = "WAIT";
    


    void Start()
    {
        this.stageController = GameObject.FindWithTag("StageController");
        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.systemMessage   = GameObject.FindWithTag("SystemMessage");
        this.waitTimer = this.waitTime;
    }

    void Update()
    {
        switch (this.eventFlag)
        {
            case "CLEAR":
                this.waitTimer -= Time.deltaTime;
                if (this.waitTimer < 0)
                {
                    int phase = 0;
                    this.phaseController.SendMessage("SetPhase", phase);
                    string flag = "CLEAR";
                    this.systemMessage.SendMessage("OnFlag", flag);
                    this.eventFlag = "WAIT";
                    this.waitTimer = this.waitTime;
                }
                break;

            case "GAMEOVER":
                Debug.Log("Flag = GAMEOVER");
                int phase = 0;
                this.phaseController.SendMessage("SetPhase", phase);
                string flag = "GAMEOVER";
                this.systemMessage.SendMessage("OnFlag", flag);
                this.eventFlag = "WAIT";
                break;
        }
    }

    void CatchEnemyCount(int count)
    {
        this.enemyCount = count;
        Debug.Log("EnemyCount : " + enemyCount);
    }

    void UpdateEnemyCount()
    {
        this.enemyCount--;
        Debug.Log("<UpdateEnemyCount> Enemy Count : " + this.enemyCount);
        if (this.enemyCount == 0)
        {
            Debug.Log("GoNextStage");
            this.eventFlag = "CLEAR";
        }
    }

    void GoNextStage()
    {
        Debug.Log("GoNextStage");
        this.stageController.SendMessage("NextStage");
    }

    void GameOver()
    {
        this.eventFlag = "GAMEOVER";
        Debug.Log("GameOver");
    }
}
