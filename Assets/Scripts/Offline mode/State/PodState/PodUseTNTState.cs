using UnityEngine;

namespace yuki
{
    public class PodUseTNTState : PodState
    {
        public PodUseTNTState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();
            SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_notnt);
            pod.EventHandler.OnAnimationFinished += OnAnimationFinished;
        }

        public override void Exit()
        {
            base.Exit();

            pod.EventHandler.OnAnimationFinished -= OnAnimationFinished;
        }

        private void OnAnimationFinished()
        {
            pod.Drag.UseTNT();
            pod.Drag.IsDraged = false;
            pod.Drag.ValueEarn = 0;
            pod.Drag.SlowDown = 0;
            pod.transparent = 0.3f;
            Player.Instance.UseItem(Item.TNT);
            UIMain.Instance.SetTNTCount();
            pod.FSM.ChangeState(pod.RewindLightState);
        }
    }
}
