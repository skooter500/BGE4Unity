using UnityEngine;
using System.Collections;

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
            boid.seekTarget = transform.position + transform.forward * 20.0f;
        }
	}
}
