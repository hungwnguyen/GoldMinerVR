using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace yuki
{
    public class PodRotationState : PodState
    {
        private float _angle;
        private float _direction;

        public PodRotationState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _direction = 1;
            pod.transform.position = pod.OriginPos;
            pod.Drag.IsDraged = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                _angle += podData.rotationSpeed * _direction;

                if(_angle > podData.angleMax || _angle < -podData.angleMax)
                {
                    _direction *= -1;
                }

                pod.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);

                if (Input.GetMouseButtonDown(0))
                {
                    pod.FSM.ChangeState(pod.ShootState);
                }
                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                //    pod.FSM.ChangeState(pod.ShootState);
                //}
            }
        }
    }

}
