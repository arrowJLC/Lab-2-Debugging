using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pickups;

public class powerSpawn : MonoBehaviour
{

    public List<Transform> spawnPoints;
    public List<GameObject> collectiblePrefabs;

    void Start()
    {
        //SpawnCollectibles();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NewBehaviourScript pc = collision.gameObject.GetComponent<NewBehaviourScript>();
            SpawnCollectibles();
            Destroy(gameObject);
        }
    }
    void SpawnCollectibles()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {

            GameObject collectible = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Count)];

            Instantiate(collectible, spawnPoint.position, Quaternion.identity);
        }
    }
}

