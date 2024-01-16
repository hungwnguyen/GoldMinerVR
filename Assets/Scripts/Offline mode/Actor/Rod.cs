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
        private Quaternion alpha;
        public string Type
        {
            get
            {
                if (rodData.type.ToString().Contains("DIAMOND"))
                {
                    return "DIAMOND";
                }
                else if (rodData.type.ToString().Contains("GOLD"))
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
            float x, y;
            float angle = Vector3.Angle(drag.transform.parent.position - target.position, Vector3.down);
            x = Mathf.Sin(angle * Mathf.PI / 180f) * 0.5f * GetComponent<Collider2D>().bounds.size.y / 2;
            y = Mathf.Tan(angle * Mathf.PI / 180f) * x;
            if (drag.transform.position.x > target.position.x)
            {
                this.alpha = Quaternion.Euler(target.parent.transform.rotation.x, target.parent.transform.rotation.y, angle);
            }
            else
            {
                x *= -1;
                this.alpha = Quaternion.Euler(target.parent.transform.rotation.x, target.parent.transform.rotation.y, -angle);
            }
            if (!(this is Mouse))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, alpha, 0.5f);
            }
            transform.position = new Vector3(drag.transform.GetChild(0).position.x + x,
                                         drag.transform.GetChild(0).position.y - (GetComponent<Collider2D>().bounds.size.y / 2), -1);
            Debug.Log(x + " " + y);
            drag.SlowDown = rodData.weight;
            GetValueEarn(drag);
            
            transform.SetParent(drag.transform);
        }

        private void GetValueEarn(Drag drag)
        {
            SoundManager.CreatePlayFXSound(rodData.audioClip);
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
            if (Parent != null)
            {
                Destroy(Parent.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
