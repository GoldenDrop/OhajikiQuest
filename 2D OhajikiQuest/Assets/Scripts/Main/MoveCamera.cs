using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	void Start () 
    {
	    // 移動テスト
        string test = "TITLE";
        Move(test);
	}

    void Move(string place)
    {
        Vector3 cameraPoint = new Vector3(0, 0, -10.0f);
        switch (place)
        {
            case "TITLE": // Titleへ
                cameraPoint.x += 0; 
                break;
            case "STAGE": // Stageへ
                cameraPoint.x += 10; 
                break;
            case "GAMEOVER": // GameOverへ
                cameraPoint.x += 20; 
                break;
            case "RESULT": // Resultへ
                cameraPoint.x += 30; 
                break;
        }
        transform.localPosition = cameraPoint;
    }
}
