using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermiteMovement : MonoBehaviour {
    private  float speed = .02f;

    private float direction = -1f;

    //for checking platform collisions
    private float xExtent = 0.45f;
    private float yExtent = 0.2f;
    private float xOffset = .08f;
    private int allExceptTermiteSpiderAndRockMask = ~((1 << 10) | (1 << 9) | (1 << 8));

    // Use this for initialization
    void Start () {
        //this.GetComponent<BoxCollider2D>().size.x / 2;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        if (collidewithPlatform()) 
        {
            direction = -direction;
        }
        pos.x += direction * (speed + Time.deltaTime);
        transform.position = pos;
	}

    private bool collidewithPlatform()
    {
        if (direction == -1f)
            return objectToLeft();
        return objectToRight();
    }

    bool objectToRight()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 toTopCorner = new Vector2(xExtent + xOffset, yExtent);
        Vector2 toBottomCorner = new Vector2(xExtent + xOffset, -yExtent * 0.4f);

        bool right = Physics2D.Raycast(pos, Vector2.right, 0, allExceptTermiteSpiderAndRockMask);
        bool diagUp = Physics2D.Raycast(pos, toTopCorner, toTopCorner.magnitude, allExceptTermiteSpiderAndRockMask);
        bool diagDown = Physics2D.Raycast(pos, toBottomCorner, toBottomCorner.magnitude, allExceptTermiteSpiderAndRockMask);
        return right || diagUp || diagDown;
    }

    bool objectToLeft()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 toTopCorner = new Vector2(-xExtent, yExtent);
        Vector2 toBottomCorner = new Vector2(-xExtent, -yExtent * 0.4f);

        bool left = Physics2D.Raycast(pos, Vector2.left, 0, allExceptTermiteSpiderAndRockMask);
        bool diagUp = Physics2D.Raycast(pos, toTopCorner, toTopCorner.magnitude, allExceptTermiteSpiderAndRockMask);
        bool diagDown = Physics2D.Raycast(pos, toBottomCorner, toBottomCorner.magnitude, allExceptTermiteSpiderAndRockMask);
        return left || diagUp || diagDown;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject collwith = coll.gameObject;
        if (collwith.tag == "Spider")
            print("Termite hits Spider.");
    }
}
