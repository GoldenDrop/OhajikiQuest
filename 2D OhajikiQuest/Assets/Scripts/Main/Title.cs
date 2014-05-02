﻿using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

    GameObject phaseController;
    GameObject systemMessage;
    GameObject fadeManager;
    GameObject mainCamera;
    GameObject seManager;
    GameObject bgmPlayer;



    Transform titleText;
    Transform startText;
    int phase = 0;

    GetPhase getPhase; // 現行フェイズ取得クラス

    float blinkTimer;
    float blinkInterval   = 0.6f;
    float fadeOutTimer;
    float fadeOutInterval = 1.5f;
    float fadeInTimer;
    float fadeInInterval  = 1.5f;

    bool onClick          = false;
    bool isStartedFadeOut = false;
    bool isStartedFadeIn  = false;


	void Start () 
    {
        this.getPhase        = gameObject.GetComponent<GetPhase>();

        this.fadeManager     = GameObject.FindWithTag("FadeManager");
        this.mainCamera      = GameObject.FindWithTag("MainCamera");
        this.systemMessage   = GameObject.FindWithTag("SystemMessage");
        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.seManager       = GameObject.FindWithTag("SEManager");
        this.bgmPlayer       = GameObject.FindWithTag("BGMPlayer");

        this.titleText       = gameObject.transform.Find("TitleText");
        this.startText       = gameObject.transform.Find("StartText");

        this.blinkTimer      = this.blinkInterval;
        this.fadeOutTimer    = this.fadeOutInterval;
        this.fadeInTimer     = this.fadeInInterval;

        string bgm = "TITLE";
        this.bgmPlayer.SendMessage("Play", bgm);
	}
	
	void Update () 
    {
        this.phase = this.getPhase.GetNowPhase();
        if (this.phase == 3) // Titleフェイズなら
        {
            this.blinkTimer -= Time.deltaTime;

            if (this.blinkTimer < 0 & !this.onClick)
            {
                BlinkStartText();
                this.blinkTimer = this.blinkInterval;
            }


            if (this.onClick)
            {
                this.fadeOutTimer -= Time.deltaTime;
                if (!this.isStartedFadeOut)
                {
                    string se = "TitleClick";
                    this.seManager.SendMessage("Play", se);
                    this.bgmPlayer.SendMessage("Stop");
                    float fadeOutSpeed = 0.7f;
                    this.fadeManager.SendMessage("OnFadeOutFlag", fadeOutSpeed);
                    this.isStartedFadeOut = true;
                }
                
                if (this.fadeOutTimer < 0)
                {
                    Debug.Log("Title FadeInStart");
                    if (!this.isStartedFadeIn)
                    {
                        ChangeDisplayTexts(false);
                        string place = "STAGE";
                        this.mainCamera.SendMessage("Move", place);
                        float fadeInSpeed = 0.7f;
                        this.fadeManager.SendMessage("OnFadeInFlag", fadeInSpeed);
                        this.isStartedFadeIn = true;
                    }
                    this.fadeInTimer -= Time.deltaTime;
                    if (this.fadeInTimer < 0)
                    {
                        this.fadeOutTimer = this.fadeOutInterval;
                        this.fadeInTimer = this.fadeInInterval;
                        this.onClick = false;
                        this.isStartedFadeOut = false;
                        this.isStartedFadeIn = false;
                        int phase = 0;
                        this.phaseController.SendMessage("SetPhase", phase);
                        string flag = "START";
                        this.systemMessage.SendMessage("OnFlag", flag);
                    }
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
