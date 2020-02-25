using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score;

    void Start()
    {
        score = 0;
    }

    // add/remove points to score
    public static void updateScore(int points)
    {
        score = score + points;
        PlayingPanel.Instance.ShowScore(score);

        Debug.Log("Score: " + score);
    }

    // set score back to 0
    public static void resetScore()
    {
        score = 0;
        PlayingPanel.Instance.ShowScore(score);
    }
}
