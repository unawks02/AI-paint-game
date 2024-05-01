using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class botSceneC : MonoBehaviour
{
    public GameObject hasPlayer;
    public TextMeshProUGUI mainText;

    public void ChangeScene ()
    {
        mainText.text = "Loading, please wait...";

        hasPlayer.GetComponent<isPlayer>().hasPlayer = false;
        SceneManager.LoadScene("mvp");
    }
}
