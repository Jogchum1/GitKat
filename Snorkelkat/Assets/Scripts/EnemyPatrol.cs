using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxSpeed = 4f;
    public float aggroDistance = 5f;
    public float minWaypointDistance = 5f;

    public List<Transform> waypoints = new List<Transform>();
    public GameObject groundedCheck;
    public LayerMask groundLayers;

    private GameObject player;
    private Rigidbody2D rb;

    public Transform target;
    private bool foundClosestWaypoint = true;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if(playerDistance < aggroDistance)
        {
            FollowPlayer();
        }
        else
        {
            Patrol();
        }

        if(target != null)
        {
            Move();
        }
        
    }

    public void Move()
    {
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        Vector2 force = direction * moveSpeed;
        rb.velocity = force;

        if (rb.velocity.x > 0.05f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < -0.05f)
        {
            transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void Patrol()
    {
        if(foundClosestWaypoint == false)
        {
            float closestWP = Mathf.Infinity;
            foreach (Transform wp in waypoints)
            {
                float wpDistance = Vector3.Distance(gameObject.transform.position, wp.transform.position);
                if(wpDistance < closestWP)
                {
                    target = wp;
                    Debug.Log(wp);
                }
            }
            foundClosestWaypoint = true;
        }

        float distanceToTarget = Vector3.Distance(gameObject.transform.position, target.position);

        if(distanceToTarget < minWaypointDistance)
        {
            Debug.Log("Select next waypoint");
            //make next waypoint in list the target
            if(target == waypoints[0])
            {
                target = waypoints[+1];
            }
            else
            {
                target = waypoints[0];
            }
        }
       
    }

    public void FollowPlayer()
    {
        foundClosestWaypoint = false;
        if (IsGrounded())
        {
            target = player.gameObject.transform;
        }
        else
        {
            target = null;
            rb.velocity = Vector2.zero;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundedCheck.transform.position, 0.2f, groundLayers);
    }

    public void Flip()
    {

    }
}
