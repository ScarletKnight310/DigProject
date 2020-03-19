using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody player_body = null;
    [Header("Move Control")]
    public float speed = 3f;
    public bool AutoMove = true;
    private float deadZone = 0.001f;
    float xB = 0.0f;
    float yB = 0.0f;

    float lastx = 0.0f;
    float lasty = 0.0f;

    void Start()
    {
        if (player_body == null)
            player_body = gameObject.GetComponent<Rigidbody>();
        player_body.useGravity = false;
    }

    void Update()
    {
        xB = Input.GetAxis("Horizontal");
        yB = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        float x = Mathf.Abs(xB) < deadZone ? 0f : Mathf.Sign(xB);
        float y = Mathf.Abs(yB) < deadZone ? 0f : Mathf.Sign(yB);
        bool isFullyMoving = Mathf.Abs(xB) < deadZone && Mathf.Abs(yB) < deadZone;

        if ((Mathf.Abs(xB) < deadZone || Mathf.Abs(yB) < deadZone) && !isFullyMoving) {
            lastx = x;
            lasty = y;
        }

        if (AutoMove && isFullyMoving) {
            x = lastx;
            y = lasty;
        }

        player_body.MovePosition(new Vector3((x * speed * Time.deltaTime) + transform.position.x, 
            (y * speed * Time.deltaTime) + transform.position.y, 
            0));
    }

    private void Reset()
    {
        xB = 0;
        yB = 0;
    }
}