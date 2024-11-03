using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject coin; //game object to spawn; prefab template 
    public float spawnRange = 0.25f; //radius from the spawner center
    public int max = 10; //max number of objects to spawn

    private GameObject[] coins; //for keeping track


    // Start is called before the first frame update; similar to C# game and spawning platforms
    void Start()
    {
        coins = new GameObject[max]; //10

        //initialize and spawn collectibles
        for (int i = 0; i < max; i++)
        {
            SpawnCollectible(); //calls method
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < max; i++) //check for inactive & respawn them
        {
            if (coins[i] != null && !coins[i].activeInHierarchy)
            {
                SpawnCollectible(i);
            }
        }
    }

    //for spawning objects
    private void SpawnCollectible(int index = -1) //initiialized w/ -1 so that when called it'll find first null entry to spawn
    {
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRange;
        randomPosition.y = Terrain.activeTerrain.SampleHeight(randomPosition); //so that it sits on terrain

        GameObject collectible = Instantiate(coin, randomPosition, Quaternion.identity); //prefab template

        if (index == -1) //called without specified index 
        {
            for (int i = 0; i < max; i++)
            {
                if (coins[i] == null)
                {
                    collectible.SetActive(true);
                    coins[i] = collectible;
                    break;
                }
            }
        }

        else //using specified index 
        {
            collectible.SetActive(true);
            coins[index] = collectible;
        }
    }
}
