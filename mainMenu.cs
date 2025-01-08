using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    // Update is called once per frame
    private void Update()
    {
        GameData.heroLives=10;
        GameData.cLives = 22;
        GameData.cCount = 1;
        GameData.slimeCount = 8;
        GameData.slimeQuest = 0;
        GameData.bossQuest = 0;
        GameData.bossCount = 1;
        GameData.eyeQuest = 0;
        GameData.eyeCount = 2;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level3");
        }
    }
}
