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
			Vector3 oldVel = GetComponent<Rigidbody>().velocity;
            Vector3 newVel = new Vector3(Mathf.Lerp(oldVel.x, maxVel, easing), oldVel.y, oldVel.z);
			GetComponent<Rigidbody>().velocity = newVel;
		}

		if(Input.GetKey(KeyCode.LeftArrow) && !objectToLeft()) {
			Vector3 oldVel = GetComponent<Rigidbody>().velocity;
            Vector3 newVel = new Vector3(Mathf.Lerp(oldVel.x, -maxVel, easing), oldVel.y, oldVel.z);
            GetComponent<Rigidbody>().velocity = newVel;
		}

		if(Input.GetKey(KeyCode.Space) && Mathf.Abs(GetComponent<Rigidbody>().velocity.y) < epsilon && isOnGround()) {
			Vector3 oldVel = GetComponent<Rigidbody>().velocity;
			Vector3 newVel = new Vector3(oldVel.x, jumpVel, oldVel.z);
			GetComponent<Rigidbody>().velocity = newVel;
		}

	}

	bool isOnGround() {
        Vector3 toLeftCorner = new Vector3(-GetComponent<Collider>().bounds.extents.x + 0.2f, - GetComponent<Collider>().bounds.extents.y, 0f);
        Vector3 toRightCorner = new Vector3(GetComponent<Collider>().bounds.extents.x - 0.2f, -GetComponent<Collider>().bounds.extents.y,  0f);

        bool down = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f);
		bool diagLeft = Physics.Raycast(transform.position, toLeftCorner, toLeftCorner.magnitude + 0.1f);
		bool diagRight = Physics.Raycast(transform.position, toRightCorner, toRightCorner.magnitude + 0.1f);

		return down || diagLeft || diagRight;
	}

	bool objectToRight() {
        //Vector3 toTopCorner = new Vector3(0f, GetComponent<Collider>().bounds.extents.y, GetComponent<Collider>().bounds.extents.z);
        //Vector3 toBottomCorner = new Vector3(0f, -GetComponent<Collider>().bounds.extents.y + 0.2f, GetComponent<Collider>().bounds.extents.z);

        //bool right = Physics.Raycast(transform.position, Vector3.forward, GetComponent<Collider>().bounds.extents.z + 0.1f);
        //bool diagUp = Physics.Raycast(transform.position, toTopCorner, toTopCorner.magnitude + 0.1f);
        //bool diagDown = Physics.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude + 0.1f);

        Vector3 toTopCorner = new Vector3(GetComponent<Collider>().bounds.extents.x, GetComponent<Collider>().bounds.extents.y, 0f);
        Vector3 toBottomCorner = new Vector3(GetComponent<Collider>().bounds.extents.x, -GetComponent<Collider>().bounds.extents.y + 0.2f, 0f);

        bool right = Physics.Raycast(transform.position, Vector3.right, GetComponent<Collider>().bounds.extents.z + 0.1f);
        bool diagUp = Physics.Raycast(transform.position, toTopCorner, toTopCorner.magnitude + 0.1f);
        bool diagDown = Physics.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude + 0.1f);

        return right || diagUp || diagDown;
	}

	bool objectToLeft() {
        Vector3 toTopCorner = new Vector3(GetComponent<Collider>().bounds.extents.x, GetComponent<Collider>().bounds.extents.y, 0f);
        Vector3 toBottomCorner = new Vector3(GetComponent<Collider>().bounds.extents.x, -GetComponent<Collider>().bounds.extents.y + 0.2f, 0f);

        bool left = Physics.Raycast(transform.position, Vector3.left, GetComponent<Collider>().bounds.extents.z + 0.1f);
		bool diagUp = Physics.Raycast(transform.position, toTopCorner, toTopCorner.magnitude + 0.1f);
		bool diagDown = Physics.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude + 0.1f);

		return left || diagUp || diagDown;
	}

}
