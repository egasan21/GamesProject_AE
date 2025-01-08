using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuest : MonoBehaviour
{
    public GameObject BossQStart;
    public GameObject BossQEnd;
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
        if (collider.gameObject.CompareTag("Player") && isKeyDown == true && GameData.bossCount == 0)
        {
            Debug.Log("Quest Compelete");
            exclamation.SetActive(false);
            checkmark.SetActive(false);
            BossQEnd.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            GameData.bossQuest = 1;
            BossQEnd.SetActive(false);
            checkmark.SetActive(true);
            Debug.Log(GameData.bossQuest);
        }

        else if (collider.gameObject.CompareTag("Player") && isKeyDown == true && GameData.bossCount > 0)
        {
            Debug.Log("Quest start");
            exclamation.SetActive(false);
            BossQStart.SetActive(true);
            yield return new WaitForSeconds(10.0f);
            BossQStart.SetActive(false);
            exclamation.SetActive(true);
        }

    }

}
