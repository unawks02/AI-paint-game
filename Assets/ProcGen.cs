using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProcGen : MonoBehaviour
{
    public GameObject wallLayer;
    public GameObject floorLayer;

    private Tilemap walls;
    private Tilemap floors;
    
    public Vector3Int topLeft;
    public int dimensions;
    public int minimumCenterColumnHeight;
    public int numBoxes;
    public int numUninkableAreas;

    public Tile t;
    
    // Start is called before the first frame update
    void Start()
    {
        walls = wallLayer.GetComponent<Tilemap>();
        floors = floorLayer.GetComponent<Tilemap>();

        clearMap();

        //generate walls (theres an off by one error here somewhere, the box is too thin)
        generateColumn(0, 0, dimensions, walls, true);
        generateColumn(0, 0, dimensions, walls, false);
        generateColumn(0, dimensions, dimensions, walls, false);
        generateColumn(dimensions, 0, dimensions + 1, walls, true);


        //generate floors
        for (int i = 1; i < dimensions; i++)
        {
            generateColumn(i, 1, dimensions - 1, floors, true);
        }

        //generate center column
        var rand = new System.Random();
        int columnX = rand.Next(dimensions/3) + dimensions/3;
        int columnHeight = rand.Next(dimensions - minimumCenterColumnHeight) + minimumCenterColumnHeight;

        generateColumn(columnX, 0, columnHeight, walls, true);
        //generate offshoot column

        generateColumn(columnX, columnX, 10, walls, false);

        for (int i = 0; i < numBoxes; i++)
        {
            int x = rand.Next(dimensions);
            int y = rand.Next(dimensions);

            int width = rand.Next(5) + 2;
            int height = rand.Next(5) + 2;

            generateSquare(x, y, width, height, walls);
        }

        
        
    }

    private void clearMap()
    {
        walls.ClearAllTiles();
        floors.ClearAllTiles();
    }

    private void generateColumn(int x, int y, int length, Tilemap layer, bool vertical)
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
            
            layer.SetTile(currentCoords, t);

        }
    }


    private void generateSquare(int x, int y, int width, int height, Tilemap layer)
    {
        for (int i = x; i < x + width; i++)
        {
            generateColumn(i, y, height, layer, true);
        }
    }
}
