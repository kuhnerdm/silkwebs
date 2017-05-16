using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspMovement : MonoBehaviour {
    private float patrolSpeed = .02f;
    private float attackSpeed = 2f;
    private bool attacking = false;
    private float sightRadius = 3f;
    private float direction = -1f;
    private float patrolLength = 3f;
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
            if (notyetpatrolledLength <= 0)
            {
                direction = -direction;
                notyetpatrolledLength = patrolLength;

                Vector2 oldRot = this.transform.rotation.eulerAngles;
                Vector2 newRot = new Vector2(oldRot.x, oldRot.y + 180);
                this.transform.rotation = Quaternion.Euler(newRot);
            }
            pos.x += direction * (patrolSpeed + Time.deltaTime);
            notyetpatrolledLength -= patrolSpeed + Time.deltaTime;
            transform.position = pos;

            if (seesSpider())
                attacking = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spider.transform.position, attackSpeed * Time.deltaTime);

            if (transform.position.x - spider.transform.position.x >= 0)
            {
                this.transform.rotation = Quaternion.Euler(new Vector2(0f, 0f));
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(new Vector2(0f, 180f));
            }
        }

        
        
    }

    private bool seesSpider()
    {
        float distance = Vector3.Distance(transform.position, spider.transform.position);
        return distance <= sightRadius;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collwith = coll.gameObject;
        if (collwith.tag == "Spider")
            Debug.Log("Wasp hits Spider.", this);
    }
}
