using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviour
{
    public Transform player1Loc;
    public Transform player2Loc;
    public GameObject cubeAnim;
    public Grid grid = new Grid(9, 16, 1);

    // Start is called before the first frame update
    void Start()
    {
        List<int[]> obstacles = new List<int[]>();
        obstacles.Add(new int[2] { 4, 1 });        
        obstacles.Add(new int[2] { 4, 2 });
        obstacles.Add(new int[2] { 4, 3 });
        obstacles.Add(new int[2] { 1, 5 });
        grid.addObstacles(obstacles);

        List<int[]> breakableObstacles = new List<int[]>();
        breakableObstacles.Add(new int[2] { 6, 6 });
        grid.addBreakableObstacles(breakableObstacles);

        List<int[]> traps = new List<int[]>();
        traps.Add(new int[2] { 7, 3 });
        grid.addTraps(traps);

        StartCoroutine(setupBlocks());
    }

    IEnumerator setupBlocks()
    {
        for (int y = 0; y < grid.getHeight(); y++)
        {
            for (int x = 0; x < grid.getWidth(); x++)
            {
                if (grid.getCellID(x, y) == 1)
                {
                    GameObject clone;
                    clone = Instantiate(cubeAnim, new Vector3(x + grid.getCellSize() / 2, grid.getCellSize() / 2, y + grid.getCellSize() / 2), Quaternion.identity) as GameObject;
                    
                    clone.transform.parent = transform;
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
    }
    
    public void LoadScene()
    {
        Time.timeScale = 1;
        StartCoroutine(setupBlocksEnd());
    }
    IEnumerator setupBlocksEnd()
    {
        for (int y = 0; y < grid.getHeight(); y++)
        {
            for (int x = 0; x < grid.getWidth(); x++)
            {
                if (grid.getCellID(x, y) == 0)
                {
                    GameObject clone = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    clone.transform.position = new Vector3(x + grid.getCellSize() / 2, grid.getCellSize() / 2, y + grid.getCellSize() / 2);
                    clone.transform.parent = transform;
                    yield return new WaitForSeconds(0.0125f);
                }
            }
        }
        SceneManager.LoadScene("Gameplay");

    }

}
