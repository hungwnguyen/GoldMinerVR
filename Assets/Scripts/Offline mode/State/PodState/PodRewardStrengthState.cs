using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class PodRewardStrengthState : PodState
    {
        public PodRewardStrengthState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                if (Player.Instance.RewardFinished)
                {
                    GameManager.Instance.RestartCoundown();
                    Player.Instance.RewardFinished = false;
                    pod.Drag.GetStrength = false;
                    pod.FSM.ChangeState(pod.RotationState);
                }
            }
        }
    }
}
