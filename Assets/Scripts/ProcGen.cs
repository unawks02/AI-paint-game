using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProcGen : MonoBehaviour
{
    public Grid grid;

    public GameObject wallLayer;
    public GameObject floorLayer;

    private Tilemap walls;
    private Tilemap floors;
    
    public Vector3Int topLeft;
    public int dimensions;
    public int minimumCenterColumnHeight;
    public int numBoxes;
    public int numUninkableAreas;

    public Tile floorTile;
    public Tile wallTile;
    public Tile unpaintableTile;

    private Color c = new Color(0.1f, 0.0f, 0.0f, 0.0f);

    public GameObject grateSprite;

    // Start is called before the first frame update
    void Start()
    {
        walls = wallLayer.GetComponent<Tilemap>();
        floors = floorLayer.GetComponent<Tilemap>();

        clearMap();

        //generate walls (theres an off by one error here somewhere, the box is too thin)
        generateColumn(0, 0, dimensions, walls, wallTile, true);
        generateColumn(0, 0, dimensions, walls, wallTile, false);
        generateColumn(0, dimensions, dimensions, walls, wallTile, false);
        generateColumn(dimensions, 0, dimensions + 1, walls, wallTile, true);


        //generate floors
        for (int i = 1; i < dimensions; i++)
        {
            generateColumn(i, 1, dimensions - 1, floors, floorTile, true, flag:true);
        }

        //generate center column
        var rand = new System.Random();
        int columnX = rand.Next(dimensions/3) + dimensions/3;
        int columnHeight = rand.Next(dimensions - minimumCenterColumnHeight) + minimumCenterColumnHeight;

        generateColumn(columnX, 0, columnHeight, walls, wallTile, true);
        //generate offshoot column

        generateColumn(columnX, columnX, 10, walls, wallTile, false);


        //generate random boxes
        for (int i = 0; i < numBoxes; i++)
        {
            int x = rand.Next(dimensions);
            int y = rand.Next(dimensions);

            int width = rand.Next(5) + 2;
            int height = rand.Next(5) + 2;

            generateSquare(x, y, width, height, floors, unpaintableTile);
            /*GameObject g = Instantiate(grateSprite);
            g.transform.position = grid.CellToWorld(new Vector3Int(x - 5, -y + 5));
            g.transform.localScale = new Vector3(width, height, g.transform.localScale.z);*/
        }

        
        
    }

    private void clearMap()
    {
        walls.ClearAllTiles();
        floors.ClearAllTiles();
    }

    private void generateColumn(int x, int y, int length, Tilemap layer, Tile tile, bool vertical, bool flag = false)
    {
        for (int i = 0; i < length; i++)
        {
            Vector3Int currentCoords;
            if (vertical)
            {
                currentCoords = new Vector3Int(x, -y + (-1 * i)) + topLeft;
            } else
            {
                currentCoords = new Vector3Int(x + (1 * i), -y) + topLeft;  
            }
            Vector3Int unnOffset = currentCoords - topLeft;
            if (unnOffset.x < 0 || unnOffset.x > dimensions || -unnOffset.y < 0 || -unnOffset.y > dimensions)
            {
                break;
            }
            
            layer.SetTile(currentCoords, tile);
            if (flag)
            {
                layer.SetTileFlags(currentCoords, TileFlags.None);
                layer.SetColor(currentCoords, c);
            }

        }
    }


    private void generateSquare(int x, int y, int width, int height, Tilemap layer, Tile tile)
    {
        for (int i = x; i < x + width; i++)
        {
            generateColumn(i, y, height, layer, tile, true);
        }
    }
}
