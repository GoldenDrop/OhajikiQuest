using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    int enemyCount;
    int sendPhase = 0;
    GameObject stageController;
    float waitTimer;
    float waitTime = 3.5f;
    GameObject phaseController;
    GameObject systemMessage;
    GameObject player;
    // WAIT : 待機, CLEAR : ステージクリア, GAMEOVER : ゲームオーバー, GAMECLEAR : ゲームクリア
    string eventFlag = "WAIT";
    string sendFlag = "";
    bool isLastStage = false;
    


    void Start()
    {
        this.stageController = GameObject.FindWithTag("StageController");
        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.systemMessage   = GameObject.FindWithTag("SystemMessage");
        this.player          = GameObject.FindWithTag("Player");
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
                    this.sendPhase = 0;
                    this.phaseController.SendMessage("SetPhase", this.sendPhase);
                    this.sendFlag = "CLEAR";
                    this.systemMessage.SendMessage("OnFlag", this.sendFlag);
                    this.eventFlag = "WAIT";
                    this.waitTimer = this.waitTime;
                }
                break;

            case "GAMEOVER":
                Debug.Log("Flag = GAMEOVER");
                this.sendPhase = 0;
                this.phaseController.SendMessage("SetPhase", this.sendPhase);
                this.sendFlag = "GAMEOVER";
                this.systemMessage.SendMessage("OnFlag", this.sendFlag);
                this.eventFlag = "WAIT";
                break;

            case "GAMECLEAR":
                this.waitTimer -= Time.deltaTime;
                if (this.waitTimer < 0)
                {
                    this.sendPhase = 0;
                    this.phaseController.SendMessage("SetPhase", this.sendPhase);
                    this.sendFlag = "GAMECLEAR";
                    this.systemMessage.SendMessage("OnFlag", this.sendFlag);
                    this.eventFlag = "WAIT";
                    this.isLastStage = false;
                    this.waitTimer = this.waitTime;
                }
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
            this.player.SendMessage("OnClearFlag");

            if (this.isLastStage)
            {
                this.eventFlag = "GAMECLEAR";
            }
            else
            {
                this.eventFlag = "CLEAR";

            }
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

    void OnLastStageFlag()
    {
        this.isLastStage = true;
    }
}
