using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;
    private Vector3 startTouch, swipeDelta;
    
    

    
    public Vector3 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
   
    private void Update()
    {
        
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }

        //calculate distance
        swipeDelta = Vector3.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = (Vector3)Input.touches[0].position - startTouch;

            
        }

        if (swipeDelta.magnitude > 0.5f)
        {
            //which direction
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //left or right
                if (x < 0) 
                {
                    swipeLeft = true;
                                     
                }
                else
                {
                    swipeRight = true;
                    
                }
            }
            else
            {
                if (y < 0)
                {
                    swipeDown = true;
                    
                }

                else
                {
                    swipeUp = true;
                    
                }
                    
            }

            Reset();
        }

    }

    private void Reset()
    {
        //startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
        
    }

}
