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
    


    void Start()
    {
        this.stageController = GameObject.FindWithTag("StageController");
        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.systemMessage   = GameObject.FindWithTag("SystemMessage");
        this.waitTimer = this.waitTime;
    }

    void Update()
    {
        if (this.clearFloag)
        {
            this.waitTimer -= Time.deltaTime;
            if (this.waitTimer < 0)
            {
                int phase = 0;
                this.phaseController.SendMessage("SetPhase", phase);
                string flag = "CLEAR";
                this.systemMessage.SendMessage("OnFlag", flag);
                this.clearFloag = false;
                this.waitTimer = this.waitTime;
            }
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
            this.clearFloag = true;
        }
    }

    void GoNextStage()
    {
        Debug.Log("GoNextStage");
        this.stageController.SendMessage("NextStage");
    }

    void GameOver()
    {
        // シーン移動予定
        Debug.Log("GameOver");
    }
}
