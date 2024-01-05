using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuki
{
    public class RodState : State
    {
        protected Rod rod;
        protected RodData rodData;
        public RodState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
            rod = (Rod)actor;
            rodData = (RodData)data;
        }
    }
}
