using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yuki
{
    public class PodShootState : PodState
    {
        public PodShootState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                pod.Drag.Dragging();
                pod.transform.Translate(Vector3.down * podData.strength * Time.deltaTime);

                if (pod.CheckIfOutOfScreen() || pod.Drag.IsDraged)
                {
                    pod.FSM.ChangeState(pod.RewindLightState);
                }
            }
        }
    }
}

