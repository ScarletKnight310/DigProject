using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;
public class LevelTrans : MonoBehaviour
{
    private float uiMoveTime;
    private int curLvl;
    private bool transitioning = false;
    [Header("Transtion Time (Seconds)")]
    public float transTime = 3f;
    [Header("Player Ref")]
    public GameObject playerRef;
    [Header("UI")]
    public GameObject playingPanel;
    public GameObject transUi;
    public Text lvlNum;

    void Start()
    {
        uiMoveTime = Time.time;
        curLvl = GameManager.level;
    }

    // Update is called once per frame
    void Update()
    {
        // starts transtion
        if(curLvl != GameManager.level) {
            curLvl = GameManager.level;
            playingPanel.SetActive(false);
            lvlNum.text = curLvl + "";
            transUi.SetActive(true);
            playerRef.SetActive(false);

            transitioning = true;
            uiMoveTime = Time.time + transTime;
        }
        // ends it
        if(Time.time >= uiMoveTime && transitioning) {
            playingPanel.SetActive(true);
            transUi.SetActive(false);
            playerRef.SetActive(true);
            playerRef.GetComponent<Movement>().zero();
            transitioning = false;
        }
    }
}
