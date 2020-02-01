using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour
{
#if UNITY_STANDALONE

    bool isMouseLeftDown = false;
    bool isMouseRightDown = false;
    bool isMouseLeftUp = false;
    bool isMouseRightUp = false;

    bool isDragging = false;
    Vector3 mouseDrag = Vector3.zero;
    Ray ray;
    RaycastHit hit;

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))   
        {
            if (hit.transform.gameObject.name != gameObject.name) //Works only if the oject with the script is clicked
            {
                return;
            }

            #region MOUSE BUTTONS UP DOWN
            //Left Mouse Button Down
            if (Input.GetMouseButtonDown(0))
            {
                isMouseLeftDown = true;
            }
            else
            {
                isMouseLeftDown = false;
            }

            //Right Mouse Button Down
            if (Input.GetMouseButtonDown(1))
            {
                isMouseRightDown = true;
            }
            else
            {
                isMouseRightDown = false;
            }

            //Left Mouse Button Up
            if (Input.GetMouseButtonUp(0))
            {
                isMouseLeftUp = true;
            }
            else
            {
                isMouseLeftUp = false;
            }

            //Right Mouse Button up
            if (Input.GetMouseButtonUp(1))
            {
                isMouseRightUp = true;
            }
            else
            {
                isMouseRightUp = false;
            }
            #endregion
        }
#if DEBUG_MODE
        if (isMouseLeftDown)
            Debug.Log("Left Down" + isMouseLeftDown);
        if (isMouseRightDown)
            Debug.Log("Right Down" + isMouseRightDown);
        if (isMouseLeftUp)
            Debug.Log("Left Up" + isMouseLeftUp);
        if (isMouseRightUp)
            Debug.Log("Right Up" + isMouseRightUp);
        if (isDragging)
        {
            Debug.Log(MouseDragPosition().ToString());
        }
#endif
    }

    #region LEFT MOUSE
    public bool IsMouseLeftDown()
    {
        return isMouseLeftDown;
    }
    
    public bool IsMouseLeftUp()
    {
        return isMouseLeftUp;
    }
    #endregion

    #region RIGHT MOUSE
    public bool IsMouseRightDown()
    {
        return isMouseRightDown;
    }

    public bool IsMouseRightUp()
    {
        return isMouseRightUp;
    }
    #endregion

    #region DRAG MOUSE
    public void OnMouseDrag()
    {
        isDragging = true;
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        mouseDrag = new Vector3(pos_move.x, transform.position.y, pos_move.z);
    }
    private void OnMouseUp()
    {
        isDragging = false;
    }

    public Vector3 MouseDragPosition()
    {
        if (!isDragging)
        { return Vector3.zero; }
        else
        { return mouseDrag; }
    }
    #endregion

#endif
}
