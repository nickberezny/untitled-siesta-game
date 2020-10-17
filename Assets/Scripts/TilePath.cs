using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePath : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject player;

    private List<Vector3> availablePlaces;

    private void Awake()
    {
        availablePlaces = new List<Vector3>();

        for (int i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++)
        {
            for(int j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++)
            {
                Vector3Int localPlace = (new Vector3Int(i, j, (int)tilemap.transform.position.y));
                Vector3 place = tilemap.CellToWorld(localPlace);
                if (tilemap.HasTile(localPlace))
                {
                    //Tile at "place"
                    //availablePlaces.Add(place);
                    Debug.Log(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }

        /*
        TileBase[] allTiles = tilemap.GetTilesBlock(tilemap.cellBounds);
        foreach(TileBase tile in allTiles)
        {

        }
        */
    }
}
