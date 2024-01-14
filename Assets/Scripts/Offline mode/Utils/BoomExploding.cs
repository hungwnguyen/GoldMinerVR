using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class BoomExploding : MonoBehaviour
    {
        public Animator Anim { get; private set; }
        public EventHandler EventHandler { get; private set; }

        void Awake()
        {
            Anim = GetComponent<Animator>();
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
            Destroy(this.gameObject);
        }
    }
}
