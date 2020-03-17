using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [Header("Border Art")]
    public Material BorderMat;
    [Space]
    [Header("Block Types")]
    public GameObject[] block_types = new GameObject[1];
    public GameObject[] bomb_types = new GameObject[1];
    private List<GameObject> block_ref = new List<GameObject>(); 
    [Space]
    [Header("Area Range")]
    public static int MaxDepth = 20;
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

    public void PlaceBlocks()
    {
        for (int y = 0; y >= -MaxDepth; y--)
        {
            for (int x = -Xrange; x <= Xrange; x++)
            {
                // MaxNumBombs >= 0 &&
                if (!(-BombBuffer < y) && Random.value <= BombRate)
                {
                    block_ref.Add(Instantiate(bomb_types[Random.Range(0, bomb_types.Length)], new Vector3(x, y, 0), Quaternion.identity));
                   // MaxNumBombs--;
                }
                else
                {
                    block_ref.Add(Instantiate(block_types[Random.Range(0, block_types.Length)], new Vector3(x, y, 0), Quaternion.identity));
                }
            }
        }
    }

    public void clearBounds() {
        for (int i = 0; i < bounds.Length; i++) {
            Destroy(bounds[i]);
        }
    }

    public void CreateBounds()
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
        // bottom
        bounds[2].transform.localScale = new Vector3((Xrange * 2) + 1, 1, 1);
        // top
        bounds[3].transform.localScale = new Vector3((Xrange * 2) + 1, 1, 1);
        if(BorderMat!= null) {
            for(int i = 0; i < bounds.Length; i++) {
                bounds[i].GetComponent<MeshRenderer>().material = BorderMat;
            }
        }
    }

    public void removeLevel()
    {
    //    print("got here");
        while(block_ref.Count > 0)
        {
            Destroy(block_ref[0]);
            block_ref.RemoveAt(0);
        }
    }
}
