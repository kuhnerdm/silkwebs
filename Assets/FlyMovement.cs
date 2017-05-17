using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour {
    private float sightradius = 3f;
    private bool fleeing = false;
    private bool dead = false;
    private float speed = 3f;
    private GameObject spider;

	// Use this for initialization
	void Start () {
        spider = GameObject.Find("Spider");
    }
	
	// Update is called once per frame
	void Update () {
        if (dead)
        {
            //do nothing
        }
		else if (!fleeing)
        {
            if (seesSpider()) 
                fleeing = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spider.transform.position, -speed * Time.deltaTime);
        }
	}

    private bool seesSpider()
    {
        float distance = Vector3.Distance(transform.position, spider.transform.position);
        return distance <= sightradius;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collwith = coll.gameObject;
        if (collwith.tag == "Spider")
        {
            Debug.Log("Fly hits Spider.", this);
            dead = true;
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            Vector3 pos = transform.position;
            pos.z = 1;
            transform.position = pos;
            //Destroy(this.gameObject);
        }
    }
}
