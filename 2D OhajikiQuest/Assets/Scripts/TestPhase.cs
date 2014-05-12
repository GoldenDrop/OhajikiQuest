using UnityEngine;
using System.Collections;

public class TestPhase : MonoBehaviour {
    int phase = 0;
    string s;
	void Start () 
    {
        this.phase = (int)Phase.PresentPhase.Wait;
        Debug.Log(this.phase);
        switch (this.phase)
        {
            case (int)Phase.PresentPhase.Wait:
                Debug.Log("Wait");
                break;
        }
        this.s = BGM.BGM2;
        Debug.Log(this.s);
        this.s = SE.SE1;
        Debug.Log(this.s);

	}
}
