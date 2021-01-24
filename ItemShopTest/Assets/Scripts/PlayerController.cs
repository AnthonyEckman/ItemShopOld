using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //8 directional movement that will stop and face the direction as soon as input stops
    
    public float velocity = 5;
    public float turnspeed = 10;

    Vector2 input;
    float angle;

    Quaternion targetRotation;
    Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        GetInput();

        if(Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1)
        {
            return;
        }

        CalculateDirection();
        Rotate();
        Move();
    }

    //Input based on HOrizontal(a,d,<,>) and vertical (w,s,^,v) keys
    private void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

    }

    //direction relative to camera's rotation
    private void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;

    }

    //rotate toward the calculated angle
    private void Rotate()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);

    }

    //this player may only move along its own forward axis
    private void Move()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;
        
    }

}
