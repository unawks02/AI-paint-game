using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class freezeBotsPlayer : MonoBehaviour
{
    public GameObject playerChar;
    public GameObject thisBot;
    public float timeGiven = 120.0f;
    private float timeLeft = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (playerChar.name == "agent") {
            timeGiven = playerChar.GetComponent<timerBot>().timeGiven;
            Debug.Log("agent time " + timeGiven);
        }
        else
        {
            timeGiven = playerChar.GetComponent<timer>().timeGiven;
        }
        timeLeft = timeGiven + 5.0f;
        thisBot.GetComponent<mlagentTraining>().enabled = false; 
        thisBot.GetComponent<paint>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;


        if (timeLeft <= 0.0f)
        {
            timerEnded();
        }
        else if (timeLeft <= (timeGiven + 0.0f))
        {
            thisBot.GetComponent<mlagentTraining>().enabled = true;
            thisBot.GetComponent<paint>().enabled = true;
            //Debug.Log("paint enabled");
        }

    }

    void timerEnded()
    {
        thisBot.GetComponent<mlagentTraining>().enabled = false;
        thisBot.GetComponent<paint>().enabled = false;
    }

    //end old code

}
