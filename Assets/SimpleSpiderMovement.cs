using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpiderMovement : MonoBehaviour {

    public float maxVel;
    public float jumpVel;
    public float easing;
    public float epsilon;

    private float xExtent = 0.8f;
    private float yExtent = 0.5f;

    private int pushableMask = ~(1 << 8);

    void Start()
    {
    }

    void FixedUpdate()
    {
        //if (Input.GetKey(KeyCode.RightArrow) && !objectToRight() && !Input.GetKey(KeyCode.LeftArrow))
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        { // just right
            Vector2 oldVel = GetComponent<Rigidbody2D>().velocity;
            Vector2 newVel = new Vector2(Mathf.Lerp(oldVel.x, maxVel, easing), oldVel.y);
            GetComponent<Rigidbody2D>().velocity = newVel;
        }

        //if (Input.GetKey(KeyCode.LeftArrow) && !objectToLeft() && !Input.GetKey(KeyCode.RightArrow))
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        { // just left
            Vector2 oldVel = GetComponent<Rigidbody2D>().velocity;
            Vector2 newVel = new Vector2(Mathf.Lerp(oldVel.x, -maxVel, easing), oldVel.y);
            GetComponent<Rigidbody2D>().velocity = newVel;
        }

        if (Input.GetKey(KeyCode.Space) && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < epsilon && isOnGround())
        { // space
            Vector2 oldVel = GetComponent<Rigidbody2D>().velocity;
            Vector2 newVel = new Vector2(oldVel.x, jumpVel);
            GetComponent<Rigidbody2D>().velocity = newVel;
        }

        if ((Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)) || (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)))
        { // either both or neither
            Vector2 oldVel = GetComponent<Rigidbody2D>().velocity;
            Vector2 newVel = new Vector2(Mathf.Lerp(oldVel.x, 0, easing), oldVel.y);
            GetComponent<Rigidbody2D>().velocity = newVel;
        }

    }

    bool isOnGround()
    {
        Vector2 toLeftCorner = new Vector2(-xExtent, -yExtent);
        Vector2 toRightCorner = new Vector2(xExtent, -yExtent);

        bool down = Physics2D.Raycast(transform.position, Vector2.down, yExtent);
        bool diagLeft = Physics2D.Raycast(transform.position, toLeftCorner, toLeftCorner.magnitude);
        bool diagRight = Physics2D.Raycast(transform.position, toRightCorner, toRightCorner.magnitude);

        return down || diagLeft || diagRight;
    }

    bool objectToRight()
    {

        Vector2 toTopCorner = new Vector2(xExtent, yExtent);
        Vector2 toBottomCorner = new Vector2(xExtent, -yExtent * 0.4f);

        bool right = Physics2D.Raycast(transform.position, Vector2.right, xExtent * 1.2f, pushableMask);
        bool diagUp = Physics2D.Raycast(transform.position, toTopCorner, toTopCorner.magnitude * 1.2f, pushableMask);
        bool diagDown = Physics2D.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude, pushableMask);

        return right || diagUp || diagDown;
    }

    bool objectToLeft()
    {
        Vector2 toTopCorner = new Vector2(-xExtent, yExtent);
        Vector2 toBottomCorner = new Vector2(-xExtent, -yExtent * 0.4f);

        bool left = Physics2D.Raycast(transform.position, Vector2.left, xExtent * 1.2f, pushableMask);
        bool diagUp = Physics2D.Raycast(transform.position, toTopCorner, toTopCorner.magnitude * 1.2f, pushableMask);
        bool diagDown = Physics2D.Raycast(transform.position, toBottomCorner, toBottomCorner.magnitude, pushableMask);

        return left || diagUp || diagDown;
    }
}
