using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

    public Vector3 targetPos;
    public float speed;

    public CubeScript()
    {
        targetPos = Vector3.zero;
        speed = 10.0f;

    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 toTarget = targetPos - transform.position;

        if (toTarget.magnitude > 1.0f)
        {
            toTarget.Normalize();
            transform.position += toTarget * speed * Time.deltaTime;
            transform.forward = toTarget;
        }
	}
}
