using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour {
    private float sightradius = 5f;
    private bool fleeing = false;
    private float speed = 3f;
    private GameObject spider;
    private Transform thisTransform;

	// Use this for initialization
	void Start () {
        spider = GameObject.Find("Spider");
    }
	
	// Update is called once per frame
	void Update () {
		if (!fleeing)
        {
            if (seesSpider()) //TODO spider is within sightRadius
                fleeing = true;
        }
        else
        {
            Vector3 direction = thisTransform.position - spider.GetComponent<Transform>().position;
            direction.Normalize();
            transform.position = Vector3.MoveTowards(transform.position, direction * sightradius, speed * Time.deltaTime);
            //TODO get angle at which spider approaches, then fly away at that angle.
        }
	}

    private bool seesSpider()
    {
        float distance = Vector3.Distance(thisTransform.position, spider.transform.position);
        return distance <= sightradius;
    }
}
