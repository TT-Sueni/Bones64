using UnityEngine;

public class Run : BaseState
{
    public Run(FiniteStateMachine fsm) : base(fsm)
    {
        playerState = PlayerState.Run;
    }
    
    public override void OnUpdate() 
    {
        base.OnUpdate();
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveInput != Vector2.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                fsm.ChangeState(PlayerState.Walk);
            }
            else if (moveInput == Vector2.zero)
            {
                fsm.ChangeState(PlayerState.Idle);
            }

        }
    }

   

}
