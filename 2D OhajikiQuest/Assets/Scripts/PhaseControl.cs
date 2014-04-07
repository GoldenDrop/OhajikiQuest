using UnityEngine;
using System.Collections;

public class PhaseControl : MonoBehaviour {
    int phase = 0;

    void Start()
    {
        this.phase = 1;
    }

    public void SetPhase(int p)
    {
        this.phase = p;
    }

    public int GetPhase()
    {
        int phaseNumber = this.phase;
        return phaseNumber;
    }

    void Update()
    {
        // デバッグ用　Phaseを1に戻す
        if (this.phase == 2)
        {
            this.phase = 1;
        }
    }
}
