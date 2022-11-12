using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager Instance;

    [HideInInspector]
    public List<TagPlayer> tagPlayers = new List<TagPlayer>();

    [HideInInspector]
    public Vector2 maxPosition = Vector2.one;
    [HideInInspector]
    public Vector2 minPosition = Vector2.one;

    public float edgePadding = 1f;

    public TagPlayer tagPlayerPrefab;

    public int numTagPlayers = 10;

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

        for (int i = 0; i < numTagPlayers; i++)
        {
            tagPlayers.Add(Spawn(tagPlayerPrefab));
        }
    }

    private T Spawn<T>(T prefabToSpawn) where T : Agent
    {
        float xPosition = Random.Range(minPosition.x, maxPosition.x);

        float yPosition = Random.Range(minPosition.y, maxPosition.y);

        return Instantiate(prefabToSpawn, new Vector3(xPosition, yPosition), Quaternion.identity);
    }
}
