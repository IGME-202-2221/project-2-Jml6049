using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
     SpriteRenderer spriteRenderer;

    private Vector3 position; 

    public Bounds bounds;

    public float radius = 1f; 

    public bool isCurrentlyColliding;

    //Health
    public int health = 1;

    //Collision Handler
    GameObject ch;

    //List of all objects this instance is colliding with
    public List<CollidableObject> collisions = new List<CollidableObject>();

    // Start is called before the first frame update
    void Start()
    {
        ch = GameObject.Find("AgentHandler");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        position = transform.position;
        bounds = new Bounds(position, spriteRenderer.bounds.extents);

        if (isCurrentlyColliding)
        {
            foreach (CollidableObject c in collisions)
            {
                if (c.gameObject.tag != gameObject.tag)
                {
                    StartCoroutine(dmgColor());
                }
            }
        }

        if (health <= 0)
        {
            ch.GetComponent<CollisionHandler>().collidableObjects.Remove(gameObject.GetComponent<CollidableObject>());
            switch(gameObject.tag)
            {
                case "Hunter":
                    gameObject.GetComponent<hunter>().Dead();
                    break;
                case "LargeAnimal":
                    gameObject.GetComponent<LargeAnimal>().Dead();
                    break;
                case "SmallAnimal":
                    gameObject.GetComponent<SmallAnimal>().Dead();
                    break;
            }
            Destroy(gameObject);
        }
        collisions.Clear();
    }


    public void RegisterCollisions(CollidableObject other)
    {
        isCurrentlyColliding = true;
        other.isCurrentlyColliding = true;
        collisions.Add(other);
    }
   
   private void OnDrawGizmos()
   {
        if (isCurrentlyColliding)
        {
            Gizmos.color = Color.red;
        }
        else 
        {
            Gizmos.color = Color.black;
        }

        // Gizmos.DrawWireCube(bounds.center,bounds.extents);
        Gizmos.DrawWireSphere(transform.position,radius);
   }

    private IEnumerator dmgColor()
    {
        spriteRenderer.color = Color.red;

        health--;
        yield return new WaitForSeconds(1f);

        spriteRenderer.color = Color.white;
    }
}
