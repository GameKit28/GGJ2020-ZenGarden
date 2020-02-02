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
        CRACKED_ROCK,
        BROKEN_ROCK,
        ROCKY_GROUND,
    }

    public PuzzlePieceType Type { get; set; }
    public bool NeedsToCheckNeighbors { get {
            return DissallowedNeighbors != null && DissallowedNeighbors.Count > 0; 
    }}

    public HashSet<PuzzlePieceType> PlantableOn { get; internal set; }
    public HashSet<PuzzlePieceType> DissallowedNeighbors { get; internal set; }
    public HashSet<PuzzlePieceType> AffectedBy { get; internal set; }
    public PuzzlePieceType NextTypeForDamage { get; set; }



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

    internal bool IsAffectedBy(PuzzlePiece toolPuzzlePiece)
    {
        if (AffectedBy == null) return false;
        return AffectedBy.Contains(toolPuzzlePiece.Type);
    }

}
