using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneEnemySpawner : MonoBehaviour
{
    public bool enabled;
    public float timeBetweenSpawns = 10.0f;

    public GameObject cloneEnemy;

    private List<GameObject> spawnedClones = new List<GameObject>();
    private int clonesSpawned = 1;
    private float timeSinceLastSpawn = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            if (clonesSpawned < LevelManager.Instance.loops)
            {
                if (timeSinceLastSpawn > 0.0f)
                {
                    timeSinceLastSpawn = Mathf.Max(0.0f, timeSinceLastSpawn - Time.deltaTime);
                }
                else
                {
                    Vector3 position = transform.position + new Vector3(Random.Range(-6.0f, 6.0f), 0.0f, 0.0f);
                    GameObject newCloneEnemy = Instantiate(cloneEnemy, position, transform.rotation);
                    spawnedClones.Add(newCloneEnemy);
                    clonesSpawned++;

                    timeSinceLastSpawn = timeBetweenSpawns;
                }
            }
            else
            { // If all enemies have been spawned, check if all clones are disabled, if so, that means the player has defeated all enemies, so we can end the game
                bool canEnd = true;
                foreach (GameObject clone in spawnedClones)
                {
                    if (clone.activeSelf)
                    {
                        canEnd = false;
                        break;
                    }
                }

                if (canEnd)
                {
                    LevelManager.Instance.EndGame();
                }
            }
        }
    }

    public void EnableSpawner()
    {
        enabled = true;
        clonesSpawned = 1;
    }

    public void DisableSpawner()
    {
        enabled = false;
        foreach(GameObject clone in spawnedClones)
        {
            Destroy(clone, 0.0f);
        }
    }
}
