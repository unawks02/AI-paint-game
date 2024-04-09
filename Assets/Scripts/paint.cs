using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class paint : MonoBehaviour
{
    public float paintSpeed = 10.0f; // The number of units you want to move
    public Tilemap tilemap;
    public float red = 1.0f;
    public float green = 0.0f;
    public float blue = 0.0f;
    private float timer = 1.0f;
    private float time = 0.0f;
    private Color paintcol;
    private int brushDimension = 3; //should only be set to odd numbers
    private int[,] brush;


    // Start is called before the first frame update
    void Start()
    {
        timer = 1.0f / paintSpeed;
        paintcol = new Color(red, green, blue, 1.0f);

        brush = new int[brushDimension, brushDimension];


        for (int i = 0; i < brushDimension; i++)
        {
            for (int j = 0; j < brushDimension; j++)
            {
                brush[i, j] = Random.Range(0, 2); //returns 0 or 1 at random to generate a random pattern for the brush
            }
        }
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
                        tilemap.SetTileFlags(currentTile, TileFlags.None);
                        tilemap.SetColor(currentTile, paintcol);
                    }
                }
            }

        }
    }
}
