using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyFollower : MonoBehaviour
{

    public bool isActive=false;

    public GameObject previous;
    private FollowEnemy enemyhead=null;
    public float minDistance = 2f;
    private SphereCollider c;
    public float force;
    private Rigidbody rb;

    public void setIsActive(bool isActive) {
        this.isActive = isActive;
    }

    public void Start() {
        c = gameObject.GetComponent<SphereCollider>();
        c.isTrigger = true;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void setPositionTrain(GameObject head, GameObject previous) {
        enemyhead = head.GetComponent<FollowEnemy>();
        this.previous = previous;
    }

    public void Update() {
        if (isActive) {
            if (previous != null&&Vector3.Distance(transform.position, previous.transform.position)>minDistance) {
                Vector3 dir = previous.transform.position - transform.position;
                dir.Normalize();
                if(rb.velocity.x*dir.x<0 || rb.velocity.y * dir.y < 0) {
                    rb.velocity = Vector3.zero;
                }
                rb.AddForce(dir * force*Time.deltaTime);
            } else {
                rb.velocity = Vector3.zero;
            }
        }
    }

    //public void OnCollisionEnter(Collision collision) {
    //    if (collision.gameObject.tag.Equals("Player") && isActive) {
    //        print(collision.gameObject.name);
    //        Movement.instance.gameObject.transform.position = new Vector3(0, 0, 0);
    //        Movement.instance.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //        enemyhead.die();
    //    }
    //}

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("Player")) {
            c.isTrigger = false;
        }
    }
}
