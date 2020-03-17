using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public KeyCode mine = KeyCode.X;
    public bool autoMine = false;
    public Vector3 origin;


    void Start()
    {
        origin = transform.position;
    }

    void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.tag == "Block") && (Input.GetKey(mine) || autoMine))
            mineIt(collision.gameObject);
        if ((collision.gameObject.tag == "Death") && (Input.GetKey(mine) || autoMine))
            blowUp(collision.gameObject);
    }

    public void mineIt(GameObject block)
    {
        Depth.updateDepth((int)block.transform.position.y * -1);
        Destroy(block);
    }

    public void blowUp(GameObject bomb)
    {
        Depth.updateDepth(0);
        Destroy(bomb);
        transform.position = origin;
        SendMessage("Reset");
    }
}
