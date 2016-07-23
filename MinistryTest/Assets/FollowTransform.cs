using UnityEngine;
using System.Collections;

public class FollowTransform : MonoBehaviour {

    public Transform t;
    public float localRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (t)
        {
            this.transform.position = t.position;
            this.transform.rotation = t.transform.rotation;
            this.transform.Rotate(new Vector3(0, 0, localRotation));
        }
	}
}
