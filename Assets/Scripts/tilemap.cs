using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tilemap : MonoBehaviour
{
    public Tilemap tile;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void color()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3Int pos = new Vector3Int(0, 0, 0);
        Vector3Int pos2 = new Vector3Int(5, 0, 0);
        tile.GetTile(pos);
        tile.SetTileFlags(pos2, TileFlags.None);
        tile.SetColor(pos2, Color.black);
        tile.SetTileFlags(pos, TileFlags.None);
        tile.SetColor(pos, Color.gray);

    }
}
