using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    public List<Color> depthColors = new List<Color>();

    Color newColor;

    MeshRenderer mr;

    float currentDepth;

    float maxDepthInLevel;

    float increment;

    float colorNumber = 0.0f;

    int cf;

    int cc;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        maxDepthInLevel = GroundManager.MaxDepth;

        increment = depthColors.Count; //set increment to num of colors

        mr = GetComponent<MeshRenderer>(); //get renderer

        //print("color: " + colorNumber);
        //print("increment: " + increment);

        currentDepth = -transform.position.y; //get current depth and flip sign so we can do positive math

        colorNumber = (increment - 1) * (currentDepth / maxDepthInLevel); //constrain depth to # of colors

        cf = Mathf.FloorToInt(colorNumber); //color before where we are in the list

        cc = Mathf.CeilToInt(colorNumber); //color after where we are in the list

        if ((cf > -1 && cf < increment) && (cc > -1 && cc < increment))
        {

            newColor = Color.Lerp(depthColors[cf], depthColors[cc], (colorNumber % 1)); //calculate color between the 2 in list

            mr.material.color = newColor; //display color on tile
        }
        else
        {
            //print("cf: " + cf);

            //print("cc: " + cc);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
