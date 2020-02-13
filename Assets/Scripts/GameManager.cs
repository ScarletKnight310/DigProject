using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject gm;
    public GameObject player;
    GroundManager manager;

    // Start is called before the first frame update
    void Awake()
    {
        manager = gm.GetComponent<GroundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y <= (-1*manager.MaxDepth)) {
            player.transform.position = new Vector3(0, 0, 0);
            manager.MaxDepth = manager.MaxDepth*2;
            manager.resetBottom();
            manager.PlaceBlocks();
        }
    }
}
