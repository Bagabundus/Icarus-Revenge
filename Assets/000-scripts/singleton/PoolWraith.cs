using System.Collections;
using UnityEngine;

public class PoolWraith : MonoBehaviour
{
    public static PoolWraith instance;
    public float spawnSpeed;
    private int currentSpawned;
    private int killCount;
    private int currentWave;
    
    private void Awake()
    {
        instance = this;
    }




    //spawn en fonction des waves
    public IEnumerator StartWaves(int[] wavesWraith,Vector3[] spawnpoints,GameObject[] doors,Transform parent)
    {
        Debug.Log("test");
        if (currentSpawned < wavesWraith[currentWave] && killCount < wavesWraith[currentWave]) // si il reste des mobs à spawner et si ils sont pas tous mort
        {
            Spawn(transform.GetChild(0).gameObject, spawnpoints[Random.Range(0, spawnpoints.Length)], parent);
            yield return new WaitForSeconds(spawnSpeed);
            StartCoroutine(StartWaves(wavesWraith, spawnpoints, doors, parent));
        }
        else if (currentWave == wavesWraith.Length && killCount == wavesWraith[wavesWraith.Length]) // si ils sont tous morts
        {
            parent.GetComponent<StartWave>().wraithCleared = true;
            parent.GetComponent<StartWave>().OpenDoors();
            currentWave = 0;
            killCount = 0;
            currentSpawned = 0;
        }
        else if (currentSpawned == wavesWraith[currentWave] && killCount == wavesWraith[currentWave])// si ils sont pas tous morts mais le joueur à clear la wave
        {
            currentWave++;
            killCount = 0;
            currentSpawned = 0;
            //si wave demon fini{next wave + demon wave}
            StartCoroutine(StartWaves(wavesWraith, spawnpoints, doors, parent)); //next wave

        }

    }





    //ce qui se passe quand ça spawn
    public GameObject Spawn(GameObject g, Vector3 spawnpoint,Transform parent)
    {
        // if !object return null
        if(g == null)
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
    public void Restock(IADamnWraith w)
    {
        killCount++;
        w.transform.position = transform.position;
        w.FullLife();
        w.gameObject.SetActive(false);
        w.transform.parent = transform;
        
    }


   
}
