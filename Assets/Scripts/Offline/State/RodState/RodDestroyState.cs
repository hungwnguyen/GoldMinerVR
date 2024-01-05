using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(rod is Mouse && !rod.IsDraged)
            {
                Mouse mouse = (Mouse)rod;
                mouse.DestroyMouse();
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
