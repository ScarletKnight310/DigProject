using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {

    public static GroundManager instance = null;

    [Header("Border Art")]
    public Material BorderMat;
    [Space]
    [Header("Block Types")]
    public GameObject[] block_types = new GameObject[1];
    public GameObject[] bomb_types = new GameObject[1];

    public GameObject[] followEnemies = new GameObject[1];

    public List<GameObject> block_ref = new List<GameObject>();

    public Dictionary<GameObject, GameObject> blockToEnemy = new Dictionary<GameObject, GameObject>();

    [Space]
    [Header("Area Range")]
    public static int MaxDepth = 200;

    public int Xrange = 9;
    public int BombBuffer = 4;
    public float followEnemiesRate = .75f;
    // public int MaxNumBombs = 10;
    public float BombRate = 0.75f;

    private List<GameObject> followlist = new List<GameObject>();

    private GameObject[] bounds = new GameObject[4];

    public Vector3 offset;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        CreateBounds();
        PlaceBlocks();
    }

    public void placeEnemies(int x, int y, GameObject block) {
        for (int i = 0; i < followlist.Count; i++) {
            var currentEnemy = followlist[i].GetComponent<FollowEnemy>();
            if (!currentEnemy.gameObject.activeSelf) {
                currentEnemy.uncovered = false;
                currentEnemy.setBodyActive(false);
                currentEnemy.gameObject.SetActive(true);
                currentEnemy.setBlock(block);
                currentEnemy.transform.position = new Vector3(x, y, 0);
                currentEnemy.spawn(offset);
                blockToEnemy.Add(block, currentEnemy.gameObject);
                return;
            }
        }
        int enemyIndex = Random.Range(0, followEnemies.Length - 1);
        GameObject newEnemy = Instantiate(followEnemies[enemyIndex], new Vector3(x, y, 0), Quaternion.identity);
        FollowEnemy head = newEnemy.GetComponent<FollowEnemy>();
        head.setBlock(block);
        head.spawn(offset);
        followlist.Add(newEnemy);
        blockToEnemy.Add(block, newEnemy);
    }

    public void PlaceBlocks()
    {
        stoneMat.SetFloat("_maxDepth", MaxDepth);

        for (int y = 0; y >= -MaxDepth; y--)
        {
            for (int x = -Xrange; x <= Xrange; x++)
            {
                // MaxNumBombs >= 0 &&
                if (!(-BombBuffer < y) && Random.value <= BombRate) {
                    block_ref.Add(Instantiate(bomb_types[Random.Range(0, bomb_types.Length)], new Vector3(x, y, 0), Quaternion.identity));
                    // MaxNumBombs--;
                } else {
                    block_ref.Add(Instantiate(block_types[Random.Range(0, block_types.Length)], new Vector3(x, y, 0), Quaternion.identity));
                }

                if (Random.value <= followEnemiesRate) {
                    placeEnemies(x, y, block_ref[block_ref.Count - 1]);
                }
            }
        }
    }

    public void clearBounds() {
        for (int i = 0; i < bounds.Length; i++) {
            Destroy(bounds[i]);
        }
    }

    public void CreateBounds() {
        //------------------------------- create
        for (int i = 0; i < bounds.Length; i++) {
            bounds[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        //------------------------------- position each
        bounds[0].transform.position = new Vector3(Xrange + 1, -MaxDepth / 2, 0);
        bounds[1].transform.position = new Vector3(-Xrange - 1, -MaxDepth / 2, 0);
        //bottom
        bounds[2].transform.position = new Vector3(0, -MaxDepth - 1, 0);
        // top
        bounds[3].transform.position = new Vector3(0, 2, 0);
        //-------------------------------- scale each
        bounds[0].transform.localScale = new Vector3(1, MaxDepth + 5, 1);
        bounds[1].transform.localScale = new Vector3(1, MaxDepth + 5, 1);
        // bottom
        bounds[2].transform.localScale = new Vector3((Xrange * 2) + 1, 1, 1);
        // top
        bounds[3].transform.localScale = new Vector3((Xrange * 2) + 1, 1, 1);
        if (BorderMat != null) {
            for (int i = 0; i < bounds.Length; i++) {
                bounds[i].GetComponent<MeshRenderer>().material = BorderMat;
            }
        }
    }

    public void removeLevel() {
        //    print("got here");
        for(int i = 0; i < block_ref.Count; i++) {
            Destroy(block_ref[i]);   
        }
        block_ref.Clear();
        blockToEnemy.Clear();
    }
}
