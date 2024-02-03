using UnityEngine;

namespace yuki
{
    public class PodRewindHeavyState : PodRewindState
    {
        public PodRewindHeavyState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!isExistingState)
            {
                if (podData.strength - pod.Drag.SlowDown + Player.Instance.PowerBuff > 3)
                {
                    pod.FSM.ChangeState(pod.RewindLightState);
                }
            }
        }
    }
}
