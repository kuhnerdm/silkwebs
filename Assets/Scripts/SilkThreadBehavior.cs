﻿using System.Collections.Generic;
using UnityEngine;

public class SilkThreadBehavior : MonoBehaviour {

    public GameObject silkNodePrefab;

    /// <summary>
    /// Distance between silk thread nodes.
    /// </summary>
    public float nodeDistance = 0.1f;

    /// <summary>
    /// List of silk links that make up the thread
    /// </summary>
    public List<GameObject> threadNodes;

    #region Unity methods

    /// Use this for initialization
    void Start () {
        threadNodes = new List<GameObject>();
    }

    /// Update is called once per frame
    void Update () {
        RenderLine();
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Perform the initial attachment of the silk between the spider and some attachment point.
    /// </summary>
    public void InitialAttach(GameObject spider, GameObject attachmentPoint)
    {
        // Create the first node at the attachment point. Link the attachment point to the first node.
        GameObject firstNode = Instantiate(silkNodePrefab, attachmentPoint.transform.position, Quaternion.identity) as GameObject;
        firstNode.transform.parent = ThisThread.transform;
        attachmentPoint.GetComponent<HingeJoint2D>().connectedBody = firstNode.GetComponent<Rigidbody2D>();

        Vector2 attachmentToSpider = spider.transform.position - attachmentPoint.transform.position;
        float attachmentToSpiderDist = attachmentToSpider.magnitude;
        attachmentToSpider.Normalize();

        GameObject prevNode = firstNode;
        GameObject nextNode = null;

        for (float attachmentToNextDist = nodeDistance; 
             attachmentToNextDist < attachmentToSpiderDist;
             attachmentToNextDist += nodeDistance)
        {
            Vector2 toNextPos = attachmentToSpider * attachmentToNextDist;
            Vector2 nextNodePos = (Vector2)attachmentPoint.transform.position + toNextPos;
            nextNode = Instantiate(silkNodePrefab, nextNodePos, Quaternion.identity) as GameObject;
            nextNode.transform.parent = ThisThread.transform;
            prevNode.GetComponent<HingeJoint2D>().connectedBody = nextNode.GetComponent<Rigidbody2D>();
            prevNode = nextNode;
        }

        GameObject lastNode = (nextNode == null) ? firstNode : nextNode;
        lastNode.GetComponent<HingeJoint2D>().connectedBody = spider.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Extend the silk thread by adding new nodes to the front.
    /// </summary>
    public void Extend()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region Private properties

    /// <summary>
    /// The silk thread to which this script is attached.
    /// </summary>
    private GameObject ThisThread { get { return transform.gameObject; } }

    /// <summary>
    /// The silk node at the front end of the thread (the end that is extended by <see cref="Extend"/>)
    /// </summary>
    private GameObject FirstNode { get { return threadNodes[0]; } }

    /// <summary>
    /// The silk node at the back end of the thread (the end of the thread that is not extended by <see cref="Extend"/>) 
    /// </summary>
    private GameObject LastNode { get { return threadNodes[threadNodes.Count - 1]; } }

    private LineRenderer LineRenderer { get { return GetComponent<LineRenderer>(); } }

    #endregion

    #region Private methods

    private void RenderLine()
    {
        LineRenderer.numPositions = threadNodes.Count;

        for (int i = 0; i < threadNodes.Count; i++)
        {
            LineRenderer.SetPosition(i, threadNodes[i].transform.position);
        }
    }

    #endregion
}