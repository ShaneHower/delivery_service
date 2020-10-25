using UnityEngine;

public class Creature : MonoBehaviour
{
    protected CharacterController characterController;

    // public variables that can be tuned
    protected float walkSpeed;
    protected float rotateRef;
    public float turnSmoothTime = 0.12f;
    public float speedSmoothTime = 0.035f;
    public float gravity = 20.0f;
    public float height;

    // ref variables 
    float speedSmoothVelocity;
    float turnSmoothVelocity;

    // trigger animation variables
    float currentSpeed;

    // variables needed for walk/run
    private float inputMag;
    protected float horizontal;
    protected float vertical;
    Vector3 playerVelocity;

    protected virtual void Start()
    {
        // this will be overidden by children
    }

    protected virtual void Update()
    {   
        move();

        height -= gravity * Time.deltaTime;
        playerVelocity.y = height;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    void move()
    {
        Vector2 input = new Vector2(horizontal, vertical);
        Vector2 inputDir = input.normalized;
        inputMag = inputDir.magnitude;

        if (inputDir != Vector2.zero)
        {
            //using atan2 because it deals with the denominator being 0.  Arctan2 returns angle in radians.
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + rotateRef;
            // SmoothDampAngle takes current angle and desired angle .  also takes turn smooth time (the amount of seconds it will take to go from the current value to the new value), 
            // and turnSmoothVelocity. ref lets the function modify the value? 
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }
        float targetSpeed = walkSpeed * inputMag;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);
        playerVelocity = transform.forward * currentSpeed;
    }
}

