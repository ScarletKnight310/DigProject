using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingPanel : MonoBehaviour
{
    public static PlayingPanel Instance = null;
    
    public Text DepthDisplay;
    public Text ScoreDisplay;


    void Awake()
    {
        if (Instance == null) //singleton
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDepth(int depth)
    {
        if(DepthDisplay != null)
        {
            DepthDisplay.text = "Max Depth: " + depth + " Leagues";
            Debug.Log("updated text");
        }
    }
    
    public void ShowScore(int score)
    {
        if(ScoreDisplay != null)
        {
            ScoreDisplay.text = "Score: " + score;
            Debug.Log("updated score");
        }
}
