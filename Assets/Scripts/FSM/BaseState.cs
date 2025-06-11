using UnityEngine;

public abstract class BaseState
{
    public PlayerState playerState = PlayerState.Idle;
    protected FiniteStateMachine fsm;

    public BaseState(FiniteStateMachine fsm) 
    {
        this.fsm = fsm;
    }
   
   
    public virtual void OnEnter()
    {
        Debug.Log("Entro a " + playerState);
    }
    public virtual void OnUpdate()
    { 
        Debug.Log("Estoy en " + playerState); 
    }
    public virtual void OnExit()
    {
        Debug.Log("salgo de " + playerState);
    }

}
