using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silk : MonoBehaviour {

    public Vector2 destination;
    public float speed = 0.5f;
    public float distance = 2;
    public GameObject nodePrefab;
    public GameObject player;
    public GameObject lastNode;
    internal bool done = false;

    public List<GameObject> nodes = new List<GameObject>();
    public LineRenderer lineRenderer; 

    private int vertexCount
    {
        get
        {
            return nodes.Count + 2;
        }
    }

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        lastNode = transform.gameObject;  // The last node is initially the hook.
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);

        if ((Vector2)transform.position != destination)
        {
            if (Vector3.Distance(player.transform.position, lastNode.transform.position) > distance)
            {
                CreateNode();
            }
        }
        else if (!done)
        {
            done = true;
            lastNode.GetComponent<HingeJoint2D>().connectedBody = player.GetComponent<Rigidbody2D>();
        }

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

    void CreateNode()
    {
        Vector3 pos2Create = player.transform.position - lastNode.transform.position;
        pos2Create.Normalize();
        pos2Create *= distance;
        pos2Create += lastNode.transform.position;
        GameObject nextNode = Instantiate(nodePrefab, pos2Create, Quaternion.identity);

        nextNode.transform.SetParent(transform);

        lastNode.GetComponent<HingeJoint2D>().connectedBody = nextNode.GetComponent<Rigidbody2D>();
        lastNode = nextNode;

        nodes.Add(lastNode);
    }
}
