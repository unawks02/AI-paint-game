using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class freezeBots : MonoBehaviour
{

    public GameObject playerChar;
    public GameObject thisBot;
    public float timeGiven = 120.0f;
    private float timeLeft = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeGiven = playerChar.GetComponent<timer>().timeGiven;
        timeLeft = timeGiven + 5.0f;
        thisBot.GetComponent<MoveToTargetAgent>().enabled = false;
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
        else if (timeLeft <= (timeGiven + 0.0f) )
        {
            thisBot.GetComponent<MoveToTargetAgent>().enabled = true;
            thisBot.GetComponent<paint>().enabled = true;
        }
        
    }

    void timerEnded()
    {
        thisBot.GetComponent<MoveToTargetAgent>().enabled = false;
        thisBot.GetComponent<paint>().enabled = false;
    }

}
