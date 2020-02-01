using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    private Tilemap tiles;

    public delegate void OnTileClicked(Vector3Int position, TileBase tile);
    public OnTileClicked onTileClicked;

    // Start is called before the first frame update
    void Start()
    {
        tiles = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(string.Format("Co-ords of mouse is [X: {0} Y: {0}]", pos.x, pos.y));
            Vector3Int position = GetClickedPosition();
            TileBase tile = tiles.GetTile(position);

            if (tile != null)
            {
                Debug.Log(string.Format("Tile is: {0}", tile.name));
                if(onTileClicked != null)
                {
                    onTileClicked(position, tile);
                }
            }
            else
            {
                Debug.Log("No tile");
            }
        }

    }

    Vector3Int GetClickedPosition()
    {
        // save the camera as public field if you using not the main camera
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // get the collision point of the ray with the z = 0 plane
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        return tiles.WorldToCell(worldPoint);
    }
}
