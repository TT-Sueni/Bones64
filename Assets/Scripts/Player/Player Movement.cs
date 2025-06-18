

using Cinemachine;
using System;
using System.Collections.Generic;

using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Rigidbody Rb;

    [field: SerializeField] public float moveSpeed { get; private set; } = 10f;
    [field: SerializeField] public Transform CameraTransform { get; private set; }

    public float jumpForce;
    private FiniteStateMachine fsm = new FiniteStateMachine();
    

    bool readyToJump;
    public float jumpCooldown;
    bool grounded;
    public float playerHeight;
    public LayerMask whatIsGround;




    /*
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    float maxRampAngle = 20f;
    */
    private void Start()
    {
        Rb = GetComponent<Rigidbody>();
        readyToJump = true;
        fsm.Initialize(this);
        //Rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;
        //
        //readyToJump = true;


        

    }
  

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        
        
        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;

            
            Rb.velocity = new Vector3(Rb.velocity.x, 0f, Rb.velocity.z);

            Rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);


            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
         
        fsm.OnUpdate();
        /*
         grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

         MyInput();
         SpeedControl();

         // handle drag
         if (grounded)
             rb.drag = groundDrag;
         else
             rb.drag = 0;*/
    }
    private void ResetJump()
    {
        readyToJump = true;
    }


    /*
        private void MyInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            // when to jump
            if (Input.GetKey(jumpKey) && readyToJump && grounded)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }
        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        private void MovePlayer()
        {
             moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;        
            if (CanMove(moveDirection)) 
            {
                if (grounded)
                {
                    rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
                }
                else if (!grounded)
                    rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
        }

        private bool CanMove(Vector3 moveDirection)
        {
            Terrain terrain = Terrain.activeTerrain;
            Vector3 relativePos = GetMapPos();
            Vector3 normal = terrain.terrainData.GetInterpolatedNormal(relativePos.x, relativePos.z);
            float angle = (Vector3.Angle(normal, Vector3.up));
            Debug.Log("Angulo : " + angle);

            float currentHeight = terrain.SampleHeight(rb.position);
            float nextHeight = terrain.SampleHeight(rb.position + moveDirection * 5 );//ver


            if (angle > maxRampAngle && nextHeight > currentHeight)
                return false;

                return true;


        }
        Vector3 GetMapPos()
        { 
            Vector3 pos= rb.position;
            Terrain terrain = Terrain.activeTerrain;  

            return new Vector3((pos.x - terrain.transform.position.x) / terrain.terrainData.size.x, 0 , 
                (pos.z - terrain.transform.position.z) / terrain.terrainData.size.z);
        }



        private void Jump()
        {
            // reset y velocity
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        private void ResetJump()
        {
            readyToJump = true;
        }*/
    public void Swap(Rigidbody newRB)
    {
        
        Rb = newRB;
    }


}






