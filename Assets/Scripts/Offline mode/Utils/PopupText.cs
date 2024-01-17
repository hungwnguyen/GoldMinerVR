using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class PopupText : MonoBehaviour
    {
        public EventHandler EventHandler { get; private set; }

        void Awake()
        {
            EventHandler = GetComponent<EventHandler>();
        }

        void OnEnable()
        {
            EventHandler.OnAnimationFinished += OnAnimationFinished;
        }

        void OnDisable() 
        {
            EventHandler.OnAnimationFinished -= OnAnimationFinished;
        }

        private void OnAnimationFinished()
        {
            Player.Instance.RewardFinished = true;
            Destroy(gameObject);
        }

        
    }
}
