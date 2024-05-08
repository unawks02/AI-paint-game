using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class paint : MonoBehaviour
{
    public float paintSpeed = 10.0f; // The number of units you want to move
    public Tilemap tilemap;
    public Tilemap maskLayer;
    public Tile t;
    public Tile unpaintableTile;
    public TilemapMask mask;
    public float red = 1.0f;
    public float green = 0.0f;
    public float blue = 0.0f;
    private float timer = 1.0f;
    private float time = 0.0f;
    private Color paintcol;
    private int brushDimension = 3; //should only be set to odd numbers
    private int[,] brush;

    private Color redC = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    private Color greenC = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    private Color blueC = new Color(0.0f, 0.0f, 1.0f, 1.0f);
    private Color whiteC = new Color(0.1f, 0.0f, 0.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        timer = 1.0f / paintSpeed;
        paintcol = new Color(red, green, blue, 0.392f);

        //brush = new int[brushDimension, brushDimension]; now get from brush obj

        GameObject[] masterBrush;
        masterBrush = GameObject.FindGameObjectsWithTag("brush");

        if (masterBrush.Length == 0)
        {
            //Debug.Log("Brush not found");
            brush = new int[brushDimension, brushDimension];

            for (int i = 0; i < brushDimension; i++)
            {
                for (int j = 0; j < brushDimension; j++)
                {
                    brush[i, j] = Random.Range(0, 2); //returns 0 or 1 at random to generate a random pattern for the brush
                }
            }
        }
        else
        {
            //Debug.Log("Brush found");
            brush = masterBrush[0].GetComponent<brushArray>().array;
        }
    }

    public Color PaintColor
    {
        get { return paintcol; }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= timer)
        {
            time = time - timer; //track time here
            Vector3Int topLeftTile = tilemap.WorldToCell(transform.position) - new Vector3Int((brushDimension / 2), (brushDimension / 2));

            //paints brush to tilemap based off player location
            for (int i = 0; i < brushDimension; i++)
            {
                for (int j = 0; j < brushDimension; j++)
                {
                    if (brush[i, j] == 1)
                    {
                        Vector3Int currentTile = topLeftTile + new Vector3Int(i, j);
                        if (tilemap.GetTile(currentTile) == null || tilemap.GetTile(currentTile).Equals(unpaintableTile))
                        {
                            continue; //uninkables
                        }
                        tilemap.SetTileFlags(currentTile, TileFlags.None);
                        //maskLayer.SetTileFlags(currentTile, TileFlags.None);
                        Color oldCol = tilemap.GetColor(currentTile);

                        tilemap.SetColor(currentTile, paintcol);
                        maskLayer.SetTile(currentTile, t);

                        //decrements old color
                        if (Extension.colEq(oldCol, redC)) 
                        {
                            tilemap.GetComponent<sumTiles>().redCount--;
                        }
                        else if (Extension.colEq(oldCol, greenC))
                        { 
                            tilemap.GetComponent<sumTiles>().greenCount--;
                        }
                        else if (Extension.colEq(oldCol, blueC))
                        {
                            tilemap.GetComponent<sumTiles>().blueCount--;
                        }
                        //increments new color
                        if (Extension.colEq(paintcol, redC))
                        {
                            tilemap.GetComponent<sumTiles>().redCount++;
                        }
                        else if (Extension.colEq(paintcol, greenC))
                        {
                            tilemap.GetComponent<sumTiles>().greenCount++;
                        }
                        else if (Extension.colEq(paintcol, blueC))
                        {
                            tilemap.GetComponent<sumTiles>().blueCount++;
                        }
                    }
                }
            }
            mask.GenerateMask();
        }
    }
}

static class Extension
{
    public static bool colEq(this Color x, Color y)
    {
        return Mathf.Approximately(x.r, y.r) && Mathf.Approximately(x.g, y.g) && Mathf.Approximately(x.b, y.b);
    }
}

