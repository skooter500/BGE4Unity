using UnityEngine;
using System.Collections;
using BGE;

public class Boid : MonoBehaviour {

    [Header("Seek")]
    public Vector3 seekTarget;
    public bool seekEnabled;

    [Header("Arrive")]    
    public Vector3 arriveTarget;

    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 force;
    public float mass;
    public float maxSpeed;

    public GameObject pursueTarget;

    public Path path;
    
    public bool pursueEnabled;
    public bool arriveEnabled;

    public bool offsetPursueEnabled;
    public GameObject offsetPursueTarget;
    public Vector3 offset;
    
    [Header("Path Following")]
    public bool pathFollowingEnabled;
    public bool Looped;
    
    public Boid()
    {
        mass = 1;
        velocity = Vector3.zero;
        force = Vector3.zero;
        acceleration = Vector3.zero;
        maxSpeed = 10.0f;

        path = new Path();
        Looped = false;

    }

	// Use this for initialization
	void Start () {
	    if (offsetPursueEnabled)
        {
            if (offsetPursueTarget != null)
            {
                offset = offsetPursueTarget.transform.position - transform.position;
            }
        }
        path.Looped = Looped;
	}

    Vector3 FollowPath()
    {
        Vector3 next = path.NextWaypoint();
        float dist = (transform.position - next).magnitude;
        float waypointDistance = 5;
        if (dist < waypointDistance)
        {
            next = path.Advance();
        }
        if (! path.Looped && path.IsLast())
        {
            return Arrive(next);
        }
        else
        {
            return Seek(next);
        }
    }

    Vector3 OffsetPursue(GameObject offsetPursueTarget)
    {
        Vector3 targetPos = offsetPursueTarget.transform.TransformPoint(offset);

        Vector3 toTarget = targetPos - transform.position;
        float distance = toTarget.magnitude;
        float time = distance / maxSpeed;
        Vector3 target = targetPos
            + offsetPursueTarget.GetComponent<Boid>().velocity * time;

        LineDrawer.DrawTarget(target, Color.gray);

        return Arrive(target);
    }

    Vector3 Seek(Vector3 seekTarget)
    {
        Vector3 desired = seekTarget - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        LineDrawer.DrawTarget(seekTarget, Color.blue);
        return desired - velocity;
    }

    Vector3 Arrive(Vector3 arriveTarget)
    {
        Vector3 toTarget = arriveTarget - transform.position;

        float distance = toTarget.magnitude;

        float slowingDistance = 10;

        LineDrawer.DrawSphere(arriveTarget, slowingDistance, 10, Color.yellow);
        
        float ramped = (distance / slowingDistance) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = (toTarget / distance) * clamped;
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
        if (arriveEnabled)
        {
            force += Arrive(arriveTarget);
        }

        if (offsetPursueEnabled)
        {
            force += OffsetPursue(offsetPursueTarget);
        }
        if (pathFollowingEnabled)
        {
            path.Draw();
            force += FollowPath();
        }
        acceleration =  force / mass;
        velocity += acceleration * Time.deltaTime;
        Vector3.ClampMagnitude(velocity, maxSpeed);

        
        
        transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > float.Epsilon)
        {
            transform.forward = velocity.normalized;
            velocity *= 0.99f;
        }

        force = Vector3.zero;
	}
}
