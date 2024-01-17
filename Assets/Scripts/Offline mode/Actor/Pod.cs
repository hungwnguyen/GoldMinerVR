﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace yuki
{
    public class Pod : Actor
    {
        [SerializeField] private PodData podData;
        [SerializeField] private Transform _originPos;
        public Transform OriginPos { get => _originPos; }
        #region State
        public PodRotationState RotationState { get; private set; }
        public PodRewindLightState RewindLightState { get; private set; }
        public PodRewindHeavyState RewindHeavyState { get; private set; }
        public PodUseTNTState UseTNTState { get; private set; }
        public PodShootState ShootState { get; private set; }
        public PodRewardState RewardState { get; private set; }
        public PodRewardStrengthState RewardStrengthState { get; private set; }
        #endregion
        public Drag Drag { get; private set; }
        public EventHandler EventHandler { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();

            RotationState = new PodRotationState(this, podData, "rotate");
            RewindLightState = new PodRewindLightState(this, podData, "rewindLight");
            RewindHeavyState = new PodRewindHeavyState(this, podData, "rewindHeavy");
            UseTNTState = new PodUseTNTState(this, podData, "useTNT");
            ShootState = new PodShootState(this, podData, "shoot");
            RewardState = new PodRewardState(this, podData, "reward");
            RewardStrengthState = new PodRewardStrengthState(this, podData, "rewardStrength");
        }

        protected override void Start()
        {
            base.Start();

            EventHandler = GetComponentInParent<EventHandler>();
            Drag = GetComponentInChildren<Drag>();
            FSM.Initialization(RotationState);
        }

        #region Check
        public bool CheckIfOutOfScreen()
        {
            return !(transform.position.x >= Screen.Instance.GameplayRect.xMin && 
                transform.position.x <= Screen.Instance.GameplayRect.xMax &&
                transform.position.y >= Screen.Instance.GameplayRect.yMin);
        }

        public bool CheckIfDragFinish()
        {
            return _originPos.position.y - transform.position.y < 0.2f;
        }
        #endregion
    }
}

