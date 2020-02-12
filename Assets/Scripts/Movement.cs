using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.X;
    private Rigidbody player_body = null;

    public float speed = 3f;
    private int directx = 0;
    private int directy = 0;
    

    void Start()
    {
        if (player_body == null)
            player_body = gameObject.GetComponent<Rigidbody>();
        player_body.useGravity = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(right))
            directx = 1;                
        if (Input.GetKey(left))
            directx = -1;
        if (Input.GetKey(up))
            directy = 1;
        if (Input.GetKey(down))
            directy = -1;
        //transform.position = new Vector3(transform.position.x + (direct * speed * Time.deltaTime), transform.position.y, transform.position.z);
        //Debug.Log("Move "+ direct);
        //player.AddForce(transform.right*(direct * speed),ForceMode.Force);
        player_body.MovePosition(transform.position + new Vector3((directx * speed * Time.deltaTime), (directy * speed * Time.deltaTime), 0));

    }

    private void Reset()
    {
        directx = 0;
        directy = 0;
    }
}