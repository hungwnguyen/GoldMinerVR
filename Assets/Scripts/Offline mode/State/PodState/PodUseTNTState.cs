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

            pod.EventHandler.OnAnimationFinished += OnAnimationFinished;
        }

        public override void Exit()
        {
            base.Exit();

            pod.EventHandler.OnAnimationFinished -= OnAnimationFinished;
        }

        private void OnAnimationFinished()
        {
            pod.Drag.UseTNT();
            pod.Drag.IsDraged = false;
            pod.Drag.ValueEarn = 0;
            pod.Drag.SlowDown = 0;
            Player.Instance.UseItem(Item.TNT);
            pod.FSM.ChangeState(pod.RewindLightState);
        }
    }
}
