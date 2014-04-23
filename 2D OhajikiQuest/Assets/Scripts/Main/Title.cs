using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

    GameObject fadeManager;
    GameObject mainCamera;
    Transform titleText;
    Transform startText;
    int phase = 0;
    float blinkTimer;
    float blinkInterval = 0.6f;
    float fadeOutTimer;
    float fadeOutInterval = 1.5f;
    float fadeInTimer;
    float fadeInInterval = 1.5f;
    bool onClick = false;
    bool isStartedFadeOut = false;
    bool isStartedFadeIn = false;
    GameObject phaseControl;


	void Start () 
    {
        this.fadeManager  = GameObject.FindWithTag("FadeManager");
        this.mainCamera   = GameObject.FindWithTag("MainCamera");
        this.titleText    = gameObject.transform.Find("TitleText");
        this.startText    = gameObject.transform.Find("StartText");
        this.phaseControl = GameObject.FindWithTag("PhaseControl");
        this.blinkTimer   = this.blinkInterval;
        this.fadeOutTimer = this.fadeOutInterval;
        this.fadeInTimer = this.fadeInInterval;
	}
	
	void Update () 
    {
        if (this.phase == 3) // Titleフェイズなら
        {
            this.blinkTimer -= Time.deltaTime;

            if (this.blinkTimer < 0)
            {
                BlinkStartText();
                this.blinkTimer = this.blinkInterval;
            }


            if (this.onClick)
            {
                this.fadeOutTimer -= Time.deltaTime;
                if (!this.isStartedFadeOut)
                {
                    float fadeOutSpeed = 0.7f;
                    this.fadeManager.SendMessage("OnFadeOutFlag", fadeOutSpeed);
                    this.isStartedFadeOut = true;
                }
                
                if (this.fadeOutTimer < 0)
                {
                    Debug.Log("Title FadeInStart");
                    this.fadeInTimer -= Time.deltaTime;
                    ChangeDisplayTexts(false);
                    string place = "STAGE";
                    this.mainCamera.SendMessage("Move", place);
                    if (!this.isStartedFadeIn)
                    {
                        float fadeInSpeed = 0.7f;
                        this.fadeManager.SendMessage("OnFadeInFlag", fadeInSpeed);
                        this.isStartedFadeIn = true;
                    }
                    UpDatePhase(0);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0)) // クリックしたなら
                {
                    this.onClick = true;
                }
            }
        }
        if (this.phase == 0)
        {
            this.fadeOutTimer = this.fadeOutInterval;
            this.fadeInTimer = this.fadeInInterval;
            this.onClick = false;
            this.isStartedFadeOut = false;
            this.isStartedFadeIn = false;
            this.phaseControl.SendMessage("SetPhase", 1);
            UpDatePhase(1);
        }

        
	}

    void UpDatePhase(int phaseNumber) 
    {
        this.phase = phaseNumber;
    }

    void BlinkStartText()
    {
        bool isBlinked = this.startText.guiText.enabled;
        if (isBlinked)
        {
            this.startText.guiText.enabled = false;
        }
        else
        {
            this.startText.guiText.enabled = true;
        }
    }

    void ChangeDisplayTexts(bool enabled) // 表示変更
    {
        if (enabled)
        {
            this.startText.guiText.enabled = true;
            this.titleText.guiText.enabled = true;
        }
        else
        {
            this.startText.guiText.enabled = false;
            this.titleText.guiText.enabled = false;
        }
    }
}
