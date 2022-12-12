using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager Instance;

    public List<SmallAnimal> smallAnimals = new List<SmallAnimal>();
    public List<LargeAnimal> largeAnimals = new List<LargeAnimal>();
    public List<hunter> hunters = new List<hunter>();
    public List<GameObject> player = new List<GameObject>();

    [HideInInspector]
    public Vector2 maxPosition = Vector2.one;
    [HideInInspector]
    public Vector2 minPosition = Vector2.one;

    public float edgePadding = 1f;

    public SmallAnimal smallAnimalPrefab;
    public LargeAnimal largeAnimalPrefab;
    public hunter hunterPrefab;

    public GameObject playerPrefab;

    public int numSmallAnimals = 10;
    public int numLargeAnimals = 1;
    public int numHunters = 10;



    private GameObject ch;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Camera cam = Camera.main;

        if (cam != null)
        {
            Vector3 camPosition = cam.transform.position;

            float halfHeight = cam.orthographicSize;
            float halfWidth = halfHeight * cam.aspect;

            maxPosition.x = camPosition.x + halfWidth - edgePadding;
            maxPosition.y = camPosition.y + halfHeight - edgePadding;

            minPosition.x = camPosition.x - halfWidth + edgePadding;
            minPosition.y = camPosition.y - halfHeight + edgePadding;
        }

        ch = GameObject.Find("CollisionHandler");

    }

    void Start()
    {
        GameObject currentPlayer = Instantiate(playerPrefab, new Vector3(-3.14f, -3.9f), Quaternion.identity);
        player.Add(currentPlayer);
        gameObject.GetComponent<CollisionHandler>().collidableObjects.Add(currentPlayer.GetComponent<CollidableObject>());
    }

    void Update()
    {
        // if there are no more agents spawn more
        if (numSmallAnimals > smallAnimals.Count)
        {
            //semi-Random range for x and y spawn
            float minX = Random.Range(-8f, 0f);
            float minY = Random.Range(0f, 4.5f);

            SmallAnimal smallAnimal = Spawn(smallAnimalPrefab, minX, minY);
            smallAnimals.Add(smallAnimal);
            gameObject.GetComponent<CollisionHandler>().collidableObjects.Add(smallAnimal.GetComponent<CollidableObject>());
        }

        if (numHunters > hunters.Count)
        {
            //semi-Random range for x and y spawn
            float minX = Random.Range(-5f, -1f);
            float minY = Random.Range(-3f, -4.5f);
            
            hunter hunt = Spawn(hunterPrefab, minX, minY);
            hunters.Add(hunt);
            gameObject.GetComponent<CollisionHandler>().collidableObjects.Add(hunt.GetComponent<CollidableObject>());
        }        

        if (numLargeAnimals > largeAnimals.Count)
        {
            LargeAnimal bear = Spawn(largeAnimalPrefab,6.1f,4.5f);
            largeAnimals.Add(bear);
            gameObject.GetComponent<CollisionHandler>().collidableObjects.Add(bear.GetComponent<CollidableObject>());
        }

        if (player[0].GetComponent<CollidableObject>().health < 999)
        {
            player[0].GetComponent<CollidableObject>().health++;
        }
    }

    //Spawns the agent at their spawn positions
    private T Spawn<T>(T prefabToSpawn, float x = 1,float y = 1) where T : Agent
    {
        float xPosition = x;

        float yPosition = y;

        return Instantiate(prefabToSpawn, new Vector3(xPosition, yPosition), Quaternion.identity);
    }
}
