using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public bool uncovered = false;
    public GameObject player=null;
    public Rigidbody rb;
    public float force;
    public GameObject block = null;

    public void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void setBlock(GameObject block) {
        this.block = block;
    }

    public void Update() {
        if (uncovered) {
            Vector3 dir = player.transform.position - transform.position;
            dir = dir.normalized;
            rb.AddForce(dir * force);
        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            player.transform.position=new Vector3(0, 0, 0);
            gameObject.SetActive(false);
        }
    }

}
