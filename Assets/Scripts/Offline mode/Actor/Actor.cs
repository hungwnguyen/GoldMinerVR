using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yuki
{
    public class Actor : MonoBehaviour
    {
        public Animator Anim { get; private set; }
        public FiniteStateMachine FSM { get; private set; }

        protected virtual void Awake()
        {
            FSM = new FiniteStateMachine();
            Anim = GetComponent<Animator>();
            if(Anim == null)
            {
                Anim = GetComponentInParent<Animator>();
            }
            if(Anim == null)
            {
                Anim = GetComponentInChildren<Animator>();
            }
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            FSM.CurrentState.LogicUpdate();
        }

        protected virtual void FixedUpdate()
        {
            FSM.CurrentState.PhysicsUpdate();
        }
    }

}
