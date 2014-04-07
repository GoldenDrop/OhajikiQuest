using UnityEngine;
using System.Collections;

public class MathfTest : MonoBehaviour {
    public int i = 0;
	
	void Start () {
        int n = Mathf.FloorToInt(i / 4);
        Debug.Log(n);
	}
	
	void Update () {
	
	}
}
