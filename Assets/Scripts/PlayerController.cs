using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float velocity;
    [SerializeField] private float angularVelocity;
    [SerializeField] private EnemyController Generator;
    [SerializeField] private GameObject GameOver;

    [SerializeField] private Slider hungryBar;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hungrySound;
    [SerializeField] private AudioClip eatingSound;
    [SerializeField] private AudioClip crashSound;
    private float hungryInterval;
    
    
    private Animator animator;

    private int score;
    private int timer;
    private float lifeCounter;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        scoreText.text = "0";
        timerText.text = "0";
        GameOver.SetActive(false);
        hungryInterval = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (hungryBar.value > 0)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(new Vector3(0, angularVelocity, 0));
                animator.SetBool("isMoving", true);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(new Vector3(0, -angularVelocity, 0));
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(transform.forward.x, 0f, transform.forward.z) * velocity;
                animator.SetBool("isMoving", true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(-transform.forward.x, 0f, -transform.forward.z) * velocity;
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            GameOver.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (hungryBar.value < 40)
        {
            hungryInterval -= Time.deltaTime;
            if (hungryInterval < 0f)
            {
                audioSource.PlayOneShot(hungrySound);
                hungryInterval = 10f;
            }
        }

        lifeCounter += Time.deltaTime;
        if (lifeCounter > 1.0f)
        {
            hungryBar.value -= Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ? 3 : 1;
            timer++;
            timerText.text = timer.ToString();
            lifeCounter = 0;
        }
        animator.SetInteger("life", (int)hungryBar.value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Generator.decrementItems();
            hungryBar.value += 5;
            score += 5;
            scoreText.text = score.ToString();
            audioSource.PlayOneShot(eatingSound);
        }

        if (other.CompareTag("Enemy"))
        {
            hungryBar.value -= 20;
            audioSource.PlayOneShot(crashSound);
        }
    }
}
