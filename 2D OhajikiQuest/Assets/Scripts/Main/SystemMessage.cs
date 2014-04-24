using UnityEngine;
using System.Collections;

public class SystemMessage : MonoBehaviour {


    Transform stageObject;
    Transform clearObject;
    Transform defenceObject;
    Transform failureObject;
    Transform battleObject;
    Transform startObject;

    string messageFlag = "WAIT";

    bool clearFlag    = false;
    bool gameOverFlag = false;
    bool battleStartFlag = false;

    float moveTimer;
    float moveTime = 2.0f;
    float moveSpeed = 2.8f;

    GameObject phaseController;
    GameObject stageController;
    GameObject fadeManager;
    GameObject mainCamera;

    float fadeOutTimer;
    float fadeOutInterval = 1.5f;
    float fadeInTimer;
    float fadeInInterval = 1.5f;
    bool isStartedFadeOut = false;
    bool isStartedFadeIn = false;
    bool isMessageStop = false;

    Vector2 topPoint;
    Vector2 bottomPoint;


	void Start () 
    {
        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.stageController = GameObject.FindWithTag("StageController");
        this.fadeManager = GameObject.FindWithTag("FadeManager");
        this.mainCamera = GameObject.FindWithTag("MainCamera");

        this.stageObject = gameObject.transform.Find("STAGE");
        this.clearObject = gameObject.transform.Find("CLEAR");
        this.defenceObject = gameObject.transform.Find("DEFENCE");
        this.failureObject = gameObject.transform.Find("FAILURE");
        this.battleObject = gameObject.transform.Find("BATTLE");
        this.startObject = gameObject.transform.Find("START");


        this.topPoint    = this.stageObject.transform.position;
        this.bottomPoint = this.clearObject.transform.position;

        Debug.Log("topPoint : " + this.topPoint.x + ", " + this.topPoint.y);
        Debug.Log("bottomPoint : " + this.bottomPoint.x + ", " + this.bottomPoint.y);


        this.moveTimer = this.moveTime;
        this.fadeOutTimer = this.fadeOutInterval;
        this.fadeInTimer = this.fadeInInterval;

        //this.clearFlag = true;
	}
	
	void Update () 
    {
        /*
        if (this.clearFlag)
        {
            ClearMessage();
        }
        */
        switch (this.messageFlag)
        {
            case "CLEAR" :
                ClearMessage();
                break;
        }
	}

    void ClearMessage()
    {
        this.moveTimer -= Time.deltaTime;
        if (this.moveTimer > 0)
        {
            this.stageObject.transform.Translate(-Vector2.up * this.moveSpeed * Time.deltaTime);
            this.clearObject.transform.Translate(Vector2.up * this.moveSpeed * Time.deltaTime);
        }
        if (this.moveTimer < 0)
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
                if (!this.isStartedFadeIn)
                {
                    this.stageObject.transform.localPosition = this.topPoint;
                    this.clearObject.transform.localPosition = this.bottomPoint;

                    this.stageController.SendMessage("NextStage");
                    this.phaseController.SendMessage("ResetTurns");
                    float fadeInSpeed = 0.7f;
                    this.fadeManager.SendMessage("OnFadeInFlag", fadeInSpeed);
                    this.isStartedFadeIn = true;
                }
                this.fadeInTimer -= Time.deltaTime;
                if (this.fadeInTimer < 0)
                {
                    this.messageFlag = "WAIT";
                    this.isStartedFadeOut = false;
                    this.isStartedFadeIn = false;
                    this.moveTimer = this.moveTime;
                    this.fadeOutTimer = this.fadeOutInterval;
                    this.fadeInTimer = this.fadeInInterval;
                }
            }

        }
    }

    void GameOverMessage()
    {

    }

    void BattleStartMessage()
    {

    }

    void OnClearFlag()
    {
        this.clearFlag = true;
    }

    void OnGameOverFlag()
    {
        this.gameOverFlag = true;
    }

    void OnBattleStartFlag()
    {
        this.battleStartFlag = true;
    }

    void OnFlag(string flag)
    {
        this.messageFlag = flag;
    }
}
