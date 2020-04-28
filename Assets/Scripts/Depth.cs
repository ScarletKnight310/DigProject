using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Depth : MonoBehaviour
{
    public static int depth;

    void Start()
    {
        depth = 0;
    }

    public static void updateDepth(int newDepth)
    {
        if (newDepth > depth)
        {
            depth = newDepth;
            PlayingPanel.Instance.ShowDepth(depth);
        }
        //Debug.Log("Depth: " +depth);
    }
}
