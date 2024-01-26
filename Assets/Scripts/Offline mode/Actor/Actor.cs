using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yuki
{
    public class Actor : MonoBehaviour
    {
        public Animator Anim { get; private set; }
        public FiniteStateMachine FSM { get; private set; }
        public float transparent {private get; set;}
        private float currentTime;
        protected virtual void Awake()
        {
            FSM = new FiniteStateMachine();
            Anim = GetComponent<Animator>();
            currentTime = 0;
            transparent = 0;
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
            if (currentTime < transparent){
                currentTime += Time.deltaTime;
            }else{
                currentTime = 0;
                transparent = 0;
                FSM.CurrentState.LogicUpdate();
            }
            
        }

        protected virtual void FixedUpdate()
        {
            FSM.CurrentState.PhysicsUpdate();
        }
    }

}
