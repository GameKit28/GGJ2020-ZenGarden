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
        { "rock", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.ROCK,
            AffectedBy = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.PLANT,
            },
            NextTypeForDamage = PuzzlePiece.PuzzlePieceType.CRACKED_ROCK
        }},
        { "rock_cracked", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.CRACKED_ROCK,
            AffectedBy = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.PLANT,
            },
            NextTypeForDamage = PuzzlePiece.PuzzlePieceType.BROKEN_ROCK
        }},
        { "rock_broken", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.BROKEN_ROCK,
            AffectedBy = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.PLANT,
            },
            NextTypeForDamage = PuzzlePiece.PuzzlePieceType.ROCKY_GROUND
        }},
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
    private Dictionary<PuzzlePiece.PuzzlePieceType, TileBase> tileForType;
    public TileBase crackedRockTile;
    public TileBase brokenRockTile;
    public TileBase rockyGroundTile;
    // Start is called before the first frame update
    void Start()
    {
        tiles = GetComponent<Tilemap>();
        GetComponent<MouseController>().onTileClicked = OnTileClicked;
        tileForType = new Dictionary<PuzzlePiece.PuzzlePieceType, TileBase>()
        {
            { PuzzlePiece.PuzzlePieceType.CRACKED_ROCK, crackedRockTile },
            { PuzzlePiece.PuzzlePieceType.BROKEN_ROCK, brokenRockTile },
            { PuzzlePiece.PuzzlePieceType.ROCKY_GROUND, rockyGroundTile },
        };
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

        PropogateEffects(position, toolPiece, -1, 0);
        PropogateEffects(position, toolPiece, 1, 0);
        PropogateEffects(position, toolPiece, 0, 1);
        PropogateEffects(position, toolPiece, 0, -1);
    }

    private void PropogateEffects(Vector3Int position, PuzzlePiece toolPiece, int xOffset, int yOffset)
    {
        var affectedPosition = new Vector3Int(position.x + xOffset, position.y + yOffset, position.z);
        TileBase tile = tiles.GetTile(affectedPosition);
        if (tile == null) return;
        PuzzlePiece targetPuzzlePiece;
        if (puzzlePieceMap.TryGetValue(tile.name, out targetPuzzlePiece))
        {
            if (targetPuzzlePiece.IsAffectedBy(toolPiece))
            {
                PuzzlePiece.PuzzlePieceType newType = targetPuzzlePiece.NextTypeForDamage;
                Debug.Log(String.Format("replacing {0} with {1}", targetPuzzlePiece.Type, newType));
                tiles.SetTile(affectedPosition, tileForType[newType]);
            }
        }
    }

    private bool CheckNeighbors(Vector3Int position, PuzzlePiece toolPiece)
    {
        if (!toolPiece.NeedsToCheckNeighbors) return true;
        if (!CheckNeighbor(position, toolPiece, -1, 0)) return false;
        if (!CheckNeighbor(position, toolPiece, 1, 0)) return false;
        if (!CheckNeighbor(position, toolPiece, 0, 1)) return false;
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
}
