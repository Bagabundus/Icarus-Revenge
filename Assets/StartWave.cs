using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWave : MonoBehaviour
{
    public int[] wraith;
    public int[] demon;
    public Vector3[] spawnPoints;
    public GameObject[] doors;
    public bool wraithCleared;
    public bool demonsCleared;
    public void OpenDoors()
    {
        if(wraithCleared && demonsCleared)
        {
            foreach (GameObject g in doors)
            {
                g.SetActive(false);
            }
        }
    }
    public void CloseDoors()  { foreach(GameObject g in doors)    { g.SetActive(true); } }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
          PoolWraith.instance.StartWaves(wraith, spawnPoints, doors, transform);
          //PoolDemons.instance.StartWaves(demon,spawnPoint);
          GetComponent<BoxCollider>().enabled = false;
          CloseDoors();
        }
  
    }
}
