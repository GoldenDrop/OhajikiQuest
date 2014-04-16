using UnityEngine;
using System.Collections;

public class BlackoutCurtainObject : MonoBehaviour {
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
