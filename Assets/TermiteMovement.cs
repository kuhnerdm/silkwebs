using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermiteMovement : MonoBehaviour {
    private  float speed = .02f;

    private float direction = -1f;

    //for checking platform collisions
    private float xExtent = 0.8f;
    private float yExtent = 0.5f;
    private int allExceptSpiderAndRockMask = ~(3 << 8);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.x += direction * (speed + Time.deltaTime);
        if (collidewithPlatform()) 
        {
            direction = -direction;
            pos.x += 2 * (direction * (speed + Time.deltaTime));
        }
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
        Vector2 toTopCorner = new Vector2(xExtent, yExtent);
        Vector2 toBottomCorner = new Vector2(xExtent, -yExtent * 0.4f);

        bool right = Physics2D.Raycast(pos, Vector2.right, xExtent * 1.5f, allExceptSpiderAndRockMask);
        bool diagUp = Physics2D.Raycast(pos, toTopCorner, toTopCorner.magnitude, allExceptSpiderAndRockMask);
        bool diagDown = Physics2D.Raycast(pos, toBottomCorner, toBottomCorner.magnitude, allExceptSpiderAndRockMask);
        return right || diagUp || diagDown;
    }

    bool objectToLeft()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        Vector2 toTopCorner = new Vector2(-xExtent, yExtent);
        Vector2 toBottomCorner = new Vector2(-xExtent, -yExtent * 0.4f);

        bool left = Physics2D.Raycast(pos, Vector2.left, xExtent * 1.5f, allExceptSpiderAndRockMask);
        bool diagUp = Physics2D.Raycast(pos, toTopCorner, toTopCorner.magnitude, allExceptSpiderAndRockMask);
        bool diagDown = Physics2D.Raycast(pos, toBottomCorner, toBottomCorner.magnitude, allExceptSpiderAndRockMask);
        return left || diagUp || diagDown;
    }
}
