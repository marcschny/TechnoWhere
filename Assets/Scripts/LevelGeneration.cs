using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGeneration : MonoBehaviour
{
    public int policeCount;

    public GameObject tree;
    public GameObject police;
    public GameObject player;
    public GameObject party;

    public List<GameObject> spawnPoints = new List<GameObject>();
    private GameObject tempGameObject;

    // Start is called before the first frame update
    void Start()
    {
        createSpawnPoints();
        ShuffleSpawnPoints();
        Instantiate(player, spawnPoints[0].transform.position, Quaternion.identity);
        Instantiate(party, spawnPoints[1].transform.position, Quaternion.identity);

        for (int i = 2; i < policeCount + 2; i++)
        {
            //Instantiate(police, spawnPoints[i].transform.position, Quaternion.identity);
            Instantiate(police, spawnPoints[i].transform.position, Quaternion.identity);
        }

        for (int i = policeCount + 2; i < spawnPoints.Count; i++)
        {
            Instantiate(tree, spawnPoints[i].transform.position, Quaternion.identity);
        }
    }

    public void ShuffleSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            int rnd = Random.Range(i, spawnPoints.Count);
            tempGameObject = spawnPoints[rnd];
            spawnPoints[rnd] = spawnPoints[i];
            spawnPoints[i] = tempGameObject;
        }
    }

    public void createSpawnPoints()
    {
        for (int i = 5; i < 105; i += 10)
        {
            for (int j = 5; j < 105; j += 10)
            {
                Vector2 randPosInGrid = new Vector2(i, j) + Random.insideUnitCircle * 5;
                spawnPoints.Add(Instantiate(new GameObject("spawnPointGenerated"), randPosInGrid,
                    Quaternion.identity));
            }
        }
    }
}