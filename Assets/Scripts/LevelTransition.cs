using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {
    public float transRate = 1f;
    public GameObject screen;
    //private GameObject mesh;


    private float alpha = 0.0f;
    private bool isFade = false;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("I update?" + isFade, this);
        if (isFade)
        {
            if (alpha >= 1f)
            {
                isFade = false;
            }
            else
            {
                Debug.Log("alpha " + alpha, this);
                alpha += transRate * (alpha + transRate);
                if (alpha > 1f)
                {
                    alpha = 1f;
                }
            }
            Color c = screen.GetComponent<Renderer>().material.color;
            c.a = alpha;
            screen.GetComponent<Renderer>().material.color = c;
        }

        if (alpha >= 1) { 

            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("Level end", this);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
            else
            {
                Debug.Log("Level end, resetting.", this);
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            }
        }   
    

	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collwith = coll.gameObject;
        if (collwith.tag == "Spider")
        {
            isFade = true;
        }
    }
    
}
