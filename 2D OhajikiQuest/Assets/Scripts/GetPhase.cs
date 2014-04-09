using UnityEngine;
using System.Collections;

public class GetPhase : MonoBehaviour {
    GameObject phaseControl;
    PhaseControl phaseComponent;

	void Start () 
    {
        this.phaseControl = GameObject.FindWithTag("PhaseControl");
	    
	}

    public int GetPhaseNumber()
    {
        this.phaseComponent = this.phaseControl.GetComponent<PhaseControl>();
        int phaseNumber = this.phaseComponent.GetPhase();
        Debug.Log("Get Phase : " + phaseNumber);
        return phaseNumber;
    }
}
