using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeAnimal : Agent
{
    public float hunger = 100;

    private GameObject aM;

    private GameObject cave;
    private SpriteRenderer sr;
    public Sprite mad;
    public Sprite calm;

    private void Awake()
    {
        aM = GameObject.Find("AgentHandler");
        cave = GameObject.Find("Cave");
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    protected override void CalculateSteeringForces()
    {
        AvoidAllObstacles();
        StayInBounds(100f);
        Seperate<LargeAnimal>(aM.GetComponent<AgentManager>().largeAnimals);
        //Hibernate mode
        if (hunger >= 1)
        {
            //Seek(invisble cave agent with a high weight);
            Seek(cave.transform.position, 30f);
            Wander(2f);
            //Flee(hunters and player);
            for (int i = 0; i < aM.GetComponent<AgentManager>().hunters.Count; i++)
            {
                Flee(aM.GetComponent<AgentManager>().hunters[i].physicsObject.Position, 1f);
            }
            for (int i = 0; i < aM.GetComponent<AgentManager>().player.Count; i++)
            {
                Flee(aM.GetComponent<AgentManager>().player[i].GetComponent<PhysicsObject>().Position, 1f);
            }

            sr.sprite = calm;
        }
        else //Hungry mode
        {
            if (gameObject.GetComponent<CollidableObject>().health <= 500)
            {
                hunger = 100;
            }

            sr.sprite = mad;

            //Seek(small animals and villagers with crazy high weight)
            for (int i = 0; i < aM.GetComponent<AgentManager>().smallAnimals.Count; i++)
            {
                FindAnimal(aM.GetComponent<AgentManager>().smallAnimals[i]);
            }
            for (int i = 0; i < aM.GetComponent<AgentManager>().hunters.Count; i++)
            {
                FindAnimal(aM.GetComponent<AgentManager>().hunters[i]);
            }
            for (int i = 0; i < aM.GetComponent<AgentManager>().player.Count; i++)
            {
                Seek(aM.GetComponent<AgentManager>().player[i].GetComponent<PhysicsObject>().Position, 1f);
            }


            
        }
        hunger -= 0.01f;
    }
    
    public void Dead()
    {
        aM.GetComponent<AgentManager>().largeAnimals.Remove(gameObject.GetComponent<LargeAnimal>());
    }
}
