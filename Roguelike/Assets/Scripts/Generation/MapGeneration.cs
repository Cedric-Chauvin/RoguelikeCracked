using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGeneration : MonoBehaviour
{
    [SerializeField]
    Texture2D texture;
    [SerializeField]
    [CustomSlider("texture")]
    Vector2Int mapSize;
    [SerializeField]
    Tilemap ground;
    [SerializeField]
    Tilemap wall;
    [SerializeField]
    List<TileBase> groundTiles;
    [SerializeField]
    List<TileBase> wallTiles;
    [SerializeField]
    [Range(0,1)]
    float groundStart = 0.25f;
    [SerializeField]
    [Range(0, 1)]
    float wallStart = 0.75f;
    [SerializeField]
    [Range(0, 200)]
    float distanceBetweenPrefab = 50f;

    [SerializeField]
    TilemapPrefabs prefabGrid;
    List<Vector2Int> prefabPositions = new List<Vector2Int>();
    int prefabTry = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear()
    {
        prefabPositions.Clear();
        ground.ClearAllTiles();
        wall.ClearAllTiles();
    }

    public void Spawn()
    {
        for (int i = 0; i < mapSize.y; i++)
        {
            for (int j = 0; j < mapSize.x; j++)
            {
                Vector3Int tilePosition = new Vector3Int(i, j, 0);
                if (texture.GetPixel(i, j).grayscale > groundStart)
                {
                    ground.SetTile(tilePosition, groundTiles[0]);
                }
                if (texture.GetPixel(i, j).grayscale > wallStart)
                {
                    wall.SetTile(tilePosition, wallTiles[0]);
                }
            }
        }
        ImportPrefab(new Vector2Int(mapSize.x/2,mapSize.y/2),"Spawn");
        for (int i = 0; i < 6; i++)
        {
            ImportPrefabRandomly();
        }

    }

    private void ImportPrefabRandomly()
    {
        prefabTry++;

        int x = Random.Range(0, mapSize.x);
        int y = Random.Range(0, mapSize.y);
        Vector2Int position = new Vector2Int(x, y);
        string prefabName = "Test";
        bool canAddPrefab = true;

        int whileTry = 0;
        do
        {
            Vector2 direction = new Vector2Int();
            canAddPrefab = true;
            int nbPrefabInRange = 0;

            foreach (Vector2Int item in prefabPositions)
            {
                Vector2 dif = (position - item);
                float distance = dif.magnitude;
                if (distance < distanceBetweenPrefab)
                {
                    canAddPrefab = false;
                    direction += dif / dif.magnitude;
                    nbPrefabInRange++;
                }
            }

            Debug.Log("try: " + whileTry + "  position : " + position);

            if (nbPrefabInRange > 3 || direction.magnitude == 0)
                break;

            position += Vector2Int.RoundToInt(direction.normalized * Random.Range(distanceBetweenPrefab/2,distanceBetweenPrefab));
            Vector2Int offset = new Vector2Int(2, 2);
            position.Clamp(-offset, mapSize + offset);

            whileTry++;
        } while (!canAddPrefab && whileTry<5);
        
        if (canAddPrefab)
            ImportPrefab(position, prefabName);
        else
        {
            if (prefabTry > 50)
                Debug.LogError("Max try number reach");
            else
                ImportPrefabRandomly();
        }
        prefabTry--;
    }

    private void ImportPrefab(Vector2Int position, string name)
    {
        PrefabData prefab = prefabGrid.GetPrefab(name);
        BoundsInt bounds = prefab.bounds;
        bounds.position = Vector2to3(position) - new Vector3Int(2,2,0);
        ground.SetTilesBlock(bounds, prefab.groundTiles);
        wall.SetTilesBlock(bounds, prefab.wallTiles);
        prefabPositions.Add(position);
    }

    private Vector3Int Vector2to3(Vector2Int vector) => new Vector3Int(vector.x, vector.y, 0);
}
