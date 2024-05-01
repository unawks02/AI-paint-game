using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPlayer : MonoBehaviour
{

    public bool hasPlayer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        hasPlayer = true;
    }

}
