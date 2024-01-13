using UnityEngine;

namespace yuki
{
    public class Boom : Rod
    {
        [SerializeField] private GameObject boom;
        [SerializeField] private Sprite boomTarget;

        protected override void Start()
        {
            base.Start();
        }

        public override void Draged(Drag drag, Transform target)
        {
            Destroy(Instantiate(boom, this.transform.position, Quaternion.identity), 0.833f);
            this.GetComponent<SpriteRenderer>().sprite = boomTarget;
            base.Draged(drag, target);
        }
    }
}
