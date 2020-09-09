using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    //[SerializeField] Animator animator;
    [SerializeField] ParticleSystem particles;
    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            InitiateBreakSequence();
        }
    }

    private void InitiateBreakSequence()
    {
        Vector3 particleInstancePostion = new Vector3(transform.position.x, transform.position.y, -0.1f);
        var particlesInstance = Instantiate(particles, particleInstancePostion, Quaternion.identity);
        Destroy(gameObject);
        Destroy(particlesInstance, 1f);
        scoreManager.currentScore += 1;
        PlayerPrefs.SetFloat("Score", scoreManager.currentScore);
    }

    public float ReturnCurrentScore()
    {
        return scoreManager.currentScore;
    }
}
