using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject groundM;
    public GameObject player;
    private Movement playMV;
    [Space]
    [Header("Level Changes")]
    //private Movement move = player.GetComponent<Movement>();
    public int depthInc = 50;
    public float speedInc = .25f;
    GroundManager manager;
    int level = 1;


    // Start is called before the first frame update
    void Awake() {
        manager = groundM.GetComponent<GroundManager>();
        PlayingPanel.Instance.ShowLevel(level);
        if (player != null) { 
        playMV = player.GetComponent<Movement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y <= (-1*GroundManager.MaxDepth)) {
            player.transform.position = new Vector3(0, 0, 0);
            player.GetComponent<Movement>().zero();
            if (level <= 5)
            {
                GroundManager.MaxDepth = GroundManager.MaxDepth * 2;

            }
            else
            {
                GroundManager.MaxDepth = GroundManager.MaxDepth + depthInc;

            }
            manager.removeLevel();
            manager.clearBounds();
            manager.PlaceBlocks();
            manager.CreateBounds();
            playMV.speed += speedInc;
            level++;
            PlayingPanel.Instance.ShowLevel(level);
        }
    }
}

