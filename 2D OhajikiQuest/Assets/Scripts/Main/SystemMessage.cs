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
    GameObject magicCircle;

    float fadeOutTimer;
    float fadeOutInterval = 1.5f;
    float fadeInTimer;
    float fadeInInterval = 1.5f;
    float massageTimer;
    float massageTime = 1.0f;

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
        this.magicCircle = GameObject.FindWithTag("MagicCircle");

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
        this.massageTimer = this.massageTime;
        //this.clearFlag = true;
	}
	
	void Update () 
    {
        switch (this.messageFlag)
        {
            case "CLEAR":
                ClearMessage();
                break;

            case "START":
                BattleStartMessage();
                break;

            case "GAMEOVERE":
                GameOverMessage();
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
                    this.magicCircle.SendMessage("InitializedPlayer");
                    this.stageController.SendMessage("NextStage");
                    this.phaseController.SendMessage("ResetTurns");
                    float fadeInSpeed = 0.7f;
                    this.fadeManager.SendMessage("OnFadeInFlag", fadeInSpeed);
                    this.isStartedFadeIn = true;
                }
                this.fadeInTimer -= Time.deltaTime;
                if (this.fadeInTimer < 0)
                {
                    Debug.Log("fadeInTimer 0");
                    this.isStartedFadeOut = false;
                    this.isStartedFadeIn = false;
                    this.moveTimer = this.moveTime;
                    this.fadeOutTimer = this.fadeOutInterval;
                    this.fadeInTimer = this.fadeInInterval;
                    this.messageFlag = "START";
                }
            }

        }
    }

    void GameOverMessage()
    {

    }

    void BattleStartMessage()
    {
        this.moveTimer -= Time.deltaTime;
        if (this.moveTimer > 0)
        {
            this.battleObject.transform.Translate(-Vector2.up * this.moveSpeed * Time.deltaTime);
            this.startObject.transform.Translate(Vector2.up * this.moveSpeed * Time.deltaTime);
        }
        if (this.moveTimer < 0)
        {
            this.massageTimer -= Time.deltaTime;
            if (this.massageTimer < 0)
            {
                this.battleObject.transform.localPosition = this.topPoint;
                this.startObject.transform.localPosition = this.bottomPoint;
                this.phaseController.SendMessage("SetPhase", 1);
                this.messageFlag = "WAIT";
                this.moveTimer = this.moveTime;
                this.massageTimer = this.massageTime;
            }
        }
    }


    void OnFlag(string flag)
    {
        this.messageFlag = flag;
    }
}
