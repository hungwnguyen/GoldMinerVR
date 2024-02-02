namespace yuki
{
    public class PodRewardStrengthState : PodState
    {
        public PodRewardStrengthState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();
            pod.EventHandler.OnAnimationFinished += OnAnimationFinished;
        }

        public override void Exit()
        {
            base.Exit();
            pod.EventHandler.OnAnimationFinished -= OnAnimationFinished;
        }

        private void OnAnimationFinished()
        {
            GameManager.Instance.RestartCoundown();
            Player.Instance.RewardFinished = false;
            pod.Drag.GetStrength = false;
            pod.FSM.ChangeState(pod.RotationState);
        }
       
    }
}
