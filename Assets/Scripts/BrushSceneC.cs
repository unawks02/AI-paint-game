using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BrushSceneC : MonoBehaviour
{
    public GameObject masterBrush;
    public GameObject hasPlayer;
    public TextMeshProUGUI mainText;

    public void ChangeScene (GameObject brushGenObj)
    {
        mainText.text = "Loading, please wait...";

        hasPlayer.GetComponent<isPlayer>().hasPlayer = true;

        masterBrush.GetComponent<brushArray>().array = brushGenObj.GetComponent<brushGen>().brush;
        masterBrush.GetComponent<brushArray>().arraysize = brushGenObj.GetComponent<brushGen>().maxSpaces;
        SceneManager.LoadScene("mvp");
    }
}
