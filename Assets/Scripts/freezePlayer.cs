using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class freezePlayer : MonoBehaviour
{

    public GameObject timerObj;
    public GameObject playerChar;
    public float timeGiven = 0.0f;
    private float timeLeft = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeGiven = timerObj.GetComponent<timer>().timeGiven;
        timeLeft = timeGiven + 5.0f;
        playerChar.GetComponent<move>().enabled = false;
        playerChar.GetComponent<paint>().enabled = false;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;


        if (timeLeft <= 0.0f)
        {
            timerEnded();
        }
        else if (timeLeft <= (timeGiven + 0.0f))
        {
            playerChar.GetComponent<move>().enabled = true;
            playerChar.GetComponent<paint>().enabled = true;
            //Debug.Log("paint enabled");
        }

    }

    void timerEnded()
    {
        playerChar.GetComponent<move>().enabled = false;
        playerChar.GetComponent<paint>().enabled = false;
    }
}
