using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSilkBehavior : MonoBehaviour
{

    /// <summary>
    /// Prefab for the hook point (the point on the object that the hook grabs on to)
    /// </summary>
    public GameObject silkAttachmentPointPrefab;

    public GameObject silkThreadPrefab;

    public GameObject attachedThread;

    // Use this for initialization
    void Start()
    {
        attachedThread = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && attachedThread == null && (FacingLeft || FacingRight))
        {
            GameObject player = transform.gameObject;  // The script should be placed on the spider.
            Vector2 spiderAimDirection;
            if (FacingLeft)
            {
                spiderAimDirection = player.transform.rotation * Vector2.left;
            }
            else if (FacingRight)
            {
                spiderAimDirection = player.transform.rotation * Vector2.right;
            }
            else
            {
                throw new System.Exception();
            }

            // Check if there is an object to attach silk to.
            // (Distance of 1 in raycast was picked arbitrarily but seems to work for now.)
            GetComponent<Collider2D>().enabled = false;
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, spiderAimDirection, 4);
            GetComponent<Collider2D>().enabled = true;

            if (hit.collider != null)
            {
                Vector2 destination = hit.point;

                // Instatiate an attachment point on the object. (This is what the hook will grab on to.)
                GameObject attachmentPoint = Instantiate(silkAttachmentPointPrefab, destination, Quaternion.identity) as GameObject;
                attachmentPoint.transform.parent = hit.transform;

                // Instatiate an empty silk thread.
                GameObject attachedSilk = Instantiate(silkThreadPrefab, destination, Quaternion.identity) as GameObject;
                attachedSilk.GetComponent<SilkThreadBehavior>().InitialAttach(player, attachmentPoint);
            }
        }
    }

    /// <summary>
    /// True if the spider is facing right.
    /// </summary>
    bool FacingRight
    {
        get
        {
            return transform.rotation.y < 45.0f;
        }
    }

    /// <summary>
    /// True if the spider is facing left.
    /// </summary>
    bool FacingLeft
    {
        get
        {
            return transform.rotation.y > 135.0f;
        }
    }
}
