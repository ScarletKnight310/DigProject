using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject[] blocks = new GameObject[1];
    public GameObject[] bombs = new GameObject[1];

    public int MaxDepth = 20;
    public int Xrange = 9;
    public int BombBuffer = 4;
   // public int MaxNumBombs = 10;
    public float BombRate = 0.75f;

    private GameObject[] bounds = new GameObject[4];

    void Awake()
    {   
        CreateBounds();
        PlaceBlocks();
    }

    private void PlaceBlocks()
    {
        for (int y = 0; y >= -MaxDepth; y--)
        {
            for (int x = -Xrange; x <= Xrange; x++)
            {
                // MaxNumBombs >= 0 &&
                if (!(-BombBuffer < y) && Random.value <= BombRate)
                {
                    Instantiate(bombs[Random.Range(0, bombs.Length)], new Vector3(x, y, 0), Quaternion.identity);
                   // MaxNumBombs--;
                }
                else
                {
                    Instantiate(blocks[Random.Range(0, blocks.Length)], new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }

    private void CreateBounds()
    {
        //------------------------------- create
        for(int i = 0; i < bounds.Length; i++)
        {
            bounds[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        //------------------------------- position each
        bounds[0].transform.position = new Vector3(Xrange + 1, -MaxDepth / 2, 0);
        bounds[1].transform.position = new Vector3(-Xrange - 1, -MaxDepth / 2, 0);
        //bottom
        bounds[2].transform.position = new Vector3(0,-MaxDepth-1, 0);
        // top
        bounds[3].transform.position = new Vector3(0,2, 0);
        //-------------------------------- scale each
        bounds[0].transform.localScale = new Vector3(1,MaxDepth + 5,1);
        bounds[1].transform.localScale = new Vector3(1,MaxDepth + 5,1);
        //bottom
        bounds[2].transform.localScale = new Vector3((Xrange * 2) + 1, 1, 1);
        // top
        bounds[3].transform.localScale = new Vector3((Xrange * 2) + 1, 1, 1);
    }
}
