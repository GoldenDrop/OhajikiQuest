using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    int enemyCount;
    GameObject stageController;


    void Start()
    {
        this.stageController = GameObject.FindWithTag("StageController");
    }

    void GetEnemyCount(int count)
    {
        this.enemyCount = count;
    }

    void UpdateEnemyCount()
    {
        this.enemyCount--;
        Debug.Log("<UpdateEnemyCount> Enemy Count : " + this.enemyCount);
        if (this.enemyCount == 0)
        {
            GoNextStage();
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
