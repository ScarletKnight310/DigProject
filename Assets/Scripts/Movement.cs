﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   
    

    private Rigidbody player_body = null;
    [Header("Move Control")]
    public float speed = 3f;
    public bool AutoMove = true;
    private float directx = 0;
    private float directy = 0;
    private float tHold = 0.2f;

    void Start()
    {
        if (player_body == null)
            player_body = gameObject.GetComponent<Rigidbody>();
        player_body.useGravity = false;
    }

    void Update()
    {
        float xB = Input.GetAxis("Horizontal");
        float yB = Input.GetAxis("Vertical");

        //float x = xB > tHold || xB < tHold ? Mathf.Sign(xB) : 0;
        //float y = yB > tHold || yB < tHold ? Mathf.Sign(yB) : 0;

        if (xB != 0)
            directx = xB;
        if (yB != 0)
            directy = yB;
        //Debug.Log(directx + ", " + directy);
    }

    private void FixedUpdate() {
        player_body.MovePosition(new Vector3((directx * speed * Time.deltaTime) + transform.position.x, 
            (directy * speed * Time.deltaTime) + transform.position.y, 
            0));
    }

    private void Reset()
    {
        directx = 0;
        directy = 0;
    }
}