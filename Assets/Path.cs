using BGE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Path
{
    public List<Vector3> waypoints = new List<Vector3>();

    public bool Looped;
    int next; 

    public Path()
    {
        Looped = false;
        next = 0;
    }

    public Vector3 NextWaypoint()
    {
        if (next < waypoints.Count)
        {
            return waypoints[next];
        }
        else
        {
            return Vector3.zero;
        }
    }

    public void AddWaypoint(Vector3 waypoint)
    {
        waypoints.Add(waypoint);
    }

    public void Draw()
    {
        for (int i = 1 ; i < waypoints.Count ; i ++)
        {
            LineDrawer.DrawLine(waypoints[i - 1], waypoints[i], Color.cyan);
        }
        if (Looped)
        {
            LineDrawer.DrawLine(waypoints[waypoints.Count - 1], waypoints[0], Color.cyan);
        }
    }

    public Vector3 Advance()
    {
        if (Looped)
        {
            next = (next + 1) % waypoints.Count;
        }
        else
        {
            if (next < (waypoints.Count - 1))
            {
                next++;
            }
        }

        return NextWaypoint();
    }

    public bool IsLast()
    {
        return ((! Looped) && (next == waypoints.Count - 1));
    }
}
