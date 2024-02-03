using UnityEngine;

namespace yuki
{
    public class Boom : Rod, IExplodeable
    {
        [SerializeField] private GameObject boom;

        public Explode Explode { get; private set; }

        protected override void Start()
        {
            base.Start();

            Explode = GetComponentInChildren<Explode>();
        }

        public void Exploded()
        {
            Instantiate(boom, transform.position, Quaternion.identity);
            Explode.Exploding();
        }

        public override void Draged(Drag drag, Transform target)
        {
            base.Draged(drag, target);
            Player.Instance.isDragBoom = true;
            Exploded();
        }

    }
}
