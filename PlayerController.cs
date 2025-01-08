using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    private const float SPEED = 4.0f;
    private const float knockbackSTR = 6.0f;
    private Rigidbody2D rigidBody;
    private AudioSource audioSource;
    private Animator animator;
    private bool isCoolDown = false;
    private const float coolDown = 0.6f;

    public TMP_Text livesText;
    public GameObject deadScreen; 
    public AudioClip huSwing;
    public AudioClip huDamage;
    public AudioClip huDeath;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.heroLives > 0) livesText.text = GameData.heroLives.ToString() + " HP";
        else if (GameData.heroLives <= 0) livesText.text = "DEAD";

        // Attacks
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (isCoolDown == false)
            {
                StartCoroutine(CoolDown());
                animator.SetBool("isAttacking", true);
                Debug.Log("Swing");
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isCoolDown == false)
            {
                StartCoroutine(CoolDown());
                animator.SetBool("isAttackingUp", true);
                Debug.Log("SwingUp");
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isCoolDown == false)
            {
                StartCoroutine(CoolDown());
                animator.SetBool("isAttackingDown", true);
                Debug.Log("SwingDown");
            }
        }

        // Movement
        else if (Input.GetKey(KeyCode.W))
        {
            rigidBody.velocity = new Vector2(0.0f, SPEED);
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isAttackingUp", false);
            animator.SetBool("isAttackingDown", false);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            GetComponentInChildren<Canvas>().transform.localRotation = Quaternion.Euler(0f, 0, 0f);
            rigidBody.velocity = new Vector2(SPEED, 0.0f);
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isAttackingUp", false);
            animator.SetBool("isAttackingDown", false);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localRotation = Quaternion.Euler(180.0f, 0, 180.0f);
            GetComponentInChildren<Canvas>().transform.localRotation = Quaternion.Euler(-180.0f, 0, -180.0f);
            rigidBody.velocity = new Vector2(-SPEED, 0.0f);
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isAttackingUp", false);
            animator.SetBool("isAttackingDown", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidBody.velocity = new Vector2(0.0f, -SPEED);
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isAttackingUp", false);
            animator.SetBool("isAttackingDown", false);
        }
        else
        {

            rigidBody.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isAttackingUp", false);
            animator.SetBool("isAttackingDown", false);
        }

        if (GameData.heroLives <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }

    IEnumerator CoolDown()
    {
        isCoolDown = true;
        yield return new WaitForSeconds(coolDown);
        isCoolDown = false;
    }


    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AI"))
        {
            Debug.Log("You got hit!");
            GameData.heroLives--;
            audioSource.PlayOneShot(huDamage);
            Debug.Log("Hero Lives " + GameData.heroLives);
            GetComponent<PlayerController>().enabled = false;

            Vector3 movementDirection = transform.position - collision.transform.position;

            // [1, 1, 0] -> sqrt(2) ---> [0.5, 0.5, 0] -> 1
            movementDirection = movementDirection.normalized;
            //rigidBody.velocity = movementDirection * knockbackSTR;

            GetComponent<Rigidbody2D>().AddForce((movementDirection * knockbackSTR), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.5f);
            GetComponent<PlayerController>().enabled = true;

            if (GameData.heroLives <= 0)
            {
                audioSource.PlayOneShot(huDeath);
                animator.SetBool("isDead", true);
                yield return new WaitForSeconds(1.0f);
                rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                deadScreen.SetActive(true);
            }


        }
    }
}
