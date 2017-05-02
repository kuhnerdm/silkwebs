using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachHook : MonoBehaviour {

    /// <summary>
    /// Hook prefab
    /// </summary>
    public GameObject hookPrefab;

    internal GameObject curHook;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameObject player = transform.gameObject;  // The script should be placed on the spider.
            Vector2 spiderAimDirection = player.transform.rotation * (FacingRight ? Vector2.right : Vector2.left);

            GetComponent<Collider2D>().enabled = false;
            // Distance of 1 in raycast was picked arbitrarily but seems to work for now.
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, spiderAimDirection, 1);  
            GetComponent<Collider2D>().enabled = true;

            //Vector3 mouseScreenPoint = Input.mousePosition;
            //mouseScreenPoint.z = -Camera.main.transform.position.z;
            //Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(mouseScreenPoint);

            if (hit.collider != null)
            {
                Vector2 destination = hit.point;

                // Instatiate hook at destination.
                curHook = Instantiate(hookPrefab, destination, 
                                      Quaternion.identity) as GameObject;
                curHook.GetComponent<Silk>().connectedObject = hit.collider.gameObject;
            }
        }
    }

    /// <summary>
    /// True if the spider is facing right. False if the spider is facing left.
    /// </summary>
    bool FacingRight
    {
        get
        {
            return GetComponent<SimpleSpiderMovement>().facingRight;
        }
    }
}
