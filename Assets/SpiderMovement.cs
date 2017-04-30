using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour {

	public float maxVel;
	public float jumpVel;
	public float easing;
	public float epsilon;

	private float xExtent = 0.8f;
	private float yExtent = 0.5f;

	private float legPosition = 0f;
	private float timeSinceReset = 0f;
	public float resetTime = 1f;

	private Transform[] hips;

	private int pushableMask = ~(1 << 8);

	void Start() {
		Transform leftHips = transform.GetChild(0).GetChild(0);
		Transform rightHips = transform.GetChild(0).GetChild(1);
		hips = new Transform[8];
		for(int i = 0; i < 4; i++) {
			hips[i] = leftHips.GetChild(i).GetChild(0);
		}

		for(int i = 4; i < 8; i++) {
			hips[i] = rightHips.GetChild(i - 4).GetChild(0);
		}
	}

	void FixedUpdate() {
		if(Input.GetKey(KeyCode.RightArrow) && !objectToRight() && !Input.GetKey(KeyCode.LeftArrow)) { // just right
			Vector3 oldVel = GetComponent<Rigidbody>().velocity;
            Vector3 newVel = new Vector3(Mathf.Lerp(oldVel.x, maxVel, easing), oldVel.y, oldVel.z);
			GetComponent<Rigidbody>().velocity = newVel;

			Vector3 oldRot = this.transform.rotation.eulerAngles;
			Vector3 newRot = new Vector3(oldRot.x, Mathf.Lerp(oldRot.y, 90f, easing), oldRot.z);

			this.transform.rotation = Quaternion.Euler(newRot);
			rotateLegs();
		}

		if(Input.GetKey(KeyCode.LeftArrow) && !objectToLeft() && !Input.GetKey(KeyCode.RightArrow)) { // just left
			Vector3 oldVel = GetComponent<Rigidbody>().velocity;
            Vector3 newVel = new Vector3(Mathf.Lerp(oldVel.x, -maxVel, easing), oldVel.y, oldVel.z);
            GetComponent<Rigidbody>().velocity = newVel;

			Vector3 oldRot = this.transform.rotation.eulerAngles;
			Vector3 newRot = new Vector3(oldRot.x, Mathf.Lerp(oldRot.y, 270f, easing), oldRot.z);

			this.transform.rotation = Quaternion.Euler(newRot);

			rotateLegs();
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
		Vector3 toLeftCorner = new Vector3(-xExtent, - yExtent, 0f);
		Vector3 toRightCorner = new Vector3(xExtent, -yExtent,  0f);

		bool down = Physics.Raycast(transform.position, Vector3.down, yExtent);
		bool diagLeft = Physics.Raycast(transform.position, toLeftCorner, toLeftCorner.magnitude);
		bool diagRight = Physics.Raycast(transform.position, toRightCorner, toRightCorner.magnitude);

		return down || diagLeft || diagRight;
	}

	bool objectToRight() {

		Vector3 toTopCorner = new Vector3(xExtent, yExtent, 0f);
		Vector3 toBottomCorner = new Vector3(xExtent, -yExtent * 0.4f, 0f);

		bool right = Physics.Raycast(transform.position, Vector3.right, xExtent * 1.2f, pushableMask);
		bool diagUp = Physics.Raycast(transform.position, toTopCorner, toTopCorner.magnitude * 1.2f, pushableMask);
		bool diagDown = Physics.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude, pushableMask);

        return right || diagUp || diagDown;
	}

	bool objectToLeft() {
        Vector3 toTopCorner = new Vector3(-xExtent, yExtent, 0f);
        Vector3 toBottomCorner = new Vector3(-xExtent, -yExtent * 0.4f, 0f);

		bool left = Physics.Raycast(transform.position, Vector3.left, xExtent * 1.2f, pushableMask);
		bool diagUp = Physics.Raycast(transform.position, toTopCorner, toTopCorner.magnitude * 1.2f, pushableMask);
		bool diagDown = Physics.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude, pushableMask);

		return left || diagUp || diagDown;
	}

	void rotateLegs() {
		timeSinceReset += Time.fixedDeltaTime;
		if(timeSinceReset > resetTime) {
			timeSinceReset = 0;
		}
		legPosition = 0.5f * Mathf.Sin(timeSinceReset / resetTime * 2 * Mathf.PI) + 0.5f;
		for(int i = 0; i < 8; i++) {
			float rot;
			if(i % 2 == 0) {
				rot = Mathf.Lerp(0, -40, legPosition);
			} else {
				rot = Mathf.Lerp(-60, -20, legPosition);
			}
			hips[i].localRotation = Quaternion.Euler(hips[i].localRotation.eulerAngles.x, rot, hips[i].localRotation.eulerAngles.z);
		}
	}

}
