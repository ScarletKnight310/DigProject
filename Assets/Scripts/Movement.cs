using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
public class Movement : MonoBehaviour
{
=======
public class Movement : MonoBehaviour {
>>>>>>> EnemyAi
    private Rigidbody player_body = null;
    [Header("Move Control")]
    public float speed = 3f;
    public bool AutoMove = true;
<<<<<<< HEAD
    private float deadZone = 0.000000001f;
    float xB = 0.0f;
    float yB = 0.0f;

    float lastx = 0.0f;
    float lasty = 0.0f;

    public static Movement instance = null;

    public void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
=======
    private float deadZone = 0.001f;
    float xB = 0.0f;
    float yB = 0.0f;
>>>>>>> EnemyAi

    public static Movement instance = null;

<<<<<<< HEAD
    void Update()
    {
        xB = Input.GetAxis("Horizontal");
        yB = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        float x = Mathf.Abs(xB) < deadZone ? 0f : Mathf.Sign(xB);
        float y = Mathf.Abs(yB) < deadZone ? 0f : Mathf.Sign(yB);
        bool isMoving = Mathf.Abs(xB) > deadZone || Mathf.Abs(yB) > deadZone;

        if (isMoving) {
            lastx = x;
            lasty = y;
        }

        if (AutoMove && !isMoving) {
            x = lastx;
            y = lasty;
        }

        player_body.MovePosition(new Vector3((x * speed * Time.deltaTime) + transform.position.x, 
            (y * speed * Time.deltaTime) + transform.position.y, 
=======
    public void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void zero() {
        Reset();
        player_body.velocity = Vector3.zero;
    }

    void Start() {
        if (player_body == null)
            player_body = gameObject.GetComponent<Rigidbody>();
        player_body.useGravity = false;
    }

    void Update() {
        xB = Input.GetAxis("Horizontal");
        yB = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        player_body.velocity = Vector3.zero;
        float x = Mathf.Abs(xB) < deadZone ? 0f : Mathf.Sign(xB);
        float y = Mathf.Abs(yB) < deadZone ? 0f : Mathf.Sign(yB);
        //Debug.Log(x + ", " + y);
        if (AutoMove) {
            if (Mathf.Abs(xB) < deadZone && Mathf.Abs(yB) < deadZone) {
                // y = -1f;
            }
        }
        player_body.MovePosition(new Vector3((x * speed * Time.deltaTime) + transform.position.x,
            (y * speed * Time.deltaTime) + transform.position.y,
>>>>>>> EnemyAi
            0));
        
    }

<<<<<<< HEAD
    private void Reset()
    {
        xB = 0;
        yB = 0;
    }

    public void zero() {
        Reset();
        player_body.velocity = Vector3.zero;
=======
    private void Reset() {
        xB = 0;
        yB = 0;
>>>>>>> EnemyAi
    }
}