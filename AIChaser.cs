using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class AIChaser : MonoBehaviour
{
    public float threshold = 8.0f;
    public float speed = 1.0f;
    public TMP_Text cLivesText;
    public AudioClip cDamage;
    public AudioClip cDeath;
    public GameObject Border;

    private GameObject player;
    private Rigidbody2D rigidBody;
    private AudioSource audioSource;

    State state = State.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.cLives > 0) cLivesText.text = GameData.cLives.ToString() + " HP";
        else if (GameData.cLives <= 0) cLivesText.text = ("DEAD");

        if (state == State.IDLE)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= threshold) state = State.FLEER;
        }
        else if (state == State.FLEER) 
        {
            Vector3 movementDirection = player.transform.position - transform.position;
            // [1, 1, 0] -> sqrt(2) ---> [0.5, 0.5, 0] -> 1
            movementDirection = movementDirection.normalized;
            rigidBody.velocity = movementDirection * speed;
        }
    }

        IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            Debug.Log("Hit!");

            // monster lives
            Debug.Log(GameData.cLives);
            GameData.cLives--;
            audioSource.PlayOneShot(cDamage);

            if (GameData.cLives == 0)
            {
                GameData.bossCount = 0;
                GameData.bossQuest = 1;
                audioSource.PlayOneShot(cDeath);
                yield return new WaitForSeconds(1.0f);
                Destroy(gameObject);
            }

                // knockback effect
            GetComponent<AIChaser>().enabled = false;
            GetComponent<Rigidbody2D>().AddForce((collision.transform.right * 10), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.1f);
            GetComponent<AIChaser>().enabled = true;
        }
    }
}
