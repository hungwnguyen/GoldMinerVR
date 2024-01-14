using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace yuki
{
    public class PodRewindLightState : PodRewindState
    {
        public PodRewindLightState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }


        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                pod.transform.Translate(Vector3.up * (podData.strength - pod.Drag.SlowDown + Player.Instance.PowerBuff) * Time.deltaTime);

                if(pod.Drag.SlowDown >= podData.strength / 2)
                {
                    pod.FSM.ChangeState(pod.RewindHeavyState);
                }
            }
        }
    }
}

