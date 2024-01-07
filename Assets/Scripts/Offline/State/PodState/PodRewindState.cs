using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace yuki
{
    public class PodRewindState : PodState
    {
        public PodRewindState(Actor actor, ActorData data, string anim) : base(actor, data, anim)
        {
        }


        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(!isExistingState)
            {
                if(Input.GetMouseButtonDown(1) && PlayerManager.Instance.GetTNTNumber() > 0 && pod.Drag.IsDraged)
                {
                    pod.Drag.DestroyRodByTNT();
                    pod.Drag.IsDraged = false;
                    PlayerManager.Instance.UseItem(Item.TNT);
                }

                if(!pod.Drag.IsDraged)
                {
                    pod.transform.Translate(Vector3.up * podData.speed * pod.PowerBuff * Time.deltaTime);
                }
                else
                {
                    pod.transform.Translate(Vector3.up * (podData.speed - pod.Drag.SlowDown) * pod.PowerBuff * Time.deltaTime);
                }

                if(Vector3.Distance(pod.transform.position, pod.OriginPos) < 0.2f)
                {
                    if (pod.Drag.IsDraged)
                    {
                        string randStr = pod.Drag.GetRandomBagItem();
                        if (randStr != "")
                        {
                            if(randStr == pod.Drag.RandomBagDict[RandomBagItem.STRENGTH_UP])
                            {
                                pod.PowerBuff = 2;
                                pod.PowerBuffTime = 10;
                                pod.StartPowerBuff();
                            }
                            pod.ShowPopupText(randStr);
                        }
                        else
                        {
                            PlayerManager.Instance.Score += pod.Drag.ValueEarn;
                        }
                        
                        pod.Drag.DestroyRod();
                    }
                    pod.FSM.ChangeState(pod.RotationState);
                }
            }
        }
    }
}

