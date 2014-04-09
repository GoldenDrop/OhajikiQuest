using UnityEngine;
using System.Collections;

public class PhaseControl : MonoBehaviour {
    int phase = 0;
    float enemyTime;
    float enemyTimeInterval = 2.5f;

    void Start()
    {
        this.phase = 1;
        this.enemyTime = this.enemyTimeInterval;
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
            this.enemyTime -= Time.deltaTime;
            if (this.enemyTime < 0)
            {
                this.phase = 1;
                this.enemyTime = this.enemyTimeInterval;
            }
        }
    }
}
