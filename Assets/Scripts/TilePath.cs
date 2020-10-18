using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePath : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject player;

    private List<Vector3> tiles;

    private void Awake()
    {
        tiles = new List<Vector3>();

        for (int i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++)
        {
            for(int j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++)
            {
                Vector3Int localPos = (new Vector3Int(i, j, (int)tilemap.transform.position.y));
                Vector3 pos = tilemap.CellToWorld(localPos);
                if (tilemap.HasTile(localPos))
                {
                    //Tile at "place"
                    tiles.Add(pos);
                    Debug.Log(pos);
                }
            }
        }


    }
}
