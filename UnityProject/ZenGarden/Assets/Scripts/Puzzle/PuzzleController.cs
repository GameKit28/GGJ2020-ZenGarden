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
        { "dirt", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.GROUND}},
        { "dirt_with_rocks", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.ROCKY_GROUND}},
        { "weeds", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.WEED }},
        { "rock", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.ROCK }},
        { "daisy", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.PLANT,
            PlantableOn = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.GROUND
            },
            DissallowedNeighbors= new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.WEED
            },
        }},
        { "tulips", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.PLANT,
            PlantableOn = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.GROUND,
                PuzzlePiece.PuzzlePieceType.ROCKY_GROUND,
                PuzzlePiece.PuzzlePieceType.WEED
            }
        }}
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
        if (!ToolIsValid())
        {
            return;
        }

        PuzzlePiece toolPiece = puzzlePieceMap[tool.tile.name];

        PuzzlePiece targetPiece;
        if (puzzlePieceMap.TryGetValue(tile.name, out targetPiece))
        {
            if(toolPiece.IsPlantableOn(targetPiece))
            {
                if (CheckNeighbors(position, toolPiece))
                {
                    PaintTile(position);
                    PropogateEffects(position, toolPiece);
                    return;
                }
              
            }
            Debug.Log("Can't plant there");
        } 
        else
        {
            Debug.Log("No target piece");
        }
    }

    private void PropogateEffects(Vector3Int position, PuzzlePiece toolPiece)
    {
        Debug.Log("Not implemented yet");
    }

    private bool CheckNeighbors(Vector3Int position, PuzzlePiece toolPiece)
    {
        if (!toolPiece.NeedsToCheckNeighbors) return true;
        if (!CheckNeighbor(position, toolPiece, -1, 0)) return false;
        if (!CheckNeighbor(position, toolPiece, 0, 1)) return false;
        if (!CheckNeighbor(position, toolPiece, 1, 1)) return false;
        if (!CheckNeighbor(position, toolPiece, 0, -1)) return false;
        return true;
    }

    private bool CheckNeighbor(Vector3Int position, PuzzlePiece toolPiece, int xOffset, int yOffset)
    {
        TileBase tile = tiles.GetTile(new Vector3Int(position.x + xOffset, position.y + yOffset, position.z));
        if (tile == null) return true;
        PuzzlePiece targetPuzzlePiece;
        if (puzzlePieceMap.TryGetValue(tile.name, out targetPuzzlePiece))
        {
            if (toolPiece.CannotPlantNextTo(targetPuzzlePiece))
            {
                return false;
            }
        }
        return true;
    }

    private void PaintTile(Vector3Int position)
    {
        if (!ToolIsValid()) { return; }
        tiles.SetTile(position, tool.tile);
        tool.count--;
    }

    private bool ToolIsValid()
    {
        return tool != null && tool.count > 0;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
