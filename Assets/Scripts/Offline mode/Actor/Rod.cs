using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    

    public class Rod : Actor, IDragable
    {
        [SerializeField] protected RodData rodData;
        [SerializeField] private GameObject _destroyByTNT;
        public RodUndragedState UndragedState { get; private set; }
        public RodDragState DragState { get; private set; }
        private bool _isDraged; public bool IsDraged { get => _isDraged; set => _isDraged = value; }
        public string Type 
        {
            get {
                if(rodData.type.ToString().Contains("DIAMOND")) 
                {
                    return "DIAMOND";
                }
                else if(rodData.type.ToString().Contains("GOLD"))
                {
                    return "GOLD";
                }
                else
                {
                    return rodData.type.ToString();
                }
            }
        }
        public Transform Parent { get; private set; }

        public EventHandler EventHandler { get; private set; }
        protected override void Awake()
        {
            base.Awake();

            UndragedState = new RodUndragedState(this, rodData, "undraged");
            DragState = new RodDragState(this, rodData, "drag");
        }

        protected override void Start()
        {
            base.Start();

            Parent = transform.parent;
            EventHandler = GetComponentInChildren<EventHandler>();
            FSM.Initialization(UndragedState);
        }

        public virtual void Draged(Drag drag, Transform target)
        {
            _isDraged = true;
            float angle = Vector3.Angle(drag.transform.parent.position - target.position, Vector3.down);
            Quaternion tg = Quaternion.Euler(target.parent.transform.rotation.x,
                                            target.parent.transform.rotation.y,
                                            drag.transform.position.x > target.position.x ? angle : - angle);
            
            if(!(this is Mouse))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, tg, 0.5f);
            }
            
            drag.SlowDown = rodData.weight;
            GetValueEarn(drag);
            transform.position = new Vector3(drag.transform.position.x,
                                         drag.transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2), -1);
            transform.SetParent(drag.transform);
        }

        private void GetValueEarn(Drag drag)
        {
            if (Type == "DIAMOND")
            {
                drag.ValueEarn = rodData.value * Player.Instance.DiamondBuff;
            }
            else if (Type == "ROCK")
            {
                drag.ValueEarn = rodData.value * Player.Instance.RockBuff;
            }
            else
            {
                drag.ValueEarn = rodData.value;
            }
        }

        public void DestroyByTNT()
        {
            Instantiate(_destroyByTNT, transform.position, Quaternion.identity);
            Destroy();
        }

        public void Destroy()
        {
            if(Parent != null)
            {
                Destroy(Parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
