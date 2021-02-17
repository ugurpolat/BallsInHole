using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public int score = 0;
    public GameObject nextLevel;
    public Swipe swipeControl;
    public string holeName = "";
    private Grid grid;
    private int step;
    private int obstacleType;
    public bool isMoving = false;
    private Vector3 desiredPosition1;
    string moving_direction = "";
    private bool DEBUG_MODE = true;
    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("GameSetup").GetComponent<GameSetup>().grid;
        grid.updateMovableLocation(holeName, new Vector2Int((int)transform.position.x, (int)transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (score == 2)
        {
            nextLevel.SetActive(true);
            
        }
        if (isMoving == false)
        {
            desiredPosition1 = transform.position;
            if (swipeControl.SwipeUp)
            {


                if (DEBUG_MODE) Debug.Log("Hit Up");
                (step, obstacleType) = grid.CalculateTargetPosition("up", transform.position);
                if (step != 0)
                {
                    isMoving = true;
                    moving_direction = "up";
                    desiredPosition1 += Vector3.forward * grid.getCellSize() * step;
                }
                

            }
            if (swipeControl.SwipeDown)
            {


                if (DEBUG_MODE) Debug.Log("Hit Down");
                (step, obstacleType) = grid.CalculateTargetPosition("down", transform.position);
                if (step != 0)
                {
                    isMoving = true;
                    moving_direction = "down";
                    desiredPosition1 += Vector3.back * grid.getCellSize() * step;
                }
               

            }
            if (swipeControl.SwipeLeft)
            {

                if (DEBUG_MODE) Debug.Log("Hit Left");
                (step, obstacleType) = grid.CalculateTargetPosition("left", transform.position);
                if (step != 0)
                {
                    isMoving = true;
                    moving_direction = "left";
                    desiredPosition1 += Vector3.left * grid.getCellSize() * step;
                }
                

            }
            if (swipeControl.SwipeRight)
            {

                if (DEBUG_MODE) Debug.Log("Hit Right");
                (step, obstacleType) = grid.CalculateTargetPosition("right", transform.position);
                if (step != 0)
                {
                    isMoving = true;
                    moving_direction = "right";
                    desiredPosition1 += Vector3.right * grid.getCellSize() * step;
                }
                

            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition1, 10f * Time.deltaTime);
            if (Vector3.Distance(transform.position, desiredPosition1) < 0.01f)
            {
                isMoving = false;
                transform.position = desiredPosition1;
                grid.updateMovableLocation(holeName, new Vector2Int((int)transform.position.x, (int)transform.position.z));
            }
        }
    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Ball1"))
        {
            score++;
            grid.ball1_active = false;
            grid.setEmptyLocation(grid.ball1);

            Destroy(target.gameObject);
        }
        if (target.gameObject.CompareTag("Ball2"))
        {
            score++;
            grid.ball2_active = false;
            grid.setEmptyLocation(grid.ball2);

            Destroy(target.gameObject);
        }
        if (target.gameObject.CompareTag("Gold"))
        {

            Destroy(target.gameObject);
            SFXCtrl.instance.ShowCoinSparkle(target.transform.position);
        }

    }
}
