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
            pod.transform.position = pod.OriginPos.position;
            pod.Drag.IsDraged = false;
            pod.Drag.GetComponent<BoxCollider2D>().enabled = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                _angle += podData.rotationSpeed * _direction * Time.deltaTime;

                if(_angle > podData.angleMax && this._direction == 1)
                {
                    _direction = -1;
                }
                else if (_angle < -podData.angleMax && this._direction == -1)
                {
                    _direction = 1;
                }

                pod.transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);

                if (Input.GetMouseButtonDown(0))
                {
                    pod.FSM.ChangeState(pod.ShootState);
                }
            }
        }
    }

}
