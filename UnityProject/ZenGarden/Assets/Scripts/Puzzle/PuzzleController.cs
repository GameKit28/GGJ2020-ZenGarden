using System;
using System.Collections;
using System.Collections.Generic;
using GameState;
using UnityEngine;
using UnityEngine.Tilemaps;
using static PuzzlePiece.PuzzlePieceType;

public class PuzzleController : MonoBehaviour
{
    static Dictionary<string, PuzzlePiece> puzzlePieceMap = new Dictionary<string, PuzzlePiece>()
    {
        { "dirt", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.GROUND}},
        { "signpost", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.SIGNPOST}},
        { "dirt_with_rocks", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.ROCKY_GROUND}},
        { "weeds", new PuzzlePiece() { Type = PuzzlePiece.PuzzlePieceType.WEED }},
        { "rock", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.ROCK,
            AffectedBy = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.PLANT,
                PuzzlePiece.PuzzlePieceType.ROSE_BUSH,
            },
            NextTypeForDamage = PuzzlePiece.PuzzlePieceType.CRACKED_ROCK
        }},
        { "rock_cracked", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.CRACKED_ROCK,
            AffectedBy = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.PLANT,
                PuzzlePiece.PuzzlePieceType.ROSE_BUSH,
            },
            NextTypeForDamage = PuzzlePiece.PuzzlePieceType.BROKEN_ROCK
        }},
        { "rock_broken", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.BROKEN_ROCK,
            AffectedBy = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.PLANT,
                PuzzlePiece.PuzzlePieceType.ROSE_BUSH,
            },
            NextTypeForDamage = PuzzlePiece.PuzzlePieceType.ROCKY_GROUND
        }},
        { "racoon", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.RACOON,
            AffectedBy = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.ROSE_BUSH,
            },
            NextTypeForDamage = PuzzlePiece.PuzzlePieceType.GROUND
        }},
        { "daisy", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.PLANT,
            PlantableOn = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.GROUND
            },
            DissallowedNeighbors= new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.WEED,
                PuzzlePiece.PuzzlePieceType.RACOON,
            },
        }},
        { "tulips", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.PLANT,
            PlantableOn = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.GROUND,
                PuzzlePiece.PuzzlePieceType.ROCKY_GROUND,
                PuzzlePiece.PuzzlePieceType.WEED
            },
            DissallowedNeighbors= new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.RACOON,
            },
        }},
        { "rose_bush", new PuzzlePiece() {
            Type = PuzzlePiece.PuzzlePieceType.ROSE_BUSH,
            PlantableOn = new HashSet<PuzzlePiece.PuzzlePieceType>()
            {
                PuzzlePiece.PuzzlePieceType.GROUND,
                PuzzlePiece.PuzzlePieceType.ROCKY_GROUND,
            }
        }}
    };

    internal void OnInventoryEmpty()
    {
        
    }

    private Tool _tool;
    public Tool tool {
        get {
            return _tool;
        }
        set
        {
            _tool = value;
            UpdateCursor();
        }
    }

    private void UpdateCursor()
    {
      mouseController.SetMouseImage((tool != null && tool.count > 0) ? tool.image : null);
    }

    private Tilemap tiles;
    private Dictionary<PuzzlePiece.PuzzlePieceType, TileBase> tileForType;
    public TileBase crackedRockTile;
    public TileBase brokenRockTile;
    public TileBase rockyGroundTile;
    public TileBase groundTile;
    public UiFeedbackController uiFeedbackController;
    private MouseController mouseController;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoopMusic("ZenGarden");
        tiles = GetComponent<Tilemap>();
        mouseController = GetComponent<MouseController>();
        mouseController.onTileClicked = OnTileClicked;
        mouseController.onTileOver = OnTileOver;
        tileForType = new Dictionary<PuzzlePiece.PuzzlePieceType, TileBase>()
        {
            { PuzzlePiece.PuzzlePieceType.CRACKED_ROCK, crackedRockTile },
            { PuzzlePiece.PuzzlePieceType.BROKEN_ROCK, brokenRockTile },
            { PuzzlePiece.PuzzlePieceType.ROCKY_GROUND, rockyGroundTile },
            { PuzzlePiece.PuzzlePieceType.GROUND, groundTile },
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
                    GameManager.Instance.PlaySoundClip("dirt_pick_01", "dirt_pick_02", "dirt_pick_04", "dirt_pick_05");
                    PaintTile(position);
                    PropogateEffects(position, toolPiece);
                    return;
                }
              
            }
            Debug.Log("Can't plant there");
            GameManager.Instance.PlaySoundClip("err_misclick");
        } 
        else
        {
            Debug.Log("No target piece");
            GameManager.Instance.PlaySoundClip("err_misclick");
        }
    }

    private void OnTileOver(Vector3Int position, TileBase tile)
    {
        uiFeedbackController.Clear();
        if(!ToolIsValid())
        {
            return;
        }

        PuzzlePiece toolPiece = puzzlePieceMap[tool.tile.name];

        PuzzlePiece targetPiece;
        if (puzzlePieceMap.TryGetValue(tile.name, out targetPiece))
        {
            if (toolPiece == targetPiece)
            {
                return;
            }
            if (toolPiece.IsPlantableOn(targetPiece))
            {
                if (CheckNeighbors(position, toolPiece))
                {
                    uiFeedbackController.ShowPlantable(position);
                    return;
                }

            }
            uiFeedbackController.ShowUnplantable(position);
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
                if (targetPuzzlePiece.Type == RACOON)
                {
                    GameManager.Instance.PlaySoundClip("racoon_01", "raccon_02", "racoon_03");
                }
                if (targetPuzzlePiece.Type == ROCK)
                {
                    GameManager.Instance.PlaySoundClip("foliage_01", "foliage_02", "foliage_03", "foliage_04");
                }
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
        UpdateCursor();
    }

    private bool ToolIsValid()
    {
        return tool != null && tool.count > 0;
    } 
}
