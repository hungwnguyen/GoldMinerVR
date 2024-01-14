using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace yuki
{
    public class Pod : Actor
    {
        [SerializeField] private PodData podData;
        [SerializeField] Transform _originPos;
        public Transform OriginPos { get => _originPos; }
        #region State
        public PodRotationState RotationState { get; private set; }
        public PodRewindLightState RewindLightState { get; private set; }
        public PodRewindHeavyState RewindHeavyState { get; private set; }
        public PodUseTNTState UseTNTState { get; private set; }
        public PodShootState ShootState { get; private set; }
        #endregion
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
            FSM.Initialization(RotationState);
        }

        #region Check
        public bool CheckIfOutOfScreen()
        {
            return !Drag.GetComponent<Renderer>().isVisible;
        }

        public bool CheckIfDragFinish()
        {
            return _originPos.position.y - transform.position.y < 0.2f;
        }
        #endregion
    }
}

