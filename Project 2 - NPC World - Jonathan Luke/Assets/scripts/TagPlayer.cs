using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagPlayer : Agent
{
    protected override void CalculateSteeringForces()
    {
        Wander();
        StayInBounds(3f);
        Seperate(AgentManager.Instance.tagPlayers);
    }
}
