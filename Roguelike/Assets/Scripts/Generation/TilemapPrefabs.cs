using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPrefabs : MonoBehaviour
{

    [SerializeField]
    Tilemap ground;
    [SerializeField]
    Tilemap wall;
    [SerializeField]
    List<PrefabData> prefabs;

    public PrefabData GetPrefab(string prefabName)
    {
        PrefabData pref = prefabs.Find(x => x.name == prefabName);
        if (pref == null)
            throw new UnityException();

        if(pref.groundTiles.Length == 0 || pref.wallTiles.Length == 0)
        {
            pref.groundTiles = ground.GetTilesBlock(pref.bounds);
            pref.wallTiles = wall.GetTilesBlock(pref.bounds);
        }

        return pref;       
    }

    public void UpdateInfo()
    {
        foreach (var prefab in prefabs)
        {
            prefab.groundTiles = ground.GetTilesBlock(prefab.bounds);
            prefab.wallTiles = wall.GetTilesBlock(prefab.bounds);

            foreach (var entitie in prefab.entities)
            {
                Vector3 localPosition = prefab.bounds.position + (Vector3)entitie.positionOnPattern;
                Vector3 position1 = ground.CellToWorld(Vector3Int.FloorToInt(localPosition));
                Vector3 position2 = ground.CellToWorld(Vector3Int.CeilToInt(localPosition));
                Vector3 realPosition = new Vector3();
                realPosition.x = Mathf.Lerp(position1.x, position2.x, entitie.positionOnPattern.x - Mathf.Floor(entitie.positionOnPattern.x));
                realPosition.y = Mathf.Lerp(position1.y, position2.y, entitie.positionOnPattern.y - Mathf.Floor(entitie.positionOnPattern.y));
                entitie.gameObject.transform.position = realPosition;
            }
        }
    }
}

[System.Serializable]
public class PrefabData
{
    public string name;
    public BoundsInt bounds;
    [HideInInspector]
    public TileBase[] groundTiles;
    [HideInInspector]
    public TileBase[] wallTiles;
    public List<EntityData> entities;
}

[System.Serializable]
public class EntityData
{
    public GameObject gameObject;
    public Vector2 positionOnPattern;
}

