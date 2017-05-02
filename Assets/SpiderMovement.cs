using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour {

	public float maxVel;
	public float jumpVel;
	public float easing;
	public float epsilon;

	void FixedUpdate() {
		if(Input.GetKey(KeyCode.RightArrow) && !objectToRight()) {
			Vector3 oldVel = GetComponent<Rigidbody2D>().velocity;
            Vector3 newVel = new Vector3(Mathf.Lerp(oldVel.x, maxVel, easing), oldVel.y, oldVel.z);
			GetComponent<Rigidbody2D>().velocity = newVel;
		}

		if(Input.GetKey(KeyCode.LeftArrow) && !objectToLeft()) {
			Vector3 oldVel = GetComponent<Rigidbody2D>().velocity;
            Vector3 newVel = new Vector3(Mathf.Lerp(oldVel.x, -maxVel, easing), oldVel.y, oldVel.z);
            GetComponent<Rigidbody2D>().velocity = newVel;
		}

		if(Input.GetKey(KeyCode.Space) && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < epsilon && isOnGround()) {
			Vector3 oldVel = GetComponent<Rigidbody2D>().velocity;
			Vector3 newVel = new Vector3(oldVel.x, jumpVel, oldVel.z);
			GetComponent<Rigidbody2D>().velocity = newVel;
		}

	}

	bool isOnGround() {
        Vector3 toLeftCorner = new Vector3(-GetComponent<Collider2D>().bounds.extents.x + 0.2f, - GetComponent<Collider2D>().bounds.extents.y, 0f);
        Vector3 toRightCorner = new Vector3(GetComponent<Collider2D>().bounds.extents.x - 0.2f, -GetComponent<Collider2D>().bounds.extents.y,  0f);

        bool down = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + 0.1f);
		bool diagLeft = Physics.Raycast(transform.position, toLeftCorner, toLeftCorner.magnitude + 0.1f);
		bool diagRight = Physics.Raycast(transform.position, toRightCorner, toRightCorner.magnitude + 0.1f);

		return down || diagLeft || diagRight;
	}

	bool objectToRight() {
        Vector3 toTopCorner = new Vector3(GetComponent<Collider2D>().bounds.extents.x, GetComponent<Collider2D>().bounds.extents.y, 0f);
        Vector3 toBottomCorner = new Vector3(GetComponent<Collider2D>().bounds.extents.x, -GetComponent<Collider2D>().bounds.extents.y + 0.2f, 0f);

        bool right = Physics2D.Raycast(transform.position, Vector3.right, GetComponent<Collider2D>().bounds.extents.z + 0.1f);
        bool diagUp = Physics2D.Raycast(transform.position, toTopCorner, toTopCorner.magnitude + 0.1f);
        bool diagDown = Physics2D.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude + 0.1f);

        return right || diagUp || diagDown;
	}

	bool objectToLeft() {
        Vector3 toTopCorner = new Vector3(GetComponent<Collider2D>().bounds.extents.x, GetComponent<Collider2D>().bounds.extents.y, 0f);
        Vector3 toBottomCorner = new Vector3(GetComponent<Collider2D>().bounds.extents.x, -GetComponent<Collider2D>().bounds.extents.y + 0.2f, 0f);

        bool left = Physics2D.Raycast(transform.position, Vector3.left, GetComponent<Collider2D>().bounds.extents.z + 0.1f);
		bool diagUp = Physics2D.Raycast(transform.position, toTopCorner, toTopCorner.magnitude + 0.1f);
		bool diagDown = Physics2D.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude + 0.1f);

		return left || diagUp || diagDown;
	}

}
