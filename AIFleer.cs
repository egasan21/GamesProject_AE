using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class AIFleer : MonoBehaviour
{
    public float fleeThreshold = 8.0f;
    public float stopThreshold = 0.0f;
    public float speed = 1.0f;
    public TMP_Text fLivesText;
    public AudioClip fDamage;
    public AudioClip fDeath;

    private GameObject player;
    private GameObject water;
    private Rigidbody2D rigidBody;
    private const float knockbackSTR = 2.0f;
    private AudioSource audioSource;
    private int fLives = 5;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        water = GameObject.FindGameObjectWithTag("Water");
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (fLives > 0) fLivesText.text = fLives.ToString() + " HP";
        if (fLives <= 0) fLivesText.text = ("DEAD");

        if (distanceToPlayer <= fleeThreshold)
        {
            Debug.Log("Fleeing");
            Vector3 movementDirection = transform.position - player.transform.position;
            // [1, 1, 0] -> sqrt(2) ---> [0.5, 0.5, 0] -> 1
            movementDirection = movementDirection.normalized;
            rigidBody.velocity = movementDirection * speed;
        }
        else if (distanceToPlayer >= stopThreshold)
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Hitbox"))
        {
            Debug.Log("Hit!");
            // monster lives

            fLives--;
            audioSource.PlayOneShot(fDamage);

            if (fLives == 0)
            {
                audioSource.PlayOneShot(fDeath);
                yield return new WaitForSeconds(0.5f);
                Destroy(gameObject);
                GameData.eyeCount--;
            }

            // knockback effect
            GetComponent<AIFleer>().enabled = false;
            GetComponent<Rigidbody2D>().AddForce((collision.transform.right * knockbackSTR), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);
            GetComponent<AIFleer>().enabled = true;
        }
        if (collision.CompareTag("Water"))
        {
            Debug.Log("Backing");   

            Vector3 movementDirection = transform.position - water.transform.position;
            // [1, 1, 0] -> sqrt(2) ---> [0.5, 0.5, 0] -> 1
            movementDirection = movementDirection.normalized;
            rigidBody.velocity = movementDirection * speed;
            yield return new WaitForSeconds(0.5f);


        }
    }
}
