using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public Player player;
    Vector2 startPos;
    [SerializeField] int threshold = 20;
    bool fingerDown = false;
    bool mouseDown = false;

    private void Update()
    {
        if (!fingerDown && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            //Debug.Log("Touch count: " + Input.touchCount);
            startPos = Input.touches[0].position;
            fingerDown = true;
        }

        if (fingerDown)
        {
            float y = Input.touches[0].position.y;
            float x = Input.touches[0].position.x;

            if(y >= startPos.y + threshold)
            {
                fingerDown = false;
                //Debug.Log(string.Format("Swipe Up: startPos:{0} curPos:{1}", startPos, Input.touches[0].position));
                player.Move(Vector3.up);
            }
            if (x <= startPos.x - threshold)
            {
                fingerDown = false;
                //Debug.Log(string.Format("Swipe Left: startPos:{0} curPos:{1}", startPos, Input.touches[0].position));
                player.Move(Vector3.left);
            }
            if (x >= startPos.x + threshold)
            {
                fingerDown = false;
                //Debug.Log(string.Format("Swipe Right: startPos:{0} curPos:{1}", startPos, Input.touches[0].position));
                player.Move(Vector3.right);
            }


            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
                fingerDown = false;
        }

        // TESTING
        if(!mouseDown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = Input.mousePosition;
                mouseDown = true;
            }
        }

        if (mouseDown)
        {
            float yDelta = Input.mousePosition.y - startPos.y;
            float yMag = Mathf.Abs(yDelta);
            float xDelta = Input.mousePosition.x - startPos.x;
            float xMag = Mathf.Abs(xDelta);
            //Debug.Log(string.Format("yDelta:{0} xDelta:{1}", yDelta, xDelta));
            if (yDelta >= threshold && yMag > xMag)
            {
                //Debug.Log("Mouse Up");
                player.Move(Vector3.up);
            }
            if (xDelta > threshold && xMag > yMag)
            {
                //Debug.Log("Mouse Right");
                player.Move(Vector3.right);
            }
            if (xDelta < -threshold && xMag > yMag)
            {
                //Debug.Log("Mouse Left");
                player.Move(Vector3.left);
            }

            if (Input.GetMouseButtonUp(0))
                mouseDown = false;
        }
    }
}
