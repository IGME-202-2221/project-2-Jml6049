using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    GameObject ch;

    // Start is called before the first frame update
    public float speed;

    public Vector3 direction;

    // Bound to destroy after firing
    private Vector3 maxPosition;

    private Vector3 minPosition; 

    void Start()
    {
       minPosition = Camera.main.ScreenToWorldPoint(Vector3.zero);
       maxPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

       ch = GameObject.Find("AgentHandler");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        

        if (transform.position.y > maxPosition.y || transform.position.y < minPosition.y)
        {
            Destroy(gameObject);
            ch.GetComponent<CollisionHandler>().collidableObjects.Remove(gameObject.GetComponent<CollidableObject>());
        }
    }
}
