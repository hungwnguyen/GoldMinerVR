﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yuki
{
    public class FiniteStateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialization(State iniState)
        {
            CurrentState = iniState;
            CurrentState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}

