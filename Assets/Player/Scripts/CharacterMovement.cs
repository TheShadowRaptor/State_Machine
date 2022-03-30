using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //Sound
    public AudioSource jumpSound;
    public AudioSource wallJumpSound;

    //Moving
    public CharacterController controller;

    public float maxSpeed = 12f;
    public float minSpeed = 0;
    public float strifeSpeed = 5;
    public float backwardsSpeed = 3;
    public float velocitySpeed;
    public float gravity = -9.81f;

    public Vector3 velocity;

    //Jumping
    public float wallJumpHeight = 1;
    public float jumpHeight = 3;

    //ground checking
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //wall checking
    public Transform wallCheck;
    public float wallDistance = 0.4f;
    public LayerMask wallOneMask;
    public LayerMask wallTwoMask;

    /*//vault checking
    public Transform vaultCheck;
    public float vaultDistance = 0.4f;
    public LayerMask vaultMask;*/

    //Bools
    private bool isGrounded;
    private bool canWallJumpOne;
    private bool canWallJumpTwo;
    /*private bool canVault;*/

    private bool oneTimeWallJumpOne;
    private bool oneTimeWallJumpTwo;
    /*private bool oneTimeVaultJump;*/

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        velocitySpeed = minSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerJump();
    }

    void FixedUpdate()
    {
        PlayerMovement();     
    }

    public void PlayerMovement()
    {
        //sets controls
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        //velocity
        if (z > 0)
        {
            
            velocitySpeed = Mathf.Lerp(velocitySpeed, maxSpeed, Time.deltaTime);
        }

        else
        {
            velocitySpeed = velocitySpeed - 30 * Time.deltaTime;
            if(velocitySpeed < minSpeed)
            {
                velocitySpeed = minSpeed;
            }
        }
        //---------------------------------------------

        //Run
        if (z > 0 && Input.GetKey("left shift")) controller.Move(move * velocitySpeed * 0.5f * Time.deltaTime);

        //Foward
        if (z > 0) controller.Move(move * velocitySpeed * Time.deltaTime);

        //Strife
        else if (x > 0 || x < 0) controller.Move(move * strifeSpeed * Time.deltaTime);

        //backwards
        else if (z < 0) controller.Move(move * backwardsSpeed * Time.deltaTime);

        //Gives the player gravity---------------------
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
    public void PlayerJump()
    {       

    //Checks if player is on the ground------------
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            oneTimeWallJumpOne = true;
            oneTimeWallJumpTwo = true;
            /*oneTimeVaultJump = true;*/
        }

        // Rest wall jump
        if (canWallJumpTwo) oneTimeWallJumpOne = true;
        if (canWallJumpOne) oneTimeWallJumpTwo = true;

        //Checks if player can walljump
        canWallJumpOne = Physics.CheckSphere(wallCheck.position, wallDistance, wallOneMask);
        canWallJumpTwo = Physics.CheckSphere(wallCheck.position, wallDistance, wallTwoMask);

        if (canWallJumpOne && velocity.x < 0) velocity.x = 0;

        if (canWallJumpTwo && velocity.x < 0) velocity.x = 0;

        //Checks if player is near a vault
        /*canVault = Physics.CheckSphere(vaultCheck.position, vaultDistance, vaultMask);*/

        //Jump Input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
            jumpSound.Play();
        }

        //Wall Jump
        if (canWallJumpOne && Input.GetButtonDown("Jump") && oneTimeWallJumpOne == true)
        {
            velocity.y = Mathf.Sqrt(wallJumpHeight * -1f * gravity);
            oneTimeWallJumpOne = false;
            wallJumpSound.Play();
        }

        if (canWallJumpTwo && Input.GetButtonDown("Jump") && oneTimeWallJumpTwo == true)
        {
            velocity.y = Mathf.Sqrt(wallJumpHeight * -1f * gravity);
            oneTimeWallJumpTwo = false;
            wallJumpSound.Play();
        }

        /* //Vaulting
         *//*if (canVault && Input.GetButtonDown("Jump") && oneTimeVaultJump == true)
         {
             //can vault
             //speed = speed + 100;
             oneTimeVaultJump = false;*//*
         }     */
    }    
}
