using UnityEngine;
using System.Collections;

public class SE : MonoBehaviour {

    AudioClip audioClip;
    AudioSource audioSource;
    string path = "SE/";

	void Start () 
    {
        this.audioSource = GetComponent<AudioSource>();
	}
	
	
    void Play(string selectSE)
    {
        string sePath = this.path + selectSE;
        this.audioClip = Resources.Load(sePath) as AudioClip;
        audioSource.clip = audioClip;
        audioSource.PlayOneShot(audioClip);
    }
}
