using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // the percentage of where the MainCamera needs to be positionsed 
    [Range(0f,100f)]
    public int percentageInbetween = 90;

    private Transform Crosshair;
    private Transform playerTS;

    // Start is called before the first frame update
    void Start()
    {
        // 
        Crosshair = GameObject.FindGameObjectWithTag("Crosshair_Mouse").GetComponent<Transform>();
        playerTS = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // variable to edit
        Vector3 direction = new Vector3(0, 0, 0);
        // get the position of the crosshair in relation to the player
        direction = Crosshair.position - playerTS.position;
        // devide by 100 and times the percentage
        direction /= 100;
        direction *= percentageInbetween;
        // keep the z value of the Camera at 10
        direction.z = -10;
        // set the positon of the Camera on the pos that we wanted
        transform.position = direction + playerTS.position;
    }

    /// <summary>
    /// Where inbetween do you need the Camera to be 
    /// </summary>
    /// <param name="_newPercentage">0 = at the player, 100 = the croshair</param>
    public void ChangePercentage(int _newPercentage)
    {
        percentageInbetween = (int)Mathf.Lerp(0, 100, _newPercentage);
    }
}
