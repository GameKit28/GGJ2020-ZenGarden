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
    }

    public PuzzlePieceType Type { get; set; }
    public bool IsPlantable {get; set;}
}
