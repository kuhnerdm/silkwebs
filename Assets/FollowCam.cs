using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
    public float offset;
    static public FollowCam S;
    

    public bool _____________;

    public GameObject poi; //thing camera follows
		 //usually/always spider

    public float camZ;

	// Use this for initialization
	void Start () {
		S = this;
		camZ = this.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (poi == null) return;

		Vector3 destination = poi.transform.position;
		destination.z = camZ;
        destination.y = destination.y + offset;
		transform.position = destination;
	}
}
