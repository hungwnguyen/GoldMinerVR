namespace yuki
{
    public class RodUndragedState : RodState
    {
        private bool _isMouse;
        private Mouse _mouse;

        public RodUndragedState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (rod is Mouse)
            {
                _isMouse = true;
                _mouse = (Mouse)rod;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                if (_isMouse)
                {
                    _mouse.Move();
                }

                if (rod.IsDraged)
                {
                    rod.FSM.ChangeState(rod.DragState);
                }
            }
        }
    }
}
