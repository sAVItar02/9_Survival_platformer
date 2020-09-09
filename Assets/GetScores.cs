using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScores : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] TextMeshProUGUI highScoreText;
    void Start()
    {
        float score = PlayerPrefs.GetFloat("Score", 0f);
        scoreText.SetText(score.ToString());
        float highScore = PlayerPrefs.GetFloat("High Score", 0f);
        highScoreText.SetText(highScore.ToString());
    }
}
