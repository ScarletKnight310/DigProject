using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    public List<Color> depthColors = new List<Color>();

    Color newColor;

    MeshRenderer mr;

    float currentDepth;

    float maxDepth = 50.0f;

    float increment;

    float ColorNumber;

    int cf;

    int cc;
    
    // Start is called before the first frame update
    void Start()
    {

        mr = GetComponent<MeshRenderer>(); //get renderer

        increment = depthColors.Count; //set increment to num of colors
    }

    // Update is called once per frame
    void Update()
    {
        currentDepth = -transform.position.y; //get current depth

        ColorNumber = increment * (currentDepth / maxDepth); //constrain depth to # of colors

        cf = Mathf.FloorToInt(ColorNumber); //color before where we are in the list

        cc = Mathf.CeilToInt(ColorNumber); //color after where we are in the list

        newColor = Color.Lerp(depthColors[cf], depthColors[cc], (ColorNumber % 1)); //calculate current color

        mr.material.color = newColor; //display color on tile 
    }
}
