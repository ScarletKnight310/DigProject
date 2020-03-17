using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [Header("Mine Presets")]
    public bool autoMine = false;
    public Vector3 origin;
    public int miningPointValue = 1;
    public ParticleSystem blockBreak;

    void Start()
    {
        origin = transform.position;
    }

   // void OnCollisionStay(Collision collision)
  //  {
  //      if ((collision.gameObject.tag == "Block") && (Input.GetButton("Mine") || autoMine))
   //         mineIt(collision.gameObject);
   //     if ((collision.gameObject.tag == "Death") && (Input.GetButton("Mine") || autoMine))
   //         blowUp(collision.gameObject);
   // }

    void OnTriggerEnter(Collider collision) {
        if ((collision.gameObject.tag == "Block") && (Input.GetButton("Mine") || autoMine))
            mineIt(collision.gameObject);
        if ((collision.gameObject.tag == "Death") && (Input.GetButton("Mine") || autoMine))
            blowUp(collision.gameObject);
    }

    public void mineIt(GameObject block)
    {
        Depth.updateDepth((int)block.transform.position.y * -1);
        Score.updateScore(miningPointValue);
        //Destroy(block);
        block.SetActive(false);
        if (blockBreak != null) {
            blockBreak.transform.position = block.transform.position;
            blockBreak.Play();
        }
    }

    public void blowUp(GameObject bomb)
    {
        Depth.updateDepth(0);
        //Destroy(bomb);
        bomb.SetActive(false);
        transform.position = origin;
        SendMessage("Reset");
    }
}
