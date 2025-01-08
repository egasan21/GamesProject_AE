using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeQuest : MonoBehaviour
{
    public GameObject EyeQStart;
    public GameObject EyeQEnd;
    public GameObject exclamation;
    public GameObject checkmark;

    private bool isKeyDown;

    
    
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            isKeyDown = true;
        }
        else { isKeyDown = false; }
    }
    IEnumerator OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && isKeyDown == true && GameData.eyeCount == 0)
        {
            Debug.Log("Quest Compelete");
            exclamation.SetActive(false);
            checkmark.SetActive(false);
            EyeQEnd.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            GameData.eyeQuest = 1;
            EyeQEnd.SetActive(false);
            checkmark.SetActive(true);
            Debug.Log(GameData.eyeQuest);
        }

        else if (collider.gameObject.CompareTag("Player") && isKeyDown == true && GameData.eyeCount > 0)
        {
            Debug.Log("Quest start");
            exclamation.SetActive(false);
            EyeQStart.SetActive(true);
            yield return new WaitForSeconds(10.0f);
            EyeQStart.SetActive(false);
            exclamation.SetActive(true);

        }

    }
}
