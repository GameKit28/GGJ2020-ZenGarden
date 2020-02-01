using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PuzzleController : MonoBehaviour
{
    static Dictionary<string, bool> isPlantable = new Dictionary<string, bool>()
    {
        { "dirt", true }
    };

    public TileBase daisy;
    private Tilemap tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = GetComponent<Tilemap>();
        GetComponent<MouseController>().onTileClicked = OnTileClicked;
    }

    private void OnTileClicked(Vector3Int position, TileBase tile)
    {
        bool tryGetValue;
        if (isPlantable.TryGetValue(tile.name, out tryGetValue))
        {
            tiles.SetTile(position, daisy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
