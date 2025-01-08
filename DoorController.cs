using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public string nextSceneName;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
            Debug.Log("Level2");
            }
    } 
}

