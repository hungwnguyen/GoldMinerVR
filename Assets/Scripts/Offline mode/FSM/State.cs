using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yuki
{
    public class State
    {
        protected Actor actor;
        protected ActorData data;
        protected string anim;
        protected bool isExistingState;

        public State(Actor actor, ActorData data, string anim)
        {
            this.actor = actor;
            this.data = data;
            this.anim = anim;
        }

        public virtual void Enter()
        {
            actor.Anim.SetBool(anim, true);
            isExistingState = false;
            DoCheck();
        }

        public virtual void Exit()
        {
            actor.Anim.SetBool(anim, false);
            isExistingState = true;
        }

        public virtual void LogicUpdate() { }

        public virtual void PhysicsUpdate()
        {
            DoCheck();
        }

        public virtual void DoCheck() { }

    }

}
