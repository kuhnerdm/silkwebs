using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collwith = coll.gameObject;
        if (collwith.tag == "Spider")
            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("Termite hits Spider.", this);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
            else
            {
                Debug.Log("Fly hits Spider.", this);
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
            
    }
}
