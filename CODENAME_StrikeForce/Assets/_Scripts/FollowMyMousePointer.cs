using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMyMousePointer : MonoBehaviour
{

    public Camera PlayerCam;

    // Update is called once per frame
    void Update()
    {
        // get the position of the mouse
        Vector3 mousePos = Input.mousePosition;
        // solve math problem beforhand by setting the ofset of the camera
        mousePos.z = 10;
        // calculate and position the object to the position of the mouse in the actual "world"
        this.transform.position = PlayerCam.ScreenToWorldPoint(mousePos);
    }

}
