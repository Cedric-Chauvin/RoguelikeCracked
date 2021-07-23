using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Singleton
    private static SpawnManager instance = null;
    public static SpawnManager Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    public GameObject EnemyPrefab = null;
    public List<SpawnData> spawnDatas = new List<SpawnData>();
    private SpawnData currentSpawnData = null;

    private Transform player = null;
    private float timer = 0;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentSpawnData = spawnDatas.Find(x => x.spawnType == "Normal");
    }

    // Update is called once per frame
    void Update()
    {
        SpawnUpdate();
    }

    private void SpawnUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        if (currentSpawnData == null || currentSpawnData.spawnSecond == 0)
            return;

        float distance = Random.Range(currentSpawnData.spawnRadius, currentSpawnData.destroyRadius);
        float angle = Random.Range(0, 360);
        Vector2 offset =new Vector2(distance, 0).Rotate(angle);
        Vector2 spawnPosition = MapGeneration.Instance.FindSpawnablePosition((Vector2)player.position + offset);
        Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
        timer = 1 / currentSpawnData.spawnSecond;
    }

    public void ChangeSpawnData(string spawnType)
    {
        SpawnData data = spawnDatas.Find(x => x.spawnType == spawnType);
        if (data == null)
            Debug.LogError("Spawn data not find");
        else
            currentSpawnData = data;
    }
}


[System.Serializable]
public class SpawnData
{
    public string spawnType;
    public float spawnRadius;
    public float destroyRadius;
    public float spawnSecond;
}

