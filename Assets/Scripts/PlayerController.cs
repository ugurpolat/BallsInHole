using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Swipe swipeControl;
    public Animator anim;

    public string ballName = "";
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
        anim = gameObject.GetComponent<Animator>();
        grid = GameObject.Find("GameSetup").GetComponent<GameSetup>().grid;
        grid.updateMovableLocation(ballName, new Vector2Int((int)transform.position.x, (int)transform.position.z));
    }

    private void processObstacle(int obstacleType)
    {
        if (obstacleType == 2)
        {
            // Breake Obstalce
            // TODO find specific breakable obstacle
            GameObject breakableObject = GameObject.Find("BreakableObject");

            grid.setEmptyLocation(new Vector2Int((int)breakableObject.transform.position.x, (int)breakableObject.transform.position.z));
            breakableObject.SetActive(false); ;
        }
        if (obstacleType == 3)
        {
            // Explode Ball
            if (ballName == "ball1")
            {
                
                grid.ball1_active = false;
                grid.setEmptyLocation(grid.ball1);
                gameObject.SetActive(false);
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
            }
            else if (ballName == "ball2")
            {
                
                grid.ball2_active = false;
                grid.setEmptyLocation(grid.ball2);
                gameObject.SetActive(false);
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
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
                else
                {
                    processObstacle(obstacleType);
                    
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
                else
                    processObstacle(obstacleType);

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
                else
                    processObstacle(obstacleType);

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
                else
                    processObstacle(obstacleType);

            }


        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition1, 10f * Time.deltaTime);
            if (Vector3.Distance(transform.position, desiredPosition1) < 0.01f)
            {
                StartCoroutine(SetAnim());
                isMoving = false;
                transform.position = desiredPosition1;
                grid.updateMovableLocation(ballName, new Vector2Int((int)transform.position.x, (int)transform.position.z));
                processObstacle(obstacleType);

            }
            else
            {
                if (moving_direction == "left")
                {
                    transform.RotateAround(transform.position, Vector3.forward, 1000 * Time.deltaTime);
                }
                else if (moving_direction == "right")
                {
                    transform.RotateAround(transform.position, Vector3.back, 1000 * Time.deltaTime);
                }
                else if (moving_direction == "up")
                {
                    transform.RotateAround(transform.position, Vector3.right, 1000 * Time.deltaTime);
                }
                else if (moving_direction == "down")
                {
                    transform.RotateAround(transform.position, Vector3.left, 1000 * Time.deltaTime);
                }
            }

        }
    }
    IEnumerator SetAnim()
    {
        anim.enabled = true;
        yield return new WaitForSeconds(1f);
        anim.enabled = false;

    }
    
}
