using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    int enemyCount;


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
            Win();
        }
    }

    void Win()
    {
        Debug.Log("YOU WIN");
    }

    void GameOver()
    {
        // シーン移動予定
        Debug.Log("GameOver");
    }
}
