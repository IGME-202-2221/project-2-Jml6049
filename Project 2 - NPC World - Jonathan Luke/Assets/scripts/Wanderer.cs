using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Agent
{
    [Min(1f)]
    public float wanderWeight = 1f;

    [Min(1f)]
    public float stayInBoundsWeight = 3f;

    protected override void CalculateSteeringForces()
    {
        Wander(wanderWeight);
        StayInBounds(stayInBoundsWeight); 
    }
}
