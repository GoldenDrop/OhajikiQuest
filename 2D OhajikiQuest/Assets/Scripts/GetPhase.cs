using UnityEngine;
using System.Collections;

public class GetPhase : MonoBehaviour {
    GameObject phaseController;
    PhaseControl phaseComponent;

	void Start () 
    {
        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.phaseComponent = this.phaseController.GetComponent<PhaseControl>();
	}

    public int GetNowPhase()
    {
        int phaseNumber = this.phaseComponent.GetPhase();
        //Debug.Log("Get Phase : " + phaseNumber);
        return phaseNumber;
    }

    public int GetTrunNumber(string phaseCase)
    {
        int getNumber = 0;
        switch (phaseCase)
        {
            case "Player" : // playerフェイス回数を取ってくる
                getNumber = this.phaseComponent.GetPlayerTrunNumber();
                break;
            case "Enemy" : // enemyフェイス回数を取ってくる
                getNumber = this.phaseComponent.GetEnemyTurnNumber();
                break;
        }
        return getNumber;
    }
}
