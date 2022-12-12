using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hunter : Agent
{
    public bool aggresive = false;
    private GameObject aM;

    private SpriteRenderer sr;
    public Sprite calm;
    public Sprite mad;

    private void Awake() 
    {
        aM = GameObject.Find("AgentHandler");
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    
    protected override void CalculateSteeringForces()
    {
        StayInBounds(100f);
        AvoidAllObstacles();

        if (aggresive) //Aggressive mode
        {
            for (int i = 0; i < aM.GetComponent<AgentManager>().largeAnimals.Count; i++)
            {
                if (aM.GetComponent<AgentManager>().largeAnimals[i].hunger > 0)
                {
                    aggresive = false;
                    sr.sprite = calm;
                }
                Seek(aM.GetComponent<AgentManager>().largeAnimals[i].physicsObject.Position, 1f);
            }
        }
        else //Passive mode (wander and kill any small animals[walk over])
        {
            Wander(1f);
            //Flee(LargeAnimal);
            for (int i = 0; i < aM.GetComponent<AgentManager>().largeAnimals.Count; i++)
            {
                if (aM.GetComponent<AgentManager>().largeAnimals[i].hunger <= 0)
                {
                    aggresive = true;
                    sr.sprite = mad;
                }
                Flee(aM.GetComponent<AgentManager>().largeAnimals[i].physicsObject.Position, 1f);
            }
            //Seek(smallAnimal);
            for (int i = 0; i < aM.GetComponent<AgentManager>().smallAnimals.Count; i++)
            {
                FindAnimal(aM.GetComponent<AgentManager>().smallAnimals[i]);
            }
            //Seperate<hunter>(hunters);
            Seperate<hunter>(aM.GetComponent<AgentManager>().hunters);
        }
    }

    public void Dead()
    {
        aM.GetComponent<AgentManager>().hunters.Remove(gameObject.GetComponent<hunter>());
    }
}
