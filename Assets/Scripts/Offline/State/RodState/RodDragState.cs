using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class RodDragState : RodState
    {
        public RodDragState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                if(rod.IsDestroy)
                {
                    rod.FSM.ChangeState(rod.DestroyState);
                }
            }
        }
    }
}
