using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public static int score, level;
    public Text scoreText, levelText;

    private void Awake()
    {
        score = 0;
        level = 1;
    }
    void Update()
    {
        level = score / 10 + 1;
        scoreText.text = "Score: " + score;
        levelText.text = "Level: " + level;
    }
}
