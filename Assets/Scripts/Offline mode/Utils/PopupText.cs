using System.Collections;
using UnityEngine;

namespace yuki
{
    public class PopupText : MonoBehaviour
    {
        public EventHandler EventHandler { get; private set; }
        private Animator animator;

        void Awake()
        {
            EventHandler = GetComponent<EventHandler>();
            // Lấy Animator component từ GameObject
            animator = GetComponent<Animator>();
            EventHandler.OnAnimationFinished += OnAnimationFinished;
        }

        private void OnAnimationFinished()
        {
            Player.Instance.RewardFinished = true;
            StartCoroutine(UpdateScore());
            animator.Play("Zoom out");
        }

        IEnumerator UpdateScore(){
            yield return new WaitForSeconds(0.68f);
            UIMain.Instance.SetScore();
            UIMain.Instance.SetTNTCount();
        }
    }
}
