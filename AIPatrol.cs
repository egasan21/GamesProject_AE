using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public float lerpSpeed = 1.0f;
    public TMP_Text pLivesText;
    public AudioClip pDamage;
    public AudioClip pDeath;


    private Vector3 startPosition;
    private Vector3 endPosition;


    private float lerpDirection = 1.0f;
    private float lerpTime = 0.0f;
    private const float knockbackSTR = 1.0f;
    private AudioSource audioSource;
    private int pLives = 2;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.Find("Start").position;
        endPosition = transform.Find("End").position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pLives > 0) pLivesText.text = pLives.ToString() + " HP";
        else if (pLives <= 0) pLivesText.text = "DEAD";

        lerpTime += lerpSpeed * lerpDirection * Time.deltaTime;

        if (lerpTime >= 1 && lerpDirection > 0) lerpDirection = -1.0f;
        else if (lerpTime <= 0 && lerpDirection < 0) lerpDirection = 1.0f;


        transform.position = Vector3.Lerp(startPosition, endPosition, lerpTime);
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.CompareTag("Hitbox"))
        {
            Debug.Log("Hit!");
            // monster lives
            pLives--;
            audioSource.PlayOneShot(pDamage);

            if (pLives == 0)
            {
                audioSource.PlayOneShot(pDeath);
                yield return new WaitForSeconds(0.5f);
                Destroy(gameObject);
                GameData.slimeCount--;
            }

            // knockback effect
           // GetComponent<AIPatrol>().enabled = false;
           // GetComponent<Rigidbody2D>().AddForce((collision.transform.right * knockbackSTR), ForceMode2D.Impulse);
           // yield return new WaitForSeconds(0.3f);
           // GetComponent<AIPatrol>().enabled = true;
        }
    }
}
