using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBehaviour : MonoBehaviour
{
    public float speed;

    public GameManager _gm;

    public Camera cam;

    float xAxis;
    float yAxis;
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButton(0)) && !_gm.switchControl&& !_gm.switchControl1)
        {
            RotatePoint();
        }
    }

    public void RotatePoint()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.collider.tag == "Ball")
        {
            xAxis = Input.GetAxisRaw("Mouse X") * speed;
            yAxis = Input.GetAxisRaw("Mouse Y") * speed;
            

            transform.Rotate(-Vector3.down, xAxis);
            transform.Rotate(-Vector3.right, yAxis);
        }
    }

}
