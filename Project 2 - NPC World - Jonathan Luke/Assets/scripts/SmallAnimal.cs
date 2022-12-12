using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAnimal : Agent
{
    private GameObject aM;

    private void Awake()
    {
        aM = GameObject.Find("AgentHandler");
    }

    protected override void CalculateSteeringForces()
    {
        AvoidAllObstacles();

        //Flee(player,hunter,largeAnimal)
        for (int i = 0; i < aM.GetComponent<AgentManager>().hunters.Count;i++)
        {
            Flee(aM.GetComponent<AgentManager>().hunters[i].physicsObject.Position, 2f);
        }

        for (int i = 0; i < aM.GetComponent<AgentManager>().largeAnimals.Count;i++)
        {
            Flee(aM.GetComponent<AgentManager>().largeAnimals[i].physicsObject.Position, 10f);
        }

        Wander(20f);

        Seperate<SmallAnimal>(aM.GetComponent<AgentManager>().smallAnimals);

        StayInBounds(100f);
    }

    public void Dead()
    {
        aM.GetComponent<AgentManager>().smallAnimals.Remove(gameObject.GetComponent<SmallAnimal>());
    }
}
