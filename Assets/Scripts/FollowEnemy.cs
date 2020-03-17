using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public bool uncovered = false;
    public GameObject player=null;
    public Rigidbody rb;
    public float force; 

    public void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Update() {
        if (uncovered&&!enabled) {
            Vector3 dir = player.transform.position - transform.position;
            dir = dir.normalized;
            rb.AddForce(dir * force);
        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Equals("Player")) {
            player.transform.position=new Vector3(0, 0, 0);
            enabled = false;
        }
    }

}
