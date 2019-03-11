using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Transform ts_Player;
    private Transform ts_mouse;

    public GameObject bulletTrail;

    // Start is called before the first frame update
    void Start()
    {
        // player transform
        ts_Player = transform;
        // mouse position so we know where to shoot towards
        ts_mouse = GameObject.FindGameObjectWithTag("Crosshair_Mouse").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// request a shot
    /// here comes all of the logic wheter or not we can shoot yes or no
    /// </summary>
    public void RequestShot()
    {
        Shoot(10f, false);
    }

    /// <summary>
    ///  shoot your weapon
    /// </summary>
    /// <param name="_range">the distance the bullet can travel</param>
    /// <param name="_wallHacks">if the player can shoot through objects in the environment yes or no</param>
    private void Shoot(float _range, bool _wallHacks)
    {
        // get the direction to shoot to
        Vector3 direction = ts_mouse.position - ts_Player.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(ts_Player.position, direction, 100f);
        for(int i = 0; i < hits.Length; i++)
        {
            // if nothing is hit
            if (hits.Length == 1)
            {
                DrawTheLine(direction * 10);
            }
            if (i > 0)
            {
                //get the tag
                string tag = hits[i].collider.transform.parent.tag;
                // chech if it hit the wall or the ground
                if (tag == "Env")
                {
                    // if we cant shoot through walls and such
                    if (!_wallHacks)
                    {
                        // draw the line
                        DrawTheLine(hits[i].point);
                        // if we cant, return so this process is stopped 
                        return;
                    }
                }
                // check if we have hit a enemy or not
                else if (tag == "Enemy")
                {
                    // BOOM execute enemy hit logic
                    print("ENEMY HAS BEEN HIT BOOOOOOM");
                    // draw the line
                    DrawTheLine(hits[i].point);
                    // end this process as we have hit something
                    return;
                }
                // if nothing has ended the process, draw a line
                if(i == hits.Length - 1)
                {
                    DrawTheLine(direction * 10);
                }
            }
        }
    }
    
    /// <summary>
    /// render and draw the line to indicate that there has been shot by someone
    /// </summary>
    /// <param name="hit">the position of where the line should end</param>
    private void DrawTheLine(Vector3 hit)
    {
        // get an instance of the object
        GameObject nextLine = bulletTrail;
        // get the linerenderer
        LineRenderer rend = nextLine.GetComponent<LineRenderer>();
        // set the starting and ending positions
        rend.SetPosition(0, ts_Player.position);
        rend.SetPosition(1, hit);
        // spawn the line into the world
        nextLine = Instantiate(nextLine);
        // activate the self destruct module
        nextLine.GetComponent<DestroyTimer>().Activate(0.1f);
    }
}
