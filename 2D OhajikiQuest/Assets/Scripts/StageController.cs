using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour {

    int stageNumber = 1;
    GameObject stage;

	void Awake () {
        CreateStage();
	}

    void CreateStage()
    {
        string path = "Stage/" + this.stageNumber;
        Debug.Log("path : " + path);
        GameObject stagePrefab = Resources.Load(path) as GameObject;
        this.stage = Instantiate(stagePrefab, Vector2.zero, Quaternion.identity) as GameObject;

    }

    void NextStage()
    {
        this.stageNumber++;
        if (this.stageNumber < 7)
        {
            Destroy(this.stage);
            CreateStage();
        }
        else
        {
            Debug.Log("Game Clear");
        }
    }
}
