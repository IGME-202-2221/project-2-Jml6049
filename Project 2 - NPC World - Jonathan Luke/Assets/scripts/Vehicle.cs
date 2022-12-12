using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    // Player Speed
    public float speed = 3f;

    // Invulnerable state
    private bool isInvul = false;

    // Invincibility frames
    public float invulTime = 5f;

    // Where to spawn bullet
    private Vector3 shotPosition;

    // bullets
    public GameObject bulletPrefab;

    public Vector2 direction = Vector2.right;

    public Vector2 velocity = Vector2.zero;

    private Vector2 movementInput;

    private Vector2 mousePosition;


    // Collision Handler
    GameObject ch;

    void Awake()
    {
        ch = GameObject.Find("AgentHandler");
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = movementInput;

        //Velocity Updates
        velocity = direction * speed * Time.deltaTime;

        //Add velocity to position to move
        transform.position += (Vector3)velocity;

        //Screen Wrap
        if (transform.position.x > 10)
        {
            transform.position += new Vector3(-20, 0, 0);
        }

        if (transform.position.x < -10)
        {
            transform.position += new Vector3(20, 0, 0);
        }

        if (transform.position.y > 5)
        {
            transform.position += new Vector3(0, -10, 0);
        }

        if (transform.position.y < -5)
        {
            transform.position += new Vector3(0, 10, 0);
        }
    }

    public void OnMove(InputAction.CallbackContext moveContext)
    {
        movementInput = moveContext.ReadValue<Vector2>();
    }

    /// <summary>
    /// Fires a bullet from the position at the moment of press
    /// </summary>
    /// <param name="fireContext"></param>
    public void OnFire(InputAction.CallbackContext fireContext)
    {
        if (fireContext.canceled)
        {
            shotPosition = transform.position;

            GameObject bullet = Instantiate(bulletPrefab, shotPosition, Quaternion.identity);

            // sets bullet direction
            BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();
            bulletBehavior.direction = mousePosition;

            // converts the bullet to a collidable object
            CollidableObject bulletCollidable = bullet.GetComponent<CollidableObject>();

            // adds to collision handler list
            ch.GetComponent<CollisionHandler>().collidableObjects.Add(bulletCollidable);
        }
    }

    public void OnRoll(InputAction.CallbackContext rollContext)
    {
        if (isInvul) return;
        if (rollContext.canceled)
        {
            StartCoroutine(Roll());
        }
    }

    private IEnumerator Roll()
    {
        isInvul = true;

        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(invulTime);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        isInvul = false;
    }
}
