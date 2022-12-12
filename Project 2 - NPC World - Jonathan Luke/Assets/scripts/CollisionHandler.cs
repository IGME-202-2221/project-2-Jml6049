using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // All Objects to consider:
    public List<CollidableObject> collidableObjects = new List<CollidableObject>();

    // Tracks collision type
    public bool circleCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Reset at the beginning of each frame
        foreach (CollidableObject collidableObject in collidableObjects)
        {
            collidableObject.isCurrentlyColliding = false;
        }


        // Check for Collisions
        for (int i = 0; i < collidableObjects.Count; i++)
        {
            for (int j = i + 1; j < collidableObjects.Count; j++)
            { 
                // Circle Collision
                if (circleCollision)
                {
                   if (CircleCollision(collidableObjects[i],collidableObjects[j])) 
                   {
                    collidableObjects[i].RegisterCollisions(collidableObjects[j]);
                   }
                }
                // AABB Collision
                else
                {
                    if (AABBCollision(collidableObjects[i],collidableObjects[j]))
                    {
                        collidableObjects[i].RegisterCollisions(collidableObjects[j]);
                    }
                }
            }
        }
    }

    // AABB Collision bool, returns true if collision occurs and false if not
    private bool AABBCollision(CollidableObject objectA, CollidableObject objectB)
    {
        if (objectA.bounds.Intersects(objectB.bounds))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CircleCollision(CollidableObject objectA,CollidableObject objectB)
    {
        double distance = Mathf.Pow((objectB.transform.position.x - objectA.transform.position.x),2)
        + Mathf.Pow((objectB.transform.position.y - objectA.transform.position.y),2);
        double radii = Mathf.Pow((objectB.radius + objectA.radius),2);
        return distance <= radii;
    }

    public void SwitchCollisionType()
    {
        circleCollision = !circleCollision;
    }

}
