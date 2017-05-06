using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspMovement : MonoBehaviour {
    private float patrolSpeed;
    private float attackSpeed;
    private bool attacking = false;
    private float sightRadius;
    private float direction = -1f;
    private float patrolLength;
    private float notyetpatrolledLength;
    private GameObject spider;


    // Use this for initialization
    void Start () {
        notyetpatrolledLength = patrolLength;
        spider = GameObject.Find("Spider");
    }
	
	// Update is called once per frame
	void Update () {
        if (!attacking)
        {
            Vector3 pos = transform.position;
            pos.x += direction * (patrolSpeed + Time.deltaTime);
            if (notyetpatrolledLength <= 0) //TODO check for patrolLength has been reached
            {
                direction = -direction;
                pos.x = -pos.x;
                notyetpatrolledLength = patrolLength;
            }
            notyetpatrolledLength -= direction * (patrolSpeed + Time.deltaTime);
            transform.position = pos;

            if (seesSpider())
                attacking = true;
        }
        else
        {

            //TODO attack spider
        }
        
    }

    private bool seesSpider()
    {
        float distance = Vector3.Distance(transform.position, spider.transform.position);
        return distance <= 5f;
    }
}
