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
        public RodUndragedState UndragedState { get; private set; }
        public RodDragState DragState { get; private set; }
        public RodDestroyState DestroyState { get; private set; }

        private bool _isDestroy; public bool IsDestroy { get => _isDestroy; set => _isDestroy = value; }
        private bool _isDraged; public bool IsDraged { get => _isDraged; set => _isDraged = value; }

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

        public virtual void Draged(Drag drag)
        {
            _isDraged = true;
            drag.SlowDown = rodData.weight;
            drag.ValueEarn = rodData.value;
            if(transform.parent != null)
            {
                Transform oldParent = transform.parent;
                Destroy(oldParent.gameObject);
            }
            //Debug.Log(oldParent.name);
            transform.SetParent(drag.transform);
            
        }

        public void DestroyRod()
        {
            Destroy(this.gameObject);
        }
    }
}
