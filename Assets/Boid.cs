using UnityEngine;
using System.Collections;

public class Boid : MonoBehaviour {

    public Vector3 seekTarget;

    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 force;
    public float mass;
    public float maxSpeed;

    public GameObject pursueTarget;

    public bool seekEnabled;
    public bool pursueEnabled;

    public Boid()
    {
        mass = 1;
        velocity = Vector3.zero;
        force = Vector3.zero;
        acceleration = Vector3.zero;
        maxSpeed = 10.0f;

    }

	// Use this for initialization
	void Start () {
	
	}

    Vector3 Seek(Vector3 seekTarget)
    {
        Vector3 desired = seekTarget - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        return desired - velocity;
    }

    Vector3 pursue(GameObject pursueTarget)
    {
        Vector3 toTarget = pursueTarget.transform.position - transform.position;
        float distance = toTarget.magnitude;

        float time = distance / maxSpeed;
        Vector3 target =
            pursueTarget.transform.position +
            pursueTarget.GetComponent<Boid>().velocity * time;
        Debug.DrawLine(target, target + Vector3.forward);
        return Seek(target);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(seekTarget, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {

        if (pursueEnabled)
        {
            force += pursue(pursueTarget);
        }
        if (seekEnabled)
        {
            force += Seek(seekTarget);
        }        
        acceleration =  force / mass;
        velocity += acceleration * Time.deltaTime;
        Vector3.ClampMagnitude(velocity, maxSpeed);
        
        transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > float.Epsilon)
        {
            transform.forward = velocity.normalized;
        }

        force = Vector3.zero;
	}
}
