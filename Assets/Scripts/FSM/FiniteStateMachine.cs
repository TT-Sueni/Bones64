using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FiniteStateMachine
{
    private BaseState currentStates;
    private List<BaseState> states = new List<BaseState> ();
    public PlayerMovement PlayerMovement{ get; private set; }
    public void Initialize(PlayerMovement playerMovement)
    {
        PlayerMovement = playerMovement;

        states.Add(new Idle(this));
        states.Add(new Walk(this));
        states.Add(new Run(this));
        states.Add(new Jump(this));
        //states.Add(new SwapController(this));

        ChangeState(PlayerState.Idle);
    }

 

    public void OnUpdate()
    {

        if (PlayerMovement.Rb.gameObject.layer == 8)
            currentStates.OnUpdate();
        
    }
    public void ChangeState(PlayerState playerState)
    {
        if(currentStates != null)
            currentStates.OnExit();
        currentStates = FindState(playerState);
        currentStates.OnEnter();
    }

    public BaseState FindState(PlayerState playerState)
    {
        foreach (BaseState state in states)
            if (state.playerState == playerState)
                return state;

        return null;
    }
        

    
}

