using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public enum RodType
    {
        GOLD_100,
        GOLD_200,
        GOLD_500,
        DIAMOND,
        RANDOM_BAG,
        MOUSE_1,
        MOUSE_600,
        MOUSE_900,
        MOUSE_1200,
        ROCK,
        BOOM
    }

    public class Rod : Actor, IDragable
    {
        [SerializeField] protected RodData rodData;
        [SerializeField] private bool debug;
        public RodUndragedState UndragedState { get; private set; }
        public RodDragState DragState { get; private set; }
        public RodDestroyState DestroyState { get; private set; }
        private bool _isDestroy; public bool IsDestroy { get => _isDestroy; set => _isDestroy = value; }
        private bool _isDraged; public bool IsDraged { get => _isDraged; set => _isDraged = value; }
        public RodType Type 
        {
            get => rodData.type;
        }

        public EventHandler EventHandler { get; private set; }
        protected override void Awake()
        {
            base.Awake();

            UndragedState = new RodUndragedState(this, rodData, "undraged");
            DragState = new RodDragState(this, rodData, "drag");
            DestroyState = new RodDestroyState(this, rodData, "destroy");
        }

        protected override void Start()
        {
            base.Start();

            EventHandler = GetComponentInChildren<EventHandler>();
            FSM.Initialization(UndragedState);
        }

        public virtual void Draged(Drag drag, Transform target)
        {
            _isDraged = true;
            float angle = Vector3.Angle(drag.transform.parent.position - target.position, Vector3.down);
            if (debug)
            {
                Debug.Log(angle);
            }
            Quaternion tg = Quaternion.Euler(target.parent.transform.rotation.x,
                                            target.parent.transform.rotation.y,
                                            drag.transform.position.x > target.position.x ? angle : - angle);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, tg, 0.5f);
            drag.SlowDown = rodData.weight;
            if(Type == RodType.DIAMOND)
            {
                drag.ValueEarn = rodData.value * Player.Instance.DiamondBuff;
            }
            else if(Type == RodType.ROCK)
            {
                drag.ValueEarn = rodData.value * Player.Instance.RockBuff;
            }
            else
            {
                drag.ValueEarn = rodData.value;
            }
            transform.position = new Vector3(drag.transform.position.x,
                                         drag.transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2), -1);
            transform.SetParent(drag.transform);
        }

        public void DestroyRod()
        {
            Destroy(gameObject);
        }
    }
}
