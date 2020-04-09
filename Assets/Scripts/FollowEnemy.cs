using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour {
    public bool uncovered = false;
        
    public GameObject player;
    public Rigidbody rb;
    public float force = 1f;
    public GameObject block = null;
    public float delayTime = 3f;
    public float delayTimer;
    public SphereCollider c;
    public float brakeFactor=0.9f;
    public float intelligenceFactor=1f;

    public float randBrakeFactor;
    public float randForceFactor;
    public float randIntelligence;

    private float actualBrakeFactor;
    private float actualForce;
    private float actualIntelligence;


    public int numBody = 1;
    public GameObject[] followBody = new GameObject[1];
    public GameObject[] body;

    public void setUncovered(bool uncovered) {
        this.uncovered = uncovered;
        setBodyActive(uncovered);

    }

    public void setBodyActive(bool active) {
        foreach(GameObject g in body) {
            g.GetComponent<EnemyBodyFollower>().isActive = active;
        }
    }

    public void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        player = Movement.instance.gameObject;
        delayTimer = delayTime;
        c = gameObject.GetComponent<SphereCollider>();
        c.isTrigger = true;
        //print(c.isTrigger);

        body = new GameObject[numBody];

        actualForce = force + (force * (randForceFactor * Random.value));
        actualBrakeFactor=brakeFactor+(brakeFactor*(randBrakeFactor*Random.value));
        if (actualBrakeFactor > 1) {
            actualBrakeFactor = 1;
        }
        actualIntelligence = intelligenceFactor - (intelligenceFactor * (intelligenceFactor * Random.value));

        GameObject prev=gameObject;
        for (int i = 0; i < body.Length; i++) {
            int BodyIndex = Random.Range(0, followBody.Length - 1);
            body[i] = Instantiate(followBody[BodyIndex], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            body[i].GetComponent<EnemyBodyFollower>().setPositionTrain(gameObject, prev);
            prev = body[i];
        }
    }

    public void spawn(Vector3 offset) {
        Vector3 newPostion = transform.position + offset;
        foreach(GameObject b in body) {
            b.transform.position = newPostion;
            newPostion += offset;
            b.SetActive(true);
        }
    }

    public void die() {
        foreach(GameObject b in body) {
            b.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void setBlock(GameObject block) {
        this.block = block;
    }

    public void Update() {
        if (uncovered) {
            if (delayTimer <= 0) {
                if (Random.value < actualIntelligence) {
                    Vector3 dir = player.transform.position - transform.position;
                    dir.Normalize();
                    if (rb.velocity.x * dir.x < 0 || rb.velocity.y * dir.y < 0) {
                        rb.velocity *= actualBrakeFactor;
                    }
                    rb.AddForce(dir * actualForce*Time.deltaTime);
                    foreach(GameObject b in body) {
                        b.GetComponent<Rigidbody>().AddForce(dir * actualForce * Time.deltaTime * 0.2f);
                    }
                }
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
            die();

        }
    }

}
