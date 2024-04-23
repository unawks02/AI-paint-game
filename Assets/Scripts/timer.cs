using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{

    public GameObject playerChar;
    public TextMeshProUGUI gameText;
    public float timeGiven = 120.0f;
    private float timeLeft = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeGiven + 5.0f;
        playerChar.GetComponent<move>().enabled = false;
        playerChar.GetComponent<paint>().enabled = false;
        gameText.text = "Ready...";
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if ((timeLeft < timeGiven) && (timeLeft > 0) && (timeLeft % 1 <= 0.01f))
        {
            gameText.text = (Mathf.Round(timeLeft * 10.0f) * 0.1f).ToString();
        }

        if (timeLeft <= 0.0f)
        {
            timerEnded();
        } 
        else if (timeLeft <= (timeGiven + 0.0f) )
        {
            playerChar.GetComponent<move>().enabled = true;
            playerChar.GetComponent<paint>().enabled = true;
        }
        else if (timeLeft <= (timeGiven + 1.0f))
        {
            gameText.text = "Go!";
        }
        else if (timeLeft <= (timeGiven + 3.0f))
        {
            gameText.text = "Set...";
        }
    }

    void timerEnded()
    {
        playerChar.GetComponent<move>().enabled = false;
        playerChar.GetComponent<paint>().enabled = false;
        gameText.text = "End!";
    }

}
