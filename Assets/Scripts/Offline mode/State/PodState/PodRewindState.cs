using UnityEngine;

namespace yuki
{
    public class PodRewindState : PodState
    {
        public PodRewindState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();

            pod.Drag.GetComponent<BoxCollider2D>().enabled = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                if (Player.Instance.isUseTNT && Player.Instance.TNTCount > 0 && pod.Drag.IsDraged)
                {
                    Player.Instance.isUseTNT = false;
                    pod.FSM.ChangeState(pod.UseTNTState);
                }
                
                if (pod.CheckIfDragFinish())
                {
                    SoundManager.Instance.StopFXLoop(SoundManager.Instance.audioClip.aud_keoday);
                    if (pod.Drag.IsDraged)
                    {
                        pod.FSM.ChangeState(pod.RewardState);
                    }
                    else
                    {
                        pod.FSM.ChangeState(pod.RotationState);
                    }
                } else {
                    pod.transform.position = Vector2.MoveTowards(pod.transform.position, pod.OriginPos.position, (podData.strength - pod.Drag.SlowDown + Player.Instance.PowerBuff) * Time.deltaTime);
                }
            }
        }
    }
}
