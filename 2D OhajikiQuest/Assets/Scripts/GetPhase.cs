using UnityEngine;
using System.Collections;

public class GetPhase : MonoBehaviour {
    GameObject phaseControl;
    PhaseControl phaseComponent;

	void Start () 
    {
        this.phaseControl = GameObject.FindWithTag("PhaseControl");
        this.phaseComponent = this.phaseControl.GetComponent<PhaseControl>();
	}

    public int GetNowPhase()
    {
        int phaseNumber = this.phaseComponent.GetPhase();
        //Debug.Log("Get Phase : " + phaseNumber);
        return phaseNumber;
    }

    public int GetPhaseNumber(string phaseCase)
    {
        int getNumber = 0;
        switch (phaseCase)
        {
            case "Player" : // playerフェイス回数を取ってくる
                getNumber = this.phaseComponent.GetPlayerPhaseNumber();
                break;
            case "Enemy" : // enemyフェイス回数を取ってくる
                getNumber = this.phaseComponent.GetEnemyPhaseNumber();
                break;
        }
        return getNumber;
    }
}
