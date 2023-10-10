using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    public DistanceJoint2D joint;
    public GameObject grappleChecker;
    //public List<>
    // Start is called before the first frame update
    void Start()
    {
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        Grapple();
    }

    public void Grapple()
    {
        
        
    }

    private void OnDrawGizmos()
    {

    }
}
