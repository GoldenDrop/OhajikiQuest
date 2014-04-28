using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {

    int resultScore     = 0;
    int resultTotalTurn = 0;
    int claerStage      = 0;

    GameObject fadeManager;
    GameObject mainCamera;
    GameObject phaseController;



	void Start () 
    {
        this.fadeManager = GameObject.FindWithTag("FadeManager");
        this.mainCamera = GameObject.FindWithTag("MainCamera");
        this.phaseController = GameObject.FindWithTag("PhaseController");
	}
	
	void Update () 
    {
	
	}

    void CatchScore(int score)
    {
        this.resultScore = score;
        Debug.Log("Result SCORE : " + this.resultScore);
    }

    void CatchTotalTurn(int turn)
    {
        this.resultTotalTurn = turn;
        Debug.Log("Result TOTAL TURN : " + this.resultTotalTurn);

    }

    void CatchClearStage(int stage)
    {
        this.claerStage = stage;
        Debug.Log("Result CLEAR STAGE : " + this.claerStage);

    }
}
