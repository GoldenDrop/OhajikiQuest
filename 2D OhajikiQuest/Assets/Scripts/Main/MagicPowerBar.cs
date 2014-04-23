using UnityEngine;
using System.Collections;

public class MagicPowerBar : MonoBehaviour {

    bool isReleased = false;
    float timer;
    float interval = 2.0f;
    float lostPower;

    void Start()
    {
        this.timer = this.interval;
        
    }

    void Update()
    {
        if (isReleased)
        {
            this.lostPower -= 0.05f;
            if (this.lostPower < 0f)
            {
                // はみ出ないように長さを調節
                this.lostPower = 0;
            }
            Debug.Log("lostPower : " + this.lostPower);
            ChangeMagicPower(lostPower);
            this.timer -= Time.deltaTime;
            Debug.Log("timer : " + this.timer);
            if (this.timer < 0)
            {
                this.isReleased = false;
                this.timer = this.interval;
            }
        }
    }
    
    void ChangeMagicPower(float power)
    {
        if (power > 1.0f)
        {
            // はみ出ないように長さを調節
            power = 1.0f;
        }
        this.lostPower = power;
        Debug.Log("ChangeMagicPower lostPower : " + this.lostPower);
        Vector3 scale = new Vector3(power, 0.5f, 1.0f);
        gameObject.transform.localScale = scale;
    }

    void ReleasedPower() 
    {
        Debug.Log("ReleasedPower");
        this.isReleased = true;
    }
}
