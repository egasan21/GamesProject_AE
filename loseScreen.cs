using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
