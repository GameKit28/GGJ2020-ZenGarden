using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece
{
    public enum PuzzlePieceType
    {
        GROUND,
        EDGE,
        PLANT,
        WEED,
        ROCK,
        ROCKY_GROUND,
    }

    public PuzzlePieceType Type { get; set; }
    public bool NeedsToCheckNeighbors { get {
            return DissallowedNeighbors != null && DissallowedNeighbors.Count > 0; 
    }}

    public HashSet<PuzzlePieceType> PlantableOn { get; internal set; }
    public HashSet<PuzzlePieceType> DissallowedNeighbors { get; internal set; }

    internal bool IsPlantableOn(PuzzlePiece targetPiece)
    {
        if (PlantableOn == null) return false;
        return PlantableOn.Contains(targetPiece.Type);
    }

    internal bool CannotPlantNextTo(PuzzlePiece targetPiece)
    {
        if (DissallowedNeighbors == null) return false;
        return DissallowedNeighbors.Contains(targetPiece.Type);
    }
}
