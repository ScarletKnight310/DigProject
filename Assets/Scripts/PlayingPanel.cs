using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPanel : MonoBehaviour
{
    public static PlayingPanel Instance = null;

    public Text DepthDisplay;
    public Text ScoreDisplay;

    public Text LevelDisplay;


    void Awake() {
        if (Instance == null) //singleton
        {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void ShowDepth(int depth) {
        if (DepthDisplay != null) {
            DepthDisplay.text = "Max Depth: " + depth + " Leagues";
           // Debug.Log("updated text");
        }
    }

    public void ShowScore(int score) {
        if (ScoreDisplay != null) {
            ScoreDisplay.text = "Score: " + score;
            //Debug.Log("updated score");
        }
    }


    public void showLevel(int level) {
        if (LevelDisplay != null) {
            LevelDisplay.text = "Level: " + level;
        }
    }
}
