using UnityEngine;
using System.Collections;

public class PathSetup : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Path path = GetComponent<Boid>().path;

        path.AddWaypoint(new Vector3(10, 0, 10));
        path.AddWaypoint(new Vector3(50, 0, 10));
        path.AddWaypoint(new Vector3(50, 0, 70));
        path.AddWaypoint(new Vector3(0, 0, 20));

        gameObject.renderer.material.color = Color.cyan;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
