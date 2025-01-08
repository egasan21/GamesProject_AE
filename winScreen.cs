using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Slime Quest " + GameData.slimeQuest);
            Debug.Log("Eye Quest " + GameData.eyeQuest);
            Debug.Log("Boss Quest " + GameData.bossQuest);
            if (GameData.slimeQuest == 1 && GameData.eyeQuest == 1 && GameData.bossQuest == 1) UnityEngine.SceneManagement.SceneManager.LoadScene("WinScreen");
        }
    }
}
