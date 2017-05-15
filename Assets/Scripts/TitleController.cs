using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleController : MonoBehaviour {

	public GameObject pic;
	public UnityEngine.UI.Text text;

	bool transitioning = false;
	float timeToTransition = 1f;

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			transitioning = true;
		}

		if(transitioning) {
			if(timeToTransition <= 0) {
				SceneManager.LoadScene(1);
			}
			timeToTransition -= Time.deltaTime;
			Color c = text.color;
			c.a -= Time.deltaTime / timeToTransition;
			text.color = c;
			pic.GetComponent<SpriteRenderer>().color = c;

		}
	}

}
