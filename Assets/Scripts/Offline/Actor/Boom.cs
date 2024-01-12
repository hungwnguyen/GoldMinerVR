using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yuki
{
    public class Boom : Rod, IExplodeable
    {
        public Explode Explode { get; private set; }

        protected override void Start()
        {
            base.Start();

            Explode = GetComponentInChildren<Explode>();
        }

        public void Exploded()
        {
            Explode.Exploding();
        }

        public override void Draged(Drag drag)
        {
            base.Draged(drag);

            Exploded();
        }
    }
}
