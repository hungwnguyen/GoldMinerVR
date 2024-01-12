using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuki
{
    public class PodUseTNTState : PodState
    {
        public PodUseTNTState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();

            
        }

        private void OnAnimationFinished()
        {
            pod.Drag.UseTNT();
            pod.Drag.IsDraged = false;
            Player.Instance.UseItem(Item.TNT);
            pod.FSM.ChangeState(pod.RewindLightState);
        }
    }
}
