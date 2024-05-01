//depracated code, only for old timer system
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class timerBot : MonoBehaviour
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
        thisBot.GetComponent<paint>().enabled = false;
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
        gameText.text = "End!";
        winCanv.SetActive(true);

        //calculate winner 
        float red = tilemap.GetComponent<sumTiles>().redCount;
        float blue = tilemap.GetComponent<sumTiles>().blueCount;
        float green = tilemap.GetComponent<sumTiles>().greenCount;

        float max = Mathf.Max(red, green, blue);

        //set colors 
        if (Mathf.Approximately(max, red))
        {
            background.color = Color.red;
            winText.text = "Red wins!";
        }
        else if (Mathf.Approximately(max, blue))
        {
            background.color = Color.blue;
            winText.text = "Blue wins!";
        }
        else if (Mathf.Approximately(max, green))
        {
            background.color = Color.green;
            winText.text = "Green wins!";
        }
        else
        {
            background.color = Color.white;
            winText.text = "Tie or error :)";
        }

        GameObject[] masterBrush;
        masterBrush = GameObject.FindGameObjectsWithTag("brush");
        if (masterBrush.Length != 0)
        {
            GameObject.Destroy(masterBrush[0]);
        }

        GameObject[] hasp;
        hasp = GameObject.FindGameObjectsWithTag("hasPlayer");

        if (hasp.Length != 0)
        { //default to using player
            GameObject.Destroy(hasp[0]);
        }

    }
}
*/