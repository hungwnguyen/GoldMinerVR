using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class PodRewindState : PodState
    {
        public PodRewindState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();

            pod.Drag.GetComponent<BoxCollider2D>().enabled = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                if (Input.GetMouseButtonDown(1) && Player.Instance.GetItemNumber(Item.TNT) > 0 && pod.Drag.IsDraged)
                {
                    pod.FSM.ChangeState(pod.UseTNTState);

                }

                if (pod.CheckIfDragFinish())
                {
                    if (pod.Drag.IsDraged)
                    {
                        pod.Drag.FinishDrag();
                    }
                    pod.FSM.ChangeState(pod.RotationState);
                }
            }
        }
    }
}
