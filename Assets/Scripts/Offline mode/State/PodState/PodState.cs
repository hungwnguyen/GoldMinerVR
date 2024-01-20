﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuki
{
    public class PodState : State
    {
        protected Pod pod;
        protected PodData podData;
        public PodState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
            pod = (Pod)actor;
            podData = (PodData)data;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (LevelManager.Instance.IsLevelEnd && this != pod.RotationState)
            {
                pod.FSM.ChangeState(pod.RotationState);
            }
        }
    }
}
