using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeQuest : MonoBehaviour
{
    public GameObject SlimeQStart;
    public GameObject SlimeQEnd;
    private bool isKeyDown;
    public GameObject exclamation;
    public GameObject checkmark;
    
    
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
        
        //Quest complete
        if (collider.gameObject.CompareTag("Player") && isKeyDown == true && GameData.slimeCount == 0)
        {
    
            Debug.Log("Quest Compelete");
            exclamation.SetActive(false);
            checkmark.SetActive(false);
            SlimeQEnd.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            SlimeQEnd.SetActive(false);
            GameData.slimeQuest = 1;
            checkmark.SetActive(true);
            Debug.Log(GameData.slimeQuest);
        }

        //Quest start
        else if (collider.gameObject.CompareTag("Player") && isKeyDown == true && GameData.slimeCount > 0)
        {
            
            Debug.Log("Quest start");
            exclamation.SetActive(false);
            SlimeQStart.SetActive(true);
            yield return new WaitForSeconds(10.0f);
            SlimeQStart.SetActive(false);
            exclamation.SetActive(true);


        }

    }
}
