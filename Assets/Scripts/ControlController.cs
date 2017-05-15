using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControlController : MonoBehaviour {

	public GameObject pic;

	bool transitioning = false;
	float timeToTransition = 1f;

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			transitioning = true;
		}

		if(transitioning) {
			if(timeToTransition <= 0) {
				SceneManager.LoadScene(2);
				return;
			}
			timeToTransition -= Time.deltaTime;
			Color c = pic.GetComponent<SpriteRenderer>().color;
			c.a -= Time.deltaTime / timeToTransition;
			pic.GetComponent<SpriteRenderer>().color = c;

		}
	}
}
