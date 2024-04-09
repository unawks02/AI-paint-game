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

    // Start is called before the first frame update
    void Start()
    {
        timer = 1.0f / paintSpeed;
        paintcol = new Color(red, green, blue, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= timer)
        {
            time = time - timer; //track time here
            Vector3Int curtile = tilemap.WorldToCell(transform.position);

            tilemap.SetTileFlags(curtile, TileFlags.None);
            tilemap.SetColor(curtile, paintcol);
        }
    }
}
