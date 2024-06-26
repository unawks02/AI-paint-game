using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class freezeBotsPlayer : MonoBehaviour
{
    public GameObject timerObj;
    public GameObject thisBot;
    public float timeGiven = 0.0f;
    private float timeLeft = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        timeGiven = timerObj.GetComponent<timer>().timeGiven;
        timeLeft = timeGiven + 5.0f;
        thisBot.GetComponent<mlagentTraining>().enabled = false; 
        thisBot.GetComponent<paintBot>().enabled = false;
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
            thisBot.GetComponent<paintBot>().enabled = true;
            //Debug.Log("paint enabled");
        }

    }

    void timerEnded()
    {
        thisBot.GetComponent<mlagentTraining>().enabled = false;
        thisBot.GetComponent<paintBot>().enabled = false;
    }
}
