using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tilemap : MonoBehaviour
{
    public Console player;
    public Tilemap tile;
    public List<Vector3Int> white = new List<Vector3Int>();
    // Start is called before the first frame update
    void Start()
    {
        for (int x = -20; x < 200; x++)
            for (int y = -20; y < 200; y++)
            {
                Vector3Int posis = new Vector3Int(x, y, 0);
                tile.SetTileFlags(posis, TileFlags.None);
                tile.SetColor(posis, Color.black);
            }
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector3Int> whiting = new List<Vector3Int>();
        foreach (GameObject unit in player.myUnits)
        {
            int x = Mathf.FloorToInt(unit.transform.position.x);
            int y = Mathf.FloorToInt(unit.transform.position.y);
            for (int x0 = x - 5; x0 <= x + 5; x0++)
            {
                for (int y0 = y - 5; y0 <= y + 5;y0++)
                {
                    Vector3Int posi0 = new Vector3Int(x0, y0, 0);
                    tile.SetColor(posi0, Color.white);
                    whiting.Add(posi0);
                }
                for (int x1 = x - 4; x1 <= x + 4; x1++)
                {
                    Vector3Int posi0 = new Vector3Int(x1, y - 6, 0);
                    Vector3Int posi1 = new Vector3Int(x1, y + 6, 0);
                    tile.SetColor(posi0, Color.white);
                    whiting.Add(posi0);
                    tile.SetColor(posi1, Color.white);
                    whiting.Add(posi1);
                }
                for (int y1 = y - 4; y1 <= y + 4; y1++)
                {
                    Vector3Int posi0 = new Vector3Int(x - 6, y1, 0);
                    Vector3Int posi1 = new Vector3Int(x + 6, y1, 0);
                    tile.SetColor(posi0, Color.white);
                    whiting.Add(posi0);
                    tile.SetColor(posi1, Color.white);
                    whiting.Add(posi1);
                }
                for (int x1 = x - 1; x1 <= x + 1; x1++)
                {
                    Vector3Int posi0 = new Vector3Int(x1, y - 7, 0);
                    Vector3Int posi1 = new Vector3Int(x1, y + 7, 0);
                    tile.SetColor(posi0, Color.white);
                    whiting.Add(posi0);
                    tile.SetColor(posi1, Color.white);
                    whiting.Add(posi1);
                }
                for (int y1 = y - 1; y1 <= y + 1; y1++)
                {
                    Vector3Int posi0 = new Vector3Int(x - 7, y1, 0);
                    Vector3Int posi1 = new Vector3Int(x + 7, y1, 0);
                    tile.SetColor(posi0, Color.white);
                    whiting.Add(posi0);
                    tile.SetColor(posi1, Color.white);
                    whiting.Add(posi1);
                }
            }
        }
        foreach (Vector3Int vect in white)
        {
            bool exist = false;
            foreach (Vector3Int vect2 in whiting)
            {
                if (vect.x == vect2.x && vect.y == vect2.y)
                {
                    exist = true;
                    break;
                }
            }
            if (!exist)
                tile.SetColor(vect, Color.grey);
        }
        white = whiting;
    }
}
