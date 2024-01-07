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

    public enum RodTypeName
    {
        GOLD,
        DIAMOND,
        MOUSE,
        RANDOM_BAG,
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
        public RodTypeName Type 
        {
            get 
            {
                if (rodData.type.ToString().Contains(RodTypeName.GOLD.ToString()))
                    return RodTypeName.GOLD;
                else if (rodData.type.ToString().Contains(RodTypeName.DIAMOND.ToString()))
                    return RodTypeName.DIAMOND;
                else if (rodData.type.ToString().Contains(RodTypeName.MOUSE.ToString()))
                    return RodTypeName.MOUSE;
                else if (rodData.type.ToString().Contains(RodTypeName.RANDOM_BAG.ToString()))
                    return RodTypeName.RANDOM_BAG;
                else if (rodData.type.ToString().Contains(RodTypeName.ROCK.ToString()))
                    return RodTypeName.ROCK;
                else if (rodData.type.ToString().Contains(RodTypeName.BOOM.ToString()))
                    return RodTypeName.BOOM;
                else
                    return RodTypeName.GOLD;
            }
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

        public virtual void Draged(Drag drag)
        {
            _isDraged = true;
            drag.SlowDown = rodData.weight;
            if(Type == RodTypeName.DIAMOND)
            {
                drag.ValueEarn = rodData.value * Pod.Instance.DiamondBuff;
            }
            else if(Type == RodTypeName.ROCK)
            {
                drag.ValueEarn = rodData.value * Pod.Instance.RockBuff;
            }
            else
            {
                drag.ValueEarn = rodData.value;
            }
            transform.SetParent(drag.transform);
            
        }

        public void DestroyRod()
        {
            Destroy(this.gameObject);
        }
    }
}
