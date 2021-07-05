using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject EnemyPrefab;

    private void Start()
    {
        StartCoroutine(AutoSpawn());
    }

    IEnumerator AutoSpawn()
    {
        while (true)
        {   
            int Rand = Random.Range(2,6);
            yield return new WaitForSeconds(Rand);
            Instantiate(EnemyPrefab, transform);
        }
    }
}
