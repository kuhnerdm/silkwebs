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

	private int allExceptSpiderAndRockMask = ~(3 << 8);
	private int allExceptSpiderMask = ~(2 << 8);

	void Start() {
		Transform leftHips = transform.GetChild(0).GetChild(0).GetChild(0);
		Transform rightHips = transform.GetChild(0).GetChild(0).GetChild(1);
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
			Vector2 oldVel = GetComponent<Rigidbody2D>().velocity;
			Vector2 newVel = new Vector2(Mathf.Lerp(oldVel.x, maxVel, easing), oldVel.y);
			GetComponent<Rigidbody2D>().velocity = newVel;

			Vector2 oldRot = this.transform.rotation.eulerAngles;
			Vector2 newRot = new Vector2(oldRot.x, Mathf.Lerp(oldRot.y, 0, easing));

			this.transform.rotation = Quaternion.Euler(newRot);
			rotateLegs();
		}

		if(Input.GetKey(KeyCode.LeftArrow) && !objectToLeft() && !Input.GetKey(KeyCode.RightArrow)) { // just left
			Vector2 oldVel = GetComponent<Rigidbody2D>().velocity;
			Vector2 newVel = new Vector2(Mathf.Lerp(oldVel.x, -maxVel, easing), oldVel.y);
			GetComponent<Rigidbody2D>().velocity = newVel;

			Vector2 oldRot = this.transform.rotation.eulerAngles;
			Vector2 newRot = new Vector2(oldRot.x, Mathf.Lerp(oldRot.y, 180f, easing));

			this.transform.rotation = Quaternion.Euler(newRot);

			rotateLegs();
		}


		if(Input.GetKey(KeyCode.Space) && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= epsilon && isOnGround()) { // space
			Vector2 oldVel = GetComponent<Rigidbody2D>().velocity;
			Vector2 newVel = new Vector2(oldVel.x, jumpVel);
			GetComponent<Rigidbody2D>().velocity = newVel;
		}

		if((Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) || (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))) { // either both or neither
			Vector2 oldVel = GetComponent<Rigidbody2D>().velocity;
			Vector2 newVel = new Vector2(Mathf.Lerp(oldVel.x, 0, easing), oldVel.y);
			GetComponent<Rigidbody2D>().velocity = newVel;
		}

	}

	bool isOnGround() {
		Vector2 pos = new Vector2(transform.position.x, transform.position.y);
		Vector2 toLeftCorner = new Vector2(-xExtent * 0.9f, - yExtent);
		Vector2 toRightCorner = new Vector2(xExtent * 0.9f, -yExtent);

		bool down = Physics2D.Raycast(pos, Vector2.down, yExtent, allExceptSpiderAndRockMask);
		bool diagLeft = Physics2D.Raycast(pos, toLeftCorner, toLeftCorner.magnitude, allExceptSpiderMask);
		bool diagRight = Physics2D.Raycast(pos, toRightCorner, toRightCorner.magnitude, allExceptSpiderMask);
		return down || diagLeft || diagRight;
	}

	bool objectToRight() {
		Vector2 pos = new Vector2(transform.position.x, transform.position.y);
		Vector2 toTopCorner = new Vector2(xExtent, yExtent);
		Vector2 toBottomCorner = new Vector2(xExtent, -yExtent * 0.4f);

		bool right = Physics2D.Raycast(pos, Vector2.right, xExtent * 1.5f, allExceptSpiderAndRockMask);
		bool diagUp = Physics2D.Raycast(pos, toTopCorner, toTopCorner.magnitude, allExceptSpiderAndRockMask);
		bool diagDown = Physics2D.Raycast(pos, toBottomCorner, toBottomCorner.magnitude, allExceptSpiderAndRockMask);
		return right || diagUp || diagDown;
	}

	bool objectToLeft() {
		Vector2 pos = new Vector2(transform.position.x, transform.position.y);
		Vector2 toTopCorner = new Vector2(-xExtent, yExtent);
		Vector2 toBottomCorner = new Vector2(-xExtent, -yExtent * 0.4f);

		bool left = Physics2D.Raycast(pos, Vector2.left, xExtent * 1.5f, allExceptSpiderAndRockMask);
		bool diagUp = Physics2D.Raycast(pos, toTopCorner, toTopCorner.magnitude, allExceptSpiderAndRockMask);
		bool diagDown = Physics2D.Raycast(pos, toBottomCorner, toBottomCorner.magnitude, allExceptSpiderAndRockMask);
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

