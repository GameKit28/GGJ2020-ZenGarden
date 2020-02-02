using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UiFeedbackController : MonoBehaviour
{
    private Tilemap uiTilemap;
    public TileBase goodTile;
    public TileBase badTile;

    // Start is called before the first frame update
    void Start()
    {
        uiTilemap = GetComponent<Tilemap>();
        Clear();
    }

    public void Clear()
    {
        uiTilemap.origin = Vector3Int.zero;
        uiTilemap.size = Vector3Int.zero;
        uiTilemap.ResizeBounds();
    }

    internal void ShowPlantable(Vector3Int position)
    {
        uiTilemap.SetTile(position, goodTile);
    }

    internal void ShowUnplantable(Vector3Int position)
    {
        uiTilemap.SetTile(position, badTile);
    }
}
