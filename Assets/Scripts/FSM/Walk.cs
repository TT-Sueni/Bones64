using UnityEngine;
public class Walk : BaseState
{
    public Walk(FiniteStateMachine fsm) : base(fsm)
    {
        playerState = PlayerState.Walk;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 camForward = fsm.PlayerMovement.CameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = fsm.PlayerMovement.CameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDir = (camForward * verticalInput + camRight * horizontalInput).normalized;
        Vector3 velocity = new Vector3(moveDir.x * fsm.PlayerMovement.moveSpeed, fsm.PlayerMovement.Rb.velocity.y, moveDir.z * fsm.PlayerMovement.moveSpeed);

        if (CanMove(moveDir))
        {
            fsm.PlayerMovement.Rb.velocity = velocity;
        }

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveInput != Vector2.zero && Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                fsm.ChangeState(PlayerState.Run);
            }
            else if (moveInput == Vector2.zero)
            {
                fsm.ChangeState(PlayerState.Idle);
            }

        }
       /*
         else if (Input.GetKeyDown(KeyCode.Space))
         {
             fsm.ChangeState(PlayerState.Jump);
         }*/
    }
    private bool CanMove(Vector3 moveDir)
    {
        return true;
    }



}
