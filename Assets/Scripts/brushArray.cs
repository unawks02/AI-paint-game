using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brushArray : MonoBehaviour
{

    private int brushDimension = 3; //should only be set to odd numbers
    public int[,] array;
    public int arraysize = 9;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        array = new int[brushDimension, brushDimension];
    }

}
