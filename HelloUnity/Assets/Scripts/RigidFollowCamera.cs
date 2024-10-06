using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFollowCamera : MonoBehaviour
{
    public Transform target; //assigned to character
    public float hDistance; 
    public float vDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void LateUpdate() //is called after update 
    {
        // tPos, tUp, tForward = Position, up, and forward vector of target

        // Camera position is offset from the target position
        Vector3 eye = target.position - target.forward * hDistance + target.up * vDistance;

        // The direction the camera should point is from the target to the camera position
        Vector3 cameraForward = cameraForward = target.position - eye;

        // Set the camera's position and rotation with the new values
        // This code assumes that this code runs in a script attached to the camera
        transform.position = eye;
        transform.rotation = Quaternion.LookRotation(cameraForward);
    }
}
