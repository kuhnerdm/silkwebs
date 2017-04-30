using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour {

	public float maxVel;
	public float jumpVel;
	public float easing;
	public float epsilon;

	private float xExtent = 0.35f;
	private float yExtent = 0.3f;

	void FixedUpdate() {
		if(Input.GetKey(KeyCode.RightArrow) && !objectToRight() && !Input.GetKey(KeyCode.LeftArrow)) { // just right
			Vector3 oldVel = GetComponent<Rigidbody>().velocity;
            Vector3 newVel = new Vector3(Mathf.Lerp(oldVel.x, maxVel, easing), oldVel.y, oldVel.z);
			GetComponent<Rigidbody>().velocity = newVel;

			Vector3 oldRot = this.transform.rotation.eulerAngles;
			Vector3 newRot = new Vector3(oldRot.x, Mathf.Lerp(oldRot.y, 90f, easing), oldRot.z);

			this.transform.rotation = Quaternion.Euler(newRot);
		}

		if(Input.GetKey(KeyCode.LeftArrow) && !objectToLeft() && !Input.GetKey(KeyCode.RightArrow)) { // just left
			Vector3 oldVel = GetComponent<Rigidbody>().velocity;
            Vector3 newVel = new Vector3(Mathf.Lerp(oldVel.x, -maxVel, easing), oldVel.y, oldVel.z);
            GetComponent<Rigidbody>().velocity = newVel;

			Vector3 oldRot = this.transform.rotation.eulerAngles;
			Vector3 newRot = new Vector3(oldRot.x, Mathf.Lerp(oldRot.y, 270f, easing), oldRot.z);

			this.transform.rotation = Quaternion.Euler(newRot);
		}

		if(Input.GetKey(KeyCode.Space) && Mathf.Abs(GetComponent<Rigidbody>().velocity.y) < epsilon && isOnGround()) { // space
			Vector3 oldVel = GetComponent<Rigidbody>().velocity;
			Vector3 newVel = new Vector3(oldVel.x, jumpVel, oldVel.z);
			GetComponent<Rigidbody>().velocity = newVel;
		}

		if((Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) || (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))) { // either both or neither
			Vector3 oldVel = GetComponent<Rigidbody>().velocity;
			Vector3 newVel = new Vector3(Mathf.Lerp(oldVel.x, 0, easing), oldVel.y, oldVel.z);
			GetComponent<Rigidbody>().velocity = newVel;
		}

	}

	bool isOnGround() {
		Vector3 toLeftCorner = new Vector3(-xExtent + 0.2f, - yExtent, 0f);
		Vector3 toRightCorner = new Vector3(xExtent - 0.2f, -yExtent,  0f);

        bool down = Physics.Raycast(transform.position, -Vector3.up, yExtent + 0.1f);
		bool diagLeft = Physics.Raycast(transform.position, toLeftCorner, toLeftCorner.magnitude + 0.1f);
		bool diagRight = Physics.Raycast(transform.position, toRightCorner, toRightCorner.magnitude + 0.1f);

		return down || diagLeft || diagRight;
	}

	bool objectToRight() {

		Vector3 toTopCorner = new Vector3(xExtent, yExtent, 0f);
        Vector3 toBottomCorner = new Vector3(xExtent, -yExtent + 0.2f, 0f);

        bool right = Physics.Raycast(transform.position, Vector3.right, xExtent + 0.1f);
        bool diagUp = Physics.Raycast(transform.position, toTopCorner, toTopCorner.magnitude + 0.1f);
        bool diagDown = Physics.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude + 0.1f);

        return right || diagUp || diagDown;
	}

	bool objectToLeft() {
        Vector3 toTopCorner = new Vector3(-xExtent, yExtent, 0f);
        Vector3 toBottomCorner = new Vector3(-xExtent, -yExtent + 0.2f, 0f);

		bool left = Physics.Raycast(transform.position, Vector3.left, xExtent + 0.1f);
		bool diagUp = Physics.Raycast(transform.position, toTopCorner, toTopCorner.magnitude + 0.1f);
		bool diagDown = Physics.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude + 0.1f);

		return left || diagUp || diagDown;
	}

}
