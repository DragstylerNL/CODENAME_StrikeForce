using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    private bool activated = false;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Activate (float _timer)
    {
        activated = true;
        timer = _timer;
    }
}
