using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    public float linearSpeed = 10.0f;
    public float turningSpeed = 5.0f;
    bool isMoving; //for animation

    Vector3 move;

    Animator animator; //the animator for Thyra
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        isMoving = animator.GetBool("isRunning"); //from the animator param
        move = new Vector3();

    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 velocity = Vector3.zero;
        Quaternion avelocity = Quaternion.identity;

        isMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            velocity = transform.forward * linearSpeed;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            velocity = -transform.forward * linearSpeed;
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.A)) //left
        {
            avelocity = Quaternion.Euler(0, horizontal * turningSpeed, 0);
            isMoving = true;
        }

        if (Input.GetKey(KeyCode.D)) //right
        {
            avelocity = Quaternion.Euler(0, horizontal * turningSpeed, 0);
            isMoving = true;
        }

        controller.Move(velocity * Time.deltaTime);
        transform.rotation *= avelocity;

        animator.SetBool("isRunning", isMoving); //update animator bool

    }
}