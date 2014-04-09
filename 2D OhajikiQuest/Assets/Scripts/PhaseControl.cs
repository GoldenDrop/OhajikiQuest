using UnityEngine;
using System.Collections;

public class PhaseControl : MonoBehaviour {
    int phase = 0;
    float enemyTime;
    float enemyTimeInterval = 2.5f;
    GameObject catapult;

    void Start()
    {
        this.catapult = GameObject.FindWithTag("Catapult");
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
        if (this.phase == 2)
        {
            this.enemyTime -= Time.deltaTime;
            if (this.enemyTime < 0)
            {
                this.phase = 1;
                this.catapult.SendMessage("InitializedCatapult");
                this.enemyTime = this.enemyTimeInterval;
            }
        }
    }
}
