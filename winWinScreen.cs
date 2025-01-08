using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winWinScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space)) UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
