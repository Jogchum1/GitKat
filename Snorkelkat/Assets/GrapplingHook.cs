using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    public DistanceJoint2D joint;
    public Vector2 shootingDir;
    private Vector2 targetLocation;
    private bool lookingLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(gameObject.transform.rotation.y < 0)
        //{
        //    lookingLeft = true;
        //    shootingDir.x = -shootingDir.x;
        //    targetLocation = new Vector2(shootingDir.x + gameObject.transform.position.x, shootingDir.y + gameObject.transform.position.y);
        //}
        //else
        //{
        //    lookingLeft = false;
        //    targetLocation = new Vector2(shootingDir.x + gameObject.transform.position.x, shootingDir.y + gameObject.transform.position.y);
        //}
        
        Grapple();
    }

    public void Grapple()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetLocation = new Vector2(shootingDir.x + gameObject.transform.position.x, shootingDir.y + gameObject.transform.position.y);
            joint.connectedAnchor = targetLocation;
            joint.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            joint.enabled = false;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(gameObject.transform.position, targetLocation);
    }
}
