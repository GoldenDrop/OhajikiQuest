using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour {

    int stageNumber = 1;
    int lastStageNumber = 2;
    GameObject stage;
    GameObject gameController;
    GameObject result;
    GameObject systemMessage;

    Transform enemyBox;

	void Start () 
    {
        this.gameController = GameObject.FindWithTag("GameController");
        this.result = GameObject.FindWithTag("Result");
        this.systemMessage = GameObject.FindWithTag("SystemMessage");

        CreateStage();
	}

    void CreateStage()
    {
        string path = "Prefabs/Stage/STAGE" + this.stageNumber;
        Debug.Log("path : " + path);
        GameObject stagePrefab = Resources.Load(path) as GameObject;
        this.stage = Instantiate(stagePrefab, new Vector2(10.0f, 0), Quaternion.identity) as GameObject;
        this.enemyBox = this.stage.transform.Find("EnemyBox");
        SendToGameControllerEnemyCount();

    }

    void NextStage()
    {
        this.stageNumber++;
        Debug.Log("Next Stage : " + this.stageNumber);
        //this.stageNumber = 1; // Debug
        if (this.stageNumber == this.lastStageNumber)
        {
            Debug.Log("Next Last Stage. OnLastStageFlag");
            this.gameController.SendMessage("OnLastStageFlag");
        }
        Destroy(this.stage);
        CreateStage();
        this.systemMessage.SendMessage("CatchStageNumber", this.stageNumber);
    }

    void RetryStage()
    {
        Destroy(this.stage);
        CreateStage();
    }

    void RestStage()
    {
        this.stageNumber = 1;
        Destroy(this.stage);
        CreateStage();
    }

    void SendToResultClearStage(string from)
    {
        int clearStage = this.stageNumber;
        switch (from)
        {
            case "GAMEOVER":
                clearStage--;
                break;
        }
        this.result.SendMessage("CatchClearStage", clearStage);
    }

    void SendToGameControllerEnemyCount()
    {
        int enemyCount = this.enemyBox.childCount;
        this.gameController.SendMessage("CatchEnemyCount", enemyCount);
    }
}
