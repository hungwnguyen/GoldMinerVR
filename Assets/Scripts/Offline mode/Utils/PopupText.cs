using System.Collections;
using UnityEngine;

namespace yuki
{
    public class PopupText : MonoBehaviour
    {
        public EventHandler EventHandler { get; private set; }
        private Animator animator;

        private int lastIndex, currentIndex;
        void Awake()
        {
            EventHandler = GetComponent<EventHandler>();
            lastIndex = 1;
            // Lấy Animator component từ GameObject
            animator = GetComponent<Animator>();
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
            currentIndex = this.animator.GetCurrentAnimatorClipInfoCount(0);
            if (currentIndex == 1 && lastIndex == 0){
                Player.Instance.RewardFinished = true;
                StartCoroutine(UpdateScore());
            }
            lastIndex = currentIndex;
        }

        IEnumerator UpdateScore(){
            yield return new WaitForSeconds(0.68f);
            UIMain.Instance.SetScore();
            UIMain.Instance.SetTNTCount();
        }
    }
}
