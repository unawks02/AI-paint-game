using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class brushGen : MonoBehaviour
{
    public int maxSpaces = 9; //set for different sizes
    private int brushDimension = 3;
    public int[,] brush;
    public GameObject masterBrush;

    public Image im00;
    public Image im01;
    public Image im02;
    public Image im10;
    public Image im11;
    public Image im12;
    public Image im20;
    public Image im21;
    public Image im22;

    // Start is called before the first frame update

    //todo: attach this to brush ui
    //the grid box place
    //and make sure to have button update master brush array
    void Start()
    {
        //generate array

        brush = new int[brushDimension, brushDimension];

        int spacesUsed = 0;
        for (int i = 0; i < brushDimension; i++)
        {
            for (int j = 0; j < brushDimension; j++)
            {
                if (spacesUsed < maxSpaces)
                {
                    brush[i, j] = Random.Range(0, 2); //returns 0 or 1 at random to generate a random pattern for the brush
                    if (brush[i, j] == 1)
                    {
                        spacesUsed++;
                    }
                }
                else
                {
                    brush[i, j] = 0;
                }
            }
        }

        //this could probably be done in a loop somehow but im not smart
        if (brush[0, 0] == 1)
        {
            im00.color = Color.black;
        }
        if (brush[0, 1] == 1)
        {
            im01.color = Color.black;
        }
        if (brush[0, 2] == 1)
        {
            im02.color = Color.black;
        }

        if (brush[1, 0] == 1)
        {
            im10.color = Color.black;
        }
        if (brush[1, 1] == 1)
        {
            im11.color = Color.black;
        }
        if (brush[1, 2] == 1)
        {
            im12.color = Color.black;
        }

        if (brush[2, 0] == 1)
        {
            im20.color = Color.black;
        }
        if (brush[2, 1] == 1)
        {
            im21.color = Color.black;
        }
        if (brush[2, 2] == 1)
        {
            im22.color = Color.black;
        }
    }
}
