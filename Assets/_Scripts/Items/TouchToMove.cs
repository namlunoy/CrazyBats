using UnityEngine;
using System.Collections;
/// <summary>
/// Script này dùng để di chuyển gameobject thông qua touch
/// hoặc click chuột để di chuyển
/// </summary>
public class TouchToMove : MonoBehaviour
{

    private bool isMouseDown = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Giúp gameobject
        if (Input.touchCount > 0 && isMouseDown)
        {
            Vector2 point = Input.GetTouch(0).position;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, 0));
            pos.z = 0;
            transform.position = pos;
        }

        if (isMouseDown)
        {
            Vector3 t = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            t.z = 0;
            transform.position = t;
        }
    }

    void OnMouseDown()
    {
        isMouseDown = true;
    }

    void OnMouseUp()
    {
        isMouseDown = false;
    }
}
