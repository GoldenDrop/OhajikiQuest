using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {

    int resultScore     = 0;
    int resultTotalTurn = 0;
    int claerStage      = 0;
    int phase           = 0;

    GetPhase getPhase; // 現行フェイズ取得クラス

    GameObject fadeManager;
    GameObject mainCamera;
    GameObject phaseController;
    GameObject title;

    float fadeOutTimer;
    float fadeOutInterval = 1.5f;
    float fadeInTimer;
    float fadeInInterval  = 1.5f;

    bool isStartedFadeOut = false;
    bool isStartedFadeIn  = false;
    bool onOk             = false;

    RaycastHit2D hit;



	void Start () 
    {
        this.getPhase = gameObject.GetComponent<GetPhase>();
        this.fadeManager = GameObject.FindWithTag("FadeManager");
        this.mainCamera = GameObject.FindWithTag("MainCamera");
        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.title = GameObject.FindWithTag("Title");

        this.fadeOutTimer = this.fadeOutInterval;
        this.fadeInTimer = this.fadeInInterval;
	}
	
	void Update () 
    {
        this.phase = this.getPhase.GetNowPhase();
        if (this.phase == 5)
        {
            if (this.onOk)
            {
                GoToTitle();
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    this.hit = Physics2D.Raycast(mousePoint, Vector2.zero);
                    if (this.hit)
                    {
                        if (this.hit.collider.gameObject.name == "OkButton")
                        {
                            this.onOk = true;
                        }
                    }
                }
            }
        }
	}


    void GoToTitle()
    {
        Debug.Log("Go to Title");
        this.fadeOutTimer -= Time.deltaTime;
        if (!this.isStartedFadeOut)
        {
            float fadeOutSpeed = 0.7f;
            this.fadeManager.SendMessage("OnFadeOutFlag", fadeOutSpeed);
            this.isStartedFadeOut = true;
        }

        if (this.fadeOutTimer < 0)
        {
            Debug.Log("GoToTitle FadeInStart");
            if (!this.isStartedFadeIn)
            {
                string place = "TITLE";
                this.mainCamera.SendMessage("Move", place);
                this.title.SendMessage("ChangeDisplayTexts", true);
                float fadeInSpeed = 0.7f;
                this.fadeManager.SendMessage("OnFadeInFlag", fadeInSpeed);
                this.isStartedFadeIn = true;
            }
            this.fadeInTimer -= Time.deltaTime;
            if (this.fadeInTimer < 0)
            {
                int phase = 3;
                this.phaseController.SendMessage("SetPhase", phase);
                this.fadeOutTimer = this.fadeOutInterval;
                this.fadeInTimer = this.fadeInInterval;
                this.onOk = false;
                this.isStartedFadeOut = false;
                this.isStartedFadeIn = false;
            }
        }
    }


    void CatchScore(int score)
    {
        this.resultScore = score;
        Debug.Log("Result SCORE : " + this.resultScore);
    }

    void CatchTotalTurn(int turn)
    {
        this.resultTotalTurn = turn;
        Debug.Log("Result TOTAL TURN : " + this.resultTotalTurn);

    }

    void CatchClearStage(int stage)
    {
        this.claerStage = stage;
        Debug.Log("Result CLEAR STAGE : " + this.claerStage);

    }
}
