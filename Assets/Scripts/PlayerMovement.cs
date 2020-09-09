using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;
    [SerializeField] AudioClip jumpSound;

    private float horizontalMove = 0f;
    private bool jumpFlag = false;
    private bool jump = false;


    [SerializeField] AudioClip deathFX;
    [SerializeField] float waitTime = 1f;
    [SerializeField] ParticleSystem playerDeathVFX;
    public bool isGameOver = false;

    SceneLoader sceneLoader;

    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        isGameOver = false;
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (jumpFlag)
        {
            animator.SetBool("IsJumping", true);
            jumpFlag = false;
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            jump = true;
            if (jump == true)
            {
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
            }
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {
        // move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);

        if (jump)
        {
            jumpFlag = true;
            jump = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Box"))
        {
            StartCoroutine(InitiateDeathSequence());
        }
    }
    private IEnumerator InitiateDeathSequence()
    {
        GetComponent<AudioSource>().PlayOneShot(deathFX);
        GetComponent<Animator>().SetBool("IsDead", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Instantiate(playerDeathVFX, transform.position, Quaternion.identity);
        sceneLoader.LoadGameOverScene();
        isGameOver = true;
    }
}
