using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static PuzzlePiece.PuzzlePieceType;

public class PuzzleController : MonoBehaviour
{
    static Dictionary<string, PuzzlePiece> puzzlePieceMap = new Dictionary<string, PuzzlePiece>()
    {
        { "dirt", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.GROUND , IsPlantable = true}},
        { "weeds", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.WEED }},
        { "rock", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.ROCK }}
    };

    public Tool tool;
    private Tilemap tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = GetComponent<Tilemap>();
        GetComponent<MouseController>().onTileClicked = OnTileClicked;
    }

    private void OnTileClicked(Vector3Int position, TileBase tile)
    {
        if (tool == null)
        {
            return;
        }
        PuzzlePiece tryGetValue;
        if (puzzlePieceMap.TryGetValue(tile.name, out tryGetValue))
        {
            if(tryGetValue.IsPlantable)
            {
              tiles.SetTile(position, tool.tile);
            }
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
