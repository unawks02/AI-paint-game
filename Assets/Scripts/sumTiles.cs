using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class sumTiles : MonoBehaviour
{
    public Tilemap tilemap;
    public TextMeshProUGUI redText;
    public TextMeshProUGUI greenText;
    public TextMeshProUGUI blueText;
    private float total = 0.0f;
    public float redCount = 0.0f;
    public float greenCount = 0.0f;
    public float blueCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //count all white tiles
        tilemap.CompressBounds(); // To only read the tiles that we have painted
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Tile tile = tilemap.GetTile<Tile>(pos);
            if (tile != null) 
            {
                Color curCol = tilemap.GetColor(pos);
                bool isWhite =  Mathf.Approximately(1.0f, curCol.r) && Mathf.Approximately(1.0f, curCol.g) && Mathf.Approximately(1.0f, curCol.b);
                if (isWhite) {
                    total++;
                }
            }
        }

        print("Amount of tiles: " + total);
    }

    // Update is called once per frame
    void Update()
    {
        redText.text = (Mathf.Round(redCount / total * 100.0f)).ToString() + "%";
        greenText.text = (Mathf.Round(greenCount / total * 100.0f)).ToString() + "%";
        blueText.text = (Mathf.Round(blueCount / total * 100.0f)).ToString() + "%";
    }
}
