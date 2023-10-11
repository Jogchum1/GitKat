using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    public DistanceJoint2D joint;
    public GameObject grappleChecker;
    public List<GrapplingPoint> grapPointList = new List<GrapplingPoint>();
    public LayerMask grapplePoints;

    [SerializeField] private float checkRadius = 5f;

    public Transform bestTarget = null;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(bestTarget != null)
        {
            distance = Vector2.Distance(grappleChecker.transform.position, bestTarget.position);
        }

        if(distance < checkRadius + 1)
        {
            Grapple(bestTarget);
        }
        else
        {
            bestTarget = null; 
        }
               
        FindGrapplingPoints();
    }

    Transform FindGrapplingPoints()
    {
        var hits = Physics2D.OverlapCircleAll(grappleChecker.transform.position, checkRadius, grapplePoints);
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = grappleChecker.transform.position;

        foreach (Collider2D hit in hits)
        {
            Vector3 directionToTarget = hit.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            //Debug.Log(dSqrToTarget);
            if (dSqrToTarget < closestDistanceSqr && dSqrToTarget < checkRadius)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = hit.transform;
            }
        }
        return bestTarget;
    }

    public void Grapple(Transform target)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(target != null)
            {
                joint.enabled = true;
                joint.connectedAnchor = target.position;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            joint.enabled = false;
        }
        
    }

    private void OnDrawGizmos()
    {
        if(bestTarget != null)
            Gizmos.DrawLine(transform.position, bestTarget.position);

        Gizmos.DrawWireSphere(grappleChecker.transform.position, checkRadius);
    }
}
