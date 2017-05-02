using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silk : MonoBehaviour {

    /// <summary>
    /// Distance between nodes.
    /// </summary>
    public float nodeSpacing = 0.2f;
    public GameObject nodePrefab;

    [HideInInspector] public GameObject player;
    [HideInInspector] public LineRenderer lineRenderer; 
    [HideInInspector] public List<GameObject> nodes = new List<GameObject>();

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");

        GameObject lastNode = transform.gameObject;  // The last node is initially the hook.
        Vector2 hookToPlayer = player.transform.position - transform.position;

        for (float d = nodeSpacing; d < hookToPlayer.magnitude; d += nodeSpacing)
        {
            Vector2 nextNodePos = (Vector2) transform.position + (hookToPlayer.normalized * d);
            GameObject nextNode = Instantiate(nodePrefab, new Vector3(nextNodePos.x, nextNodePos.y, 0.0f), 
                                              Quaternion.identity);
            nextNode.transform.SetParent(transform);

            lastNode.GetComponent<HingeJoint2D>().connectedBody = nextNode.GetComponent<Rigidbody2D>();
            lastNode = nextNode;

            nodes.Add(lastNode);
        }

        lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        RenderLine();
	}

    void RenderLine()
    {
        lineRenderer.numPositions = nodes.Count + 2;

        lineRenderer.SetPosition(0, transform.position);

        for (int i = 0; i < nodes.Count; i++)
        {
            lineRenderer.SetPosition(i+1, nodes[i].transform.position);
        }

        lineRenderer.SetPosition(nodes.Count + 1, player.transform.position);
    }
}
