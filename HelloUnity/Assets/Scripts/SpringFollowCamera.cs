using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFollowCamera : MonoBehaviour
{
    public Transform target; //assign to character in Unity IDE
    public Transform camera; //assign to camera; (actual position)
    public float hDistance = 2.0f;
    public float vDistance = 5.0f;
    public float dampConstant = 10f; //dampening factor; smooths speed gradually
    public float springConstant = 5f; //spring factor;
    public Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() //for smoother transitions
    {
        // tPos, tUp, tForward = Position, up, and forward vector of target

        // Camera position is offset from the target position
        Vector3 idealEye = target.position - target.forward * hDistance + target.up * vDistance;

        // The direction the camera should point is from the target to the camera position
        Vector3 cameraForward = target.position - camera.position;

        // Compute the acceleration of the spring, and then integrate
        Vector3 displacement = camera.position - idealEye;

        Vector3 springAccel = (-springConstant * displacement) - (dampConstant * velocity);

        // Update the camera's velocity based on the spring acceleration
        velocity += springAccel * Time.deltaTime;

        camera.position += velocity * Time.deltaTime;

        // Set the camera's position and rotation with the new values
        // This code assumes that this code runs in a script attached to the camera
        transform.position = camera.position;
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}
