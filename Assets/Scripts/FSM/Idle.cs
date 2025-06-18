using UnityEngine;

public class Idle : BaseState
{
    public Idle(FiniteStateMachine fsm) : base(fsm) 
    {
        playerState = PlayerState.Idle;
    }

    
    public override void OnUpdate()
    {
        base.OnUpdate();

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveInput != Vector2.zero)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                fsm.ChangeState(PlayerState.Run);
            }
            
            else
            {
                fsm.ChangeState(PlayerState.Walk);
            }
        }
        /* else if (Input.GetKeyDown(KeyCode.R))
         {
             fsm.ChangeState(PlayerState.Swap);
        }/* 
         else if (Input.GetKeyDown(KeyCode.Space))
         {
             fsm.ChangeState(PlayerState.Jump);
         }*/



    }


}
