using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour

{
    public float linearSpeed = 2.0f;
    public float turningSpeed = 5.0f;
    bool isMoving; //for animation

    Animator animator; //the animator for Thyra
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        isMoving = animator.GetBool("isRunning"); //from the animator param
        
    }

    // Update is called once per frame
    void Update()
    {
        //reads current input / location of char      
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //WASD movement
        if (Input.GetKey(KeyCode.W))
        {
            direction.z += 1.0f; //forward 1 space
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1.0f;
            //transform.forward = direction;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction.z -= 1.0f;
            //transform.forward = direction;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1.0f;
            //transform.forward = direction;
        }

        direction.Normalize(); //standardizes the vector!

        if (direction.magnitude > 0.1f) //checks if the character is moving, if yes then Vector != (0, 0, 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            //slerp instead of lerp for a smoother transition
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);

            Vector3 velocity = direction * linearSpeed;
            controller.Move(velocity * Time.deltaTime);
            //transform.Translate(direction * linearSpeed * Time.deltaTime, Space.World); //movement, relevant to the world
            isMoving = true;
        }

        else
        {
            isMoving = false; //idle
        }

        animator.SetBool("isRunning", isMoving); //update animator bool
    }
}

