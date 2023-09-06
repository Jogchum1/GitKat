using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBasic : MonoBehaviour
{
    private Rigidbody2D rigid;

    [SerializeField]
    private float moveSpeed;

    public bool phaseUnlocked;


    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    void PhaseCheck()
    {
        if (phaseUnlocked && Input.GetKey(KeyCode.LeftShift))
        {

        }
    }

    private void Movement()
    {
        rigid.MovePosition(transform.position + new Vector3(Input.GetAxis("Horizontal") * 1, Input.GetAxis("Vertical") * 1, 0) * moveSpeed * Time.fixedDeltaTime);
    }
}
