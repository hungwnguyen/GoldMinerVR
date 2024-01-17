using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class PodRewardState : PodState
    {
        public PodRewardState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();

            pod.Drag.FinishDrag();
            GameManager.Instance.StopCountdown();
            Debug.Log(pod.Drag.GetStrength);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                Debug.Log("1");
                if (pod.Drag.GetStrength)
                {
                    Debug.Log("Strength");
                    pod.FSM.ChangeState(pod.RewardStrengthState);
                }
                else if (Player.Instance.RewardFinished)
                {
                    GameManager.Instance.RestartCoundown();
                    Player.Instance.RewardFinished = false;
                    pod.FSM.ChangeState(pod.RotationState);
                }
            }
        }
    }
}
