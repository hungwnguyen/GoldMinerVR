using UnityEngine;
using yuki;

public class PodIdleState : PodState
{
    public PodIdleState(Actor actor, ActorData data, string anim = "") : base(actor, data, anim)
    {
    }

    public override void Enter()
    {
        pod.transform.position = pod.OriginPos.position;
        pod.transform.rotation = pod.OriginPos.rotation;
        pod.Anim.SetBool("rotate", true);
        pod.Anim.SetBool("rewindLight", false);
        pod.Anim.SetBool("rewindHeavy", false);
        pod.Anim.SetBool("useTNT", false);
        pod.Anim.SetBool("shoot", false);
        pod.Anim.SetBool("reward", false);
        pod.Anim.SetBool("rewardStrength", false);
        pod.Drag.GetComponent<Animator>().SetBool("drag", false);
        pod.Drag.SlowDown = 0;
    }

    public override void Exit()
    {
       
    }
}