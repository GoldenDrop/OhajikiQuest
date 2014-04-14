using UnityEngine;
using System.Collections;

public class GetEnemyCount : MonoBehaviour {
    GameObject gameController;
    Transform enemyBox;

    void Start()
    {
        this.gameController = GameObject.FindWithTag("GameController");
        this.enemyBox = transform.Find("EnemyBox");
        SendToGameController();
    }

    void SendToGameController()
    {
        int enemyCount = this.enemyBox.childCount;
        Debug.Log("<SendToGameController> Enemy Count : " + enemyCount);
        this.gameController.SendMessage("GetEnemyCount", enemyCount);

    }

    
}
