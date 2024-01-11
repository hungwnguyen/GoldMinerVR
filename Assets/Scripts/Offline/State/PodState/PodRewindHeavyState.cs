using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                pod.transform.Translate(Vector3.up * (podData.strength - pod.Drag.SlowDown) * Player.Instance.PowerBuff * Time.deltaTime);

                if (pod.Drag.SlowDown < podData.strength / 2)
                {
                    pod.FSM.ChangeState(pod.RewindLightState);
                }
            }
        }
    }
}
