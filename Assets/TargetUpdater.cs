using UnityEngine;
using System.Collections;
using BGE;

public class TargetUpdater : MonoBehaviour {

    public GameObject cube;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Boid boid = cube.GetComponent<Boid>();
            boid.seekTargetPos = transform.position + transform.forward * 20.0f;
        }
	}
}
