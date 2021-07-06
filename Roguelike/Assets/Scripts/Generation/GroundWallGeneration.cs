using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundWallGeneration : MonoBehaviour
{
    [SerializeField]
    Texture2D texture;
    [SerializeField]
    Tilemap ground;
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
        ground.ClearAllTiles();
    }

    public void Spawn()
    {
        for (int i = 0; i < texture.height; i++)
        {
            for (int j = 0; j < texture.width; j++)
            {
                Vector3Int tilePosition = new Vector3Int(i, j, 0);
                if (texture.GetPixel(i, j).grayscale > groundStart)
                {
                    ground.SetTile(tilePosition, groundTiles[0]);
                }
                if (texture.GetPixel(i, j).grayscale > wallStart)
                {
                    ground.SetTile(tilePosition, wallTiles[0]);
                }
            }
        }
    }
}
