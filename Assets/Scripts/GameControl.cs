using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static int score, level;
    public Text scoreText, levelText;

    private void Awake()
    {
        score = PlayerPrefs.GetInt("PlayerScore");
        level = 1;
    }
    void Update()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        scoreText.text = "Score: " + score;
        levelText.text = "Level: " + level;
        PlayerPrefs.SetInt("PlayerScore", score);
    }
}
