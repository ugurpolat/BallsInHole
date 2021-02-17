using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class SwipeTest : MonoBehaviour
{
    public bool stopBall;
    //public Swipe swipeControls;
    public Transform player;
    private Vector3 desiredPosition;
    private Rigidbody rb;
    public bool isRolling;
    public float y = 14;
    public float x = 7;
    int distanceX = 0;
    int distanceY = 0;

    string moving_direction = "";

    private void Start()
    {
        rb = player.gameObject.GetComponent<Rigidbody>();
        desiredPosition = player.transform.position;
        stopBall = false;
    }
    void Update()
    {

        if (true)
        {
            moving_direction = "left";
            desiredPosition += Vector3.left;
            //rb.AddForce(-1 * Time.deltaTime * 100, 0, 0, ForceMode.Impulse);


        }
        if (true)
        {
            moving_direction = "right";
            desiredPosition += Vector3.right;

            //player.transform.position = Vector3.MoveTowards(player.transform.position, desiredPosition, 10 * Time.fixedDeltaTime);
            //rb.AddForce(1 * Time.deltaTime * 100, 0, 0, ForceMode.Impulse);
            //StartCoroutine(StopForce());s

        }
        if (true)
        {

            moving_direction = "up";
            desiredPosition += Vector3.forward;
            //rb.AddForce(0, 0, 1 * Time.deltaTime * 150, ForceMode.Impulse);
            //StartCoroutine(StopForce());
        }
        if (true)
        {

            moving_direction = "down";
            desiredPosition += Vector3.back;
            //rb.AddForce(0, 0, -1 * Time.deltaTime * 150, ForceMode.Impulse);
            //StartCoroutine(StopForce());
        }

        player.transform.position = Vector3.MoveTowards(player.transform.position, desiredPosition, 50f * Time.deltaTime);
        if (Vector3.Distance(player.transform.position, desiredPosition) < 0.001f)
        {
            player.transform.position = desiredPosition;
        }
        else
        {
            if (moving_direction == "left")
            {
                player.transform.RotateAround(player.transform.position, Vector3.forward, 1000 * Time.deltaTime);
            }
            else if (moving_direction == "right")
            {
                player.transform.RotateAround(player.transform.position, Vector3.back, 1000 * Time.deltaTime);
            }
            else if (moving_direction == "up")
            {
                player.transform.RotateAround(player.transform.position, Vector3.right, 1000 * Time.deltaTime);
            }
            else if (moving_direction == "down")
            {
                player.transform.RotateAround(player.transform.position, Vector3.left, 1000 * Time.deltaTime);
            }
        }


    }

    //IEnumerator StopForce()
    //{
    //    yield return new WaitForSeconds(1);
        
    //}
    
    //private void OnCollisionEnter(Collision target)
    //{
    //    if (target.collider.CompareTag("Wall"))
    //    {
    //        stopBall = true;
    //        isRolling = false;
    //        rb.drag = 0;
    //        print("bam");
    //        StartCoroutine(StopForce());
            
    //    }
    //}
}
