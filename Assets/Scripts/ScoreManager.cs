using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText = null;
    float startingScore = 0;
    public float currentScore = 0f;
    Box box;
    PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        scoreText.SetText(startingScore.ToString());
    }

    void Update()
    {
        if(!player.isGameOver)
        {
            box = FindObjectOfType<Box>();
            float score = PlayerPrefs.GetFloat("Score", 0f);
            scoreText.SetText(((int)score).ToString());
        }
        else
        {
            PlayerPrefs.SetFloat("Score", currentScore);
            if(currentScore > PlayerPrefs.GetFloat("High Score", 0f))
            {
                PlayerPrefs.SetFloat("High Score", currentScore);
            }
        }
    }
}
