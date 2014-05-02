using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour {

    // Result画面に表示する値
    int resultScore     = 0;
    int resultTotalTurn = 0;
    int claerStage      = 0;
    int phase           = 0;

    GetPhase getPhase; // 現行フェイズ取得クラス

    GameObject fadeManager;
    GameObject mainCamera;
    GameObject phaseController;
    GameObject title;
    GameObject seManager;
    GameObject bgmPlayer;


    float fadeOutTimer;
    float fadeOutInterval = 1.5f;
    float fadeInTimer;
    float fadeInInterval  = 1.5f;

    bool isStartedFadeOut = false;
    bool isStartedFadeIn  = false;
    bool onOk             = false;

    RaycastHit2D hit;

    // 値の数字オブジェクト
    GameObject stageDigit;
    GameObject trunOneDigit;
    GameObject trunTwoDigit;
    GameObject scoreOneDigit;
    GameObject scoreTwoDigit;
    GameObject scoreThreeDigit;
    GameObject scoreFourDigit;
    GameObject scoreFiveDigit;

    // オフセット
    float offsetScoreX =  0.25f;
    float offsetScoreY = -1.6f;
    float offsetTrunX  =  0.15f;
    float offsetTrunY  =  0.1f;
    float offsetStageY =  1.9f;

    string numberPath = "Prefabs/Number/Stage's/";

    // 子オブジェクト　生成した数字オブジェクトを整理するため
    Transform stageText;
    Transform trunText;
    Transform scoreText;


	void Start () 
    {
        this.getPhase = gameObject.GetComponent<GetPhase>();
        this.fadeManager = GameObject.FindWithTag("FadeManager");
        this.mainCamera = GameObject.FindWithTag("MainCamera");
        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.title = GameObject.FindWithTag("Title");
        this.seManager = GameObject.FindWithTag("SEManager");
        this.bgmPlayer = GameObject.FindWithTag("BGMPlayer");

        this.stageText = gameObject.transform.Find("STAGE");
        this.trunText = gameObject.transform.Find("TURN");
        this.scoreText = gameObject.transform.Find("SCORE");

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
            string se = "Click";
            this.seManager.SendMessage("Play", se);
            this.bgmPlayer.SendMessage("Stop");
            float fadeOutSpeed = 0.7f;
            this.fadeManager.SendMessage("OnFadeOutFlag", fadeOutSpeed);
            this.isStartedFadeOut = true;
        }

        if (this.fadeOutTimer < 0)
        {
            Debug.Log("GoToTitle FadeInStart");
            if (!this.isStartedFadeIn)
            {
                DestroyNumbersObject();
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
                string selectBGM = "TITLE";
                this.bgmPlayer.SendMessage("Play", selectBGM);
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
        //Debug.Log("Result SCORE : " + this.resultScore);
        ScoreToGameObject();
    }

    void CatchTotalTurn(int turn)
    {
        this.resultTotalTurn = turn;
        if (turn > 99)
        {
            this.resultTotalTurn = 99;
        }
        //Debug.Log("Result TOTAL TURN : " + this.resultTotalTurn);
        TurnToGameObject();

    }

    void CatchClearStage(int stage)
    {
        this.claerStage = stage;
        //Debug.Log("Result CLEAR STAGE : " + this.claerStage);
        StageToGameObject();
    }

    void TurnToGameObject()
    {
        GameObject onePrefab;
        GameObject twoPrefab;
        Vector2 oneDigitPoint = new Vector2(transform.position.x + this.offsetTrunX, transform.position.y + this.offsetTrunY);
        Vector2 twoDigitPoint = new Vector2(transform.position.x - this.offsetTrunX, transform.position.y + this.offsetTrunY);
        string turnString = string.Format("{0:D2}", this.resultTotalTurn);
        Debug.Log("Result turnString : " + turnString);
        for (int i = 0; i < turnString.Length; i++)
        {
            string number = turnString.Substring((turnString.Length - 1) - i, 1);
            string path = this.numberPath + number;
            switch (i)
            {
                case 0:
                    onePrefab = Resources.Load(path) as GameObject;
                    this.trunOneDigit = Instantiate(onePrefab, oneDigitPoint, Quaternion.identity) as GameObject;
                    this.trunOneDigit.transform.parent = this.trunText;
                    break;
                case 1:
                    twoPrefab = Resources.Load(path) as GameObject;
                    this.trunTwoDigit = Instantiate(twoPrefab, twoDigitPoint, Quaternion.identity) as GameObject;
                    this.trunTwoDigit.transform.parent = this.trunText;
                    break;
            }
        }
    }

    void ScoreToGameObject()
    {
        GameObject onePrefab;
        GameObject twoPrefab;
        GameObject threePrefab;
        GameObject fourPrefab;
        GameObject fivePrefab;
        Vector2 firstPoint = new Vector2(transform.position.x + 2 * this.offsetScoreX, transform.position.y + this.offsetScoreY);
        string scoreString = string.Format("{0:D5}", this.resultScore);
        for (int i = 0; i < scoreString.Length; i++)
        {
            string number = scoreString.Substring((scoreString.Length - 1) - i, 1);
            string path = this.numberPath + number;
            switch (i)
            {
                case 0:
                    onePrefab = Resources.Load(path) as GameObject;
                    this.scoreOneDigit = Instantiate(onePrefab, firstPoint, Quaternion.identity) as GameObject;
                    this.scoreOneDigit.transform.parent = this.scoreText;
                    break;
                case 1:
                    twoPrefab = Resources.Load(path) as GameObject;
                    this.scoreTwoDigit = Instantiate(twoPrefab, firstPoint - new Vector2(this.offsetScoreX, 0), Quaternion.identity) as GameObject;
                    this.scoreTwoDigit.transform.parent = this.scoreText;
                    break;
                case 2:
                    threePrefab = Resources.Load(path) as GameObject;
                    this.scoreThreeDigit = Instantiate(threePrefab, firstPoint - new Vector2(2 * this.offsetScoreX, 0), Quaternion.identity) as GameObject;
                    this.scoreThreeDigit.transform.parent = this.scoreText;
                    break;
                case 3:
                    fourPrefab = Resources.Load(path) as GameObject;
                    this.scoreFourDigit = Instantiate(fourPrefab, firstPoint - new Vector2(3 * this.offsetScoreX, 0), Quaternion.identity) as GameObject;
                    this.scoreFourDigit.transform.parent = this.scoreText;
                    break;
                case 4:
                    fivePrefab = Resources.Load(path) as GameObject;
                    this.scoreFiveDigit = Instantiate(fivePrefab, firstPoint - new Vector2(4 * this.offsetScoreX, 0), Quaternion.identity) as GameObject;
                    this.scoreFiveDigit.transform.parent = this.scoreText;
                    break;
            }
        }
    }

    void StageToGameObject()
    {
        string turnString = this.claerStage.ToString();
        string path = this.numberPath + turnString;
        GameObject onePrefab = Resources.Load(path) as GameObject;
        Vector2 digitPoint = new Vector2(transform.position.x, transform.position.y + this.offsetStageY);
        this.stageDigit = Instantiate(onePrefab, digitPoint, Quaternion.identity) as GameObject;
        this.stageDigit.transform.parent = this.stageText;    
                    
    }

    void DestroyNumbersObject()
    {
        Destroy(this.trunOneDigit);
        Destroy(this.trunTwoDigit);
        Destroy(this.scoreOneDigit);
        Destroy(this.scoreTwoDigit);
        Destroy(this.scoreThreeDigit);
        Destroy(this.scoreFourDigit);
        Destroy(this.scoreFourDigit);
        Destroy(this.scoreFiveDigit);
        Destroy(this.stageDigit);
    }
}
