using System.Collections;
using UnityEngine;

public class PoolDemons : MonoBehaviour
{
    public static PoolDemons instance;
    public float spawnSpeed;
    private int currentSpawned;
    private int killCount;
    private int currentWave;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator StartWaves(int[] waveDemons, Vector3[] spawnpoints, GameObject[] doors, Transform parent)
    {

        if (currentSpawned < waveDemons[currentWave] && killCount < waveDemons[currentWave]) // si il reste des mobs à spawner et si ils sont pas tous mort
        {
            Spawn(transform.GetChild(0).gameObject, spawnpoints[Random.Range(0, spawnpoints.Length)], parent);
            yield return new WaitForSeconds(spawnSpeed);
            StartCoroutine(StartWaves(waveDemons, spawnpoints, doors, parent));
        }
        else if (currentWave == waveDemons.Length && killCount == waveDemons[waveDemons.Length]) // si ils sont tous morts
        {
            parent.GetComponent<StartWave>().wraithCleared = true;
            parent.GetComponent<StartWave>().OpenDoors();
            currentWave = 0;
            killCount = 0;
            currentSpawned = 0;
        }
        else if (currentSpawned == waveDemons[currentWave] && killCount == waveDemons[currentWave])// si ils sont pas tous morts mais le joueur à clear la wave
        {
            currentWave++;
            killCount = 0;
            currentSpawned = 0;
            //si wave demon fini{next wave + demon wave}
            StartCoroutine(StartWaves(waveDemons, spawnpoints, doors, parent)); //next wave

        }

    }

    public GameObject Spawn(GameObject g, Vector3 spawnpoint, Transform parent)
    {
        // if !object return null
        if (g == null)
        {
            Debug.LogError("bute les avant de me redemander ok?");

            return null;
        }
        else
        {
            g.SetActive(true);
            g.transform.position = spawnpoint;
            transform.parent = parent;
            return g;
        }

    }

    //recyclage 
    public void Restock(Demon d)
    {
        killCount++;
        d.transform.position = transform.position;
        d.FullLife();
        d.currentState = Demon.IAState.alive;
        d.gameObject.SetActive(false);
        d.transform.parent = transform;
    }
}

