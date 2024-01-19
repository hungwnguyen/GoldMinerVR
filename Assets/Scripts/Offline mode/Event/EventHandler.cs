using System;
using UnityEngine;

namespace yuki
{
    public class EventHandler : MonoBehaviour
    {
        public event Action OnAnimationFinished;

        public void OnAnimationFinishedTrigger() => OnAnimationFinished?.Invoke();
    }
}
