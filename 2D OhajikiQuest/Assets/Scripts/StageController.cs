using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour {

    int stageNumber = 1;
    GameObject stage;
    GameObject gameController;
    GameObject result;
    Transform enemyBox;

	void Start () 
    {
        this.gameController = GameObject.FindWithTag("GameController");
        this.result = GameObject.FindWithTag("Result");
        CreateStage();
	}

    void CreateStage()
    {
        string path = "Prefabs/Stage/" + this.stageNumber;
        Debug.Log("path : " + path);
        GameObject stagePrefab = Resources.Load(path) as GameObject;
        this.stage = Instantiate(stagePrefab, new Vector2(10.0f, 0), Quaternion.identity) as GameObject;
        this.enemyBox = this.stage.transform.Find("EnemyBox");
        SendToGameControllerEnemyCount();

    }

    void NextStage()
    {
        this.stageNumber++;
        this.stageNumber = 1; // Debug
        if (this.stageNumber < 7)
        {
            Destroy(this.stage);
            CreateStage();
        }
        else
        {
            Debug.Log("this.stageNumber Erorr.");
        }
    }

    void RestStageNumber()
    {
        this.stageNumber = 1;
    }

    void SendToResultClearStage()
    {
        int clearStage = this.stageNumber - 1;
        this.result.SendMessage("CatchClearStage", clearStage);
    }

    void SendToGameControllerEnemyCount()
    {
        int enemyCount = this.enemyBox.childCount;
        this.gameController.SendMessage("CatchEnemyCount", enemyCount);
    }
}
