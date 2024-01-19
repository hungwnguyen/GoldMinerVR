using UnityEngine;

namespace yuki
{
    public class PodShootState : PodState
    {
        public PodShootState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                pod.transform.Translate(Vector3.down * podData.strength * Time.deltaTime);

                if (pod.CheckIfOutOfScreen() || pod.Drag.IsDraged)
                {
                    pod.FSM.ChangeState(pod.RewindLightState);
                    SoundManager.Instance.StopFXLoop(SoundManager.Instance.audioClip.aud_thaday);
                    SoundManager.CreatePlayFXLoop(SoundManager.Instance.audioClip.aud_keoday);
                }
            }
        }
    }
}

