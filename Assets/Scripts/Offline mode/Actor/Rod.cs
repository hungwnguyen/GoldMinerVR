﻿using UnityEngine;

namespace yuki
{


    public class Rod : Actor, IDragable
    {
        [SerializeField] protected RodData rodData;
        [SerializeField] private GameObject _destroyByTNT;
        private Rigidbody2D rb;
        public RodUndragedState UndragedState { get; private set; }
        public RodDragState DragState { get; private set; }
        private bool _isDraged; public bool IsDraged { get => _isDraged; set => _isDraged = value; }
        public float Value { get => rodData.value; }
        //private Quaternion alpha;
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
            rb = this.GetComponentInChildren<Rigidbody2D>();
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
            GetDragPosition(drag, target);
            drag.SlowDown = rodData.weight;
            GetValueEarn(drag);
            
            transform.SetParent(drag.transform);
        }

        private void GetDragPosition(Drag drag, Transform other)
        {
            transform.SetParent(drag.transform);

            float yScale = 1 / drag.Bound.y;
            float yOffset = GetComponent<Renderer>().bounds.size.y;
            
            if (rodData.type == RodType.GOLD_500)
            {
                transform.localPosition = new Vector2(0, -yOffset / 2 + 0.4f);
            }
            else if (rodData.type == RodType.ROCK)
            {
                transform.localPosition = new Vector2(0, -yOffset / 2 + 0.1f);
            }
            else
            {
                transform.localPosition = new Vector2(0, -yOffset / 2 * yScale);
            }
            LockRotationAndPosition();
            float angle = other.transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        void LockRotationAndPosition()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | 
            RigidbodyConstraints2D.FreezePosition;
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
