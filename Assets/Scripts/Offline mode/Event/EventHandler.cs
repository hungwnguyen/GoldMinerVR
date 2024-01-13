using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class EventHandler : MonoBehaviour
    {
        public event Action OnAnimationFinished;

        public void OnAnimationFinishedTrigger() => OnAnimationFinished?.Invoke();
    }
}
