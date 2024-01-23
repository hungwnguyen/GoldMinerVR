using UnityEngine;

namespace yuki
{
    public class PodRewardState : PodState
    {
        public PodRewardState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();
            pod.Drag.FinishDrag();
            GameManager.Instance.StopCountdown();
            SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_congqua);
            //Debug.Log(pod.Drag.GetStrength);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                if (pod.Drag.GetStrength)
                {
                    pod.FSM.ChangeState(pod.RewardStrengthState);
                }
                else if (Player.Instance.RewardFinished)
                {
                    GameManager.Instance.RestartCoundown();
                    if (!pod.Drag.GetTNT){
                        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_congtien);
                    }
                    else{
                        pod.Drag.GetTNT = false;
                    }
                    Player.Instance.RewardFinished = false;
                    pod.FSM.ChangeState(pod.RotationState);
                }
            }
        }
    }
}
