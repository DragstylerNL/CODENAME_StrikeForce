using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private Transform ts;
    private PlayerShoot PlayerShoot;

    // Start is called before the first frame update
    void Start()
    {
        // get the players transform
        ts = transform;
        // get the players shooting script
        PlayerShoot = GetComponent<PlayerShoot>();
    }

    // Update is called once per frame
    void Update()
    {   
        // get input 
        bool _shoot = Input.GetButtonDown("MS_Right");

        // do something with the input
        if (_shoot)
        {
            PlayerShoot.RequestShot();
        }
    }
}
