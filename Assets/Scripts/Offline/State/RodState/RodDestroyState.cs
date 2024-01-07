using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class RodDestroyState : RodState
    {
        public RodDestroyState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();

            OnAnimationFinished();
            //rod.EventHandler.OnAnimationFinished += OnAnimationFinished;
        }

        public override void Exit() 
        { 
            base.Exit();

            //rod.EventHandler.OnAnimationFinished -= OnAnimationFinished;
        }

        private void OnAnimationFinished()
        {
            Debug.Log("On destroy state");
            if(rod is Mouse)
            {
                Mouse mouse = (Mouse)rod;
                mouse.DestroyMouse();
                rod.DestroyRod();
            }
            else if(rod is Boom && rod.IsDraged)
            {
                
            }
            else
            {
                rod.DestroyRod();
            }
            
        }
    }
}
