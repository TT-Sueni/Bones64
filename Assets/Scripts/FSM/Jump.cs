using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Jump : BaseState
{
    public Jump(FiniteStateMachine fsm) : base(fsm)
    {
        playerState = PlayerState.Jump;
    }
    public override void OnUpdate()
    {
        
        Debug.Log("Jump");
        fsm.PlayerMovement.Rb.velocity = new Vector3(fsm.PlayerMovement.Rb.velocity.x, 0f, fsm.PlayerMovement.Rb.velocity.z);

        fsm.PlayerMovement.Rb.AddForce(fsm.PlayerMovement.gameObject.transform.up * fsm.PlayerMovement.jumpForce, ForceMode.Impulse);
    }
    
}
