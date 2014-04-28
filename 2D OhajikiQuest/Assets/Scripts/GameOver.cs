using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    GameObject phaseController;
    GameObject stageController;
    GameObject fadeManager;
    GameObject mainCamera;
    RaycastHit2D hit;
    int phase = 0;
    //GameObject selectedObject;
    GetPhase getPhase;

	void Start () 
    {
        this.getPhase = gameObject.GetComponent<GetPhase>();
	}
	
	void Update () 
    {
        this.phase = this.getPhase.GetNowPhase();
        if (this.phase == 4)
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                // Raycast(光線の出る位置, 光線の向き)
                this.hit = Physics2D.Raycast(mousePoint, Vector2.zero);
                if (this.hit)
                {
                    GameObject selectedObject = this.hit.collider.gameObject;
                    switch (selectedObject.name)
                    {
                        case "ExitButton":
                            Debug.Log("ExitButton Click");
                            break;

                        case "RetryButton":
                            Debug.Log("RetryButton Click");
                            break;

                    }
                    
                }
            }
        }
	}
}
