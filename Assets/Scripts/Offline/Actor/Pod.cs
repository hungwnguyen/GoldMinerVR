using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace yuki
{
    public class Pod : Actor
    {
        [SerializeField] private PodData podData;
        [SerializeField] private Vector2 _position;
        

        #region State
        public PodRotationState RotationState { get; private set; }
        public PodRewindLightState RewindLightState { get; private set; }
        public PodRewindHeavyState RewindHeavyState { get; private set; }
        public PodUseTNTState UseTNTState { get; private set; }
        public PodShootState ShootState { get; private set; }
        #endregion
        private Vector3 _originPos; public Vector3 OriginPos { get => _originPos; set => _originPos = value; }
        public Drag Drag { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            RotationState = new PodRotationState(this, podData, "rotate");
            RewindLightState = new PodRewindLightState(this, podData, "rewindLight");
            RewindHeavyState = new PodRewindHeavyState(this, podData, "rewindHeavy");
            UseTNTState = new PodUseTNTState(this, podData, "useTNT");
            ShootState = new PodShootState(this, podData, "shoot");
        }

        protected override void Start()
        {
            base.Start();

            Drag = GetComponentInChildren<Drag>();
            _originPos = Screen.Instance.PlayerRect.center + _position;
            FSM.Initialization(RotationState);
        }

        #region Check
        public bool CheckIfOutOfScreen()
        {
            return Vector3.Distance(transform.position, _originPos) > (Mathf.Sqrt(Mathf.Pow(Screen.Instance.Width / 2, 2) + Mathf.Pow(Screen.Instance.Height - Screen.Instance.PlayerRect.center.y, 2)) - 2);
        }

        public bool CheckIfDragFinish()
        {
            return Vector3.Distance(transform.position, _originPos) < 0.2f;
        }
        #endregion
    }
}

