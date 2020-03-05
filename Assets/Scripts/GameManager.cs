using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject gm;
    public GameObject player;
    //private Movement move = player.GetComponent<Movement>();
    public int depthInc = 50;

    GroundManager manager;
    int level = 1;


    // Start is called before the first frame update
    void Awake()
    {
        manager = gm.GetComponent<GroundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y <= (-1*GroundManager.MaxDepth)) {
            player.transform.position = new Vector3(0, 0, 0);
            if (level <= 5)
            {
                GroundManager.MaxDepth = GroundManager.MaxDepth * 2;

            }
            else
            {
                GroundManager.MaxDepth = GroundManager.MaxDepth + depthInc;

            }
            manager.removeLevel();
            manager.resetBottom();
            manager.PlaceBlocks();
      
            level++;
        }
    }
}
