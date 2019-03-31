using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    //private Variable
    private bool grounded = false;
    public Vector3 force = new Vector3(0,0,0);

    //public variables 
    public bool useGravity = true;
    public float gravity = 5;
    public float mass = 1;
    public float sideDrag = 5;
    public Vector2 maxSpeed = new Vector2(2,1);

    //collider variables
    private Collider2D coll;
    private Vector3 offset;
    private float hight;
    private float width;

    // Start is called before the first frame update
    void Start()
    {
        // collider values
        coll = GetComponentInChildren<Collider2D>();
        ColliderChange();
    }

    // Update is called once per frame
    void Update()
    {
        // logic comes first
        CheckCollisions();

        // then comes the physics
        if (useGravity)
        {
           Gravity();
        }

        // side to side magic forces bleblebleblebwaaabwaaaa
        SideDrag();

        // make sure the force isn't above the max alowed
        KeepForceAtBay();

        // apply the forces on the object
        ApplyForce();
    }

    /// <summary>
    /// check for collisions in all directions and does the grounded-check
    /// </summary>
    private void CheckCollisions()
    {
        RaycastHit2D[] hits;
        // === down 
        hits = Physics2D.RaycastAll(transform.position + offset, Vector3.down, 0.05f + hight);
        if (hits.Length != 0) 
        {
            bool hasTouchedGround = false;
            for(int i = 0; i < hits.Length; i++)
            {
                if(hits[i].collider.transform.parent.tag == "Env")
                {
                    hasTouchedGround = true;
                    grounded = true;
                    SetForceY(0f);
                    transform.position = new Vector3(transform.position.x, hits[i].point.y + hight - offset.y, transform.position.z);
                }
            }
            if (!hasTouchedGround)
            {
                grounded = false;
            }
        }

        // === up
        hits = Physics2D.RaycastAll(transform.position + offset, Vector3.up, 0.05f + hight);
        if (hits.Length != 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.transform.parent.tag == "Env")
                {
                    SetForceY(0f);
                    transform.position = new Vector3(transform.position.x, hits[i].point.y - hight + offset.y, transform.position.z);
                }
            }
        }

        // === left
        hits = Physics2D.RaycastAll(transform.position + offset, Vector3.right, 0.05f + width);
        if (hits.Length != 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.transform.parent.tag == "Env")
                {
                    SetForceX(0f);
                    transform.position = new Vector3(hits[i].point.x + hight - offset.x, transform.position.y, transform.position.z);
                }
            }
        }

        // === right
        hits = Physics2D.RaycastAll(transform.position + offset, Vector3.left, 0.05f + width);
        if (hits.Length != 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.transform.parent.tag == "Env")
                {
                    SetForceX(0f);
                    transform.position = new Vector3(hits[i].point.x - hight + offset.x, transform.position.y, transform.position.z);
                }
            }
        }
    }

    /// <summary>
    /// adds gravitational force
    /// </summary>
    private void Gravity()
    {
        if (!grounded)
        {
            AddForceY((-gravity * mass / 10) * Time.deltaTime);
        }
    }

    /// <summary>
    /// slow down the side forces when they are not 0
    /// </summary>
    private void SideDrag()
    {
        // when going to the left
        if (force.x < 0)
        {
            AddForceX(sideDrag * Time.deltaTime);
            if (force.x > 0) { SetForceX(0); }
        }
        // when going to the right
        if (force.x > 0)
        {
            AddForceX(-sideDrag * Time.deltaTime);
            if (force.x < 0) { SetForceX(0); }
        }
    }

    /// <summary>
    /// makes sure the position is being changed according to how much force is there
    /// </summary>
    private void ApplyForce()
    {
        transform.position += force;
    }

    /// <summary>
    /// this makes sure the forces dont exceed the limits given
    /// </summary>
    private void KeepForceAtBay()
    {
        if(force.x > maxSpeed.x) { force.x = maxSpeed.x; }
        if(force.x < -maxSpeed.x) { force.x = -maxSpeed.x; }
        if(force.y > maxSpeed.y) { force.y = maxSpeed.y; }
        if(force.y < -maxSpeed.y) { force.y = -maxSpeed.y; }
    }

    /// <summary>
    /// called when the collider of the object we're controlling is changed
    /// </summary>
    public void ColliderChange()
    {
        offset = new Vector3(coll.offset.x, coll.offset.y, 0);
        hight = coll.bounds.size.y / 2;
        width = coll.bounds.size.x / 2;
    }

    /// <summary>
    /// returns the grounded value
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        return grounded;
    }

    // absolute setters of force
    public void SetForce(Vector3 _force)
    {
        force = _force;
    }
    public void SetForceX(float _Xforce)
    {
        force.x = _Xforce;
    }
    public void SetForceY(float _Yforce)
    {
        force.y = _Yforce;
    }

    // addetives for force 
    public void AddForce(Vector3 _force)
    {
        force += _force;
    }
    public void AddForceX(float _Xforce)
    {
        force.x += _Xforce;
    }
    public void AddForceY(float _Yforce)
    {
        force.y += _Yforce;
    }
}
