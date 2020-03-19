using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour {
    public bool uncovered {
        get {
            return !block.activeSelf;
        }
    }
    public GameObject player;
    public Rigidbody rb;
    public float force;
    public GameObject block = null;
    public float delayTime = 3f;
    public float delayTimer;
    public SphereCollider c;


    public void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        player = Movement.instance.gameObject;
        delayTimer = delayTime;
        c = gameObject.GetComponent<SphereCollider>();
        c.isTrigger = true;
        print(c.isTrigger);
    }

    public void setBlock(GameObject block) {
        this.block = block;
    }

    public void Update() {
        if (uncovered) {
            if (delayTimer <= 0) {
                Vector3 dir = player.transform.position - transform.position;
                dir = dir.normalized;
                rb.AddForce(dir * force);
            } else {
                delayTimer -= Time.deltaTime;
                if (delayTimer <= 0) {
                    delayTimer = 0;
                    c.isTrigger = false;
                }
            }

        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (delayTimer > 0) {
            return;
        }
        if (collision.gameObject.tag.Equals("Player")) {
            //print(collision.gameObject.name);
            player.transform.position = new Vector3(0, 0, 0);
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

}
