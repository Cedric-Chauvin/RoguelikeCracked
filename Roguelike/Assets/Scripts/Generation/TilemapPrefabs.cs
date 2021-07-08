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
        foreach (var item in prefabs)
        {
            item.groundTiles = ground.GetTilesBlock(item.bounds);
            item.wallTiles = wall.GetTilesBlock(item.bounds);
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
}

