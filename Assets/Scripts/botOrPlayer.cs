using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botOrPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject mainbot;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] hasp;
        hasp = GameObject.FindGameObjectsWithTag("hasPlayer");

        if (hasp.Length == 0)
        { //default to using player
            player.SetActive(true);
            mainbot.SetActive(false);
        }
        else
        {
            bool includeP = hasp[0].GetComponent<isPlayer>().hasPlayer;

            if (includeP)
            {
                player.SetActive(true);
                mainbot.SetActive(false);
            }
            else
            {
                player.SetActive(false);
                mainbot.SetActive(true);
            }
        }
    }

}
