using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile defaultTile; // Assign a default tile in the inspector
    public Color defaultColor = Color.white; // Default color to reset tiles to

    public void ResetTilemap()
    {
        BoundsInt bounds = tilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(pos))
                {
                    tilemap.SetTileFlags(pos, TileFlags.None);
                    tilemap.SetColor(pos, defaultColor);
                }
            }
        }
    }
}
