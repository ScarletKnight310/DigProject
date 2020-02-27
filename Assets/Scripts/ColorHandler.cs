using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    public List<Color> depthColors = new List<Color>();

    Color newColor;

    MeshRenderer mr;

    float currentDepth;

    float maxDepth = 0.0f;

    float increment;

    float colorNumber = 1;

    int cf;

    int cc;
    
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>(); //get renderer

        increment = depthColors.Count; //set increment to num of colors

        maxDepth = gameObject.GetComponent<GroundManager>().MaxDepth;
    }

    // Update is called once per frame
    void Update()
    {
        print(colorNumber);

        if ( colorNumber > 0.0f && colorNumber < increment )
        {
            currentDepth = -transform.position.y; //get current depth

            colorNumber = increment * (currentDepth / maxDepth); //constrain depth to # of colors

            cf = Mathf.FloorToInt(colorNumber); //color before where we are in the list

            cc = Mathf.CeilToInt(colorNumber); //color after where we are in the list

            newColor = Color.Lerp(depthColors[cf], depthColors[cc], (colorNumber % 1)); //calculate current color

            mr.material.color = newColor; //display color on tile
        } 
    }
}
