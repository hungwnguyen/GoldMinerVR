using TMPro;
using UnityEngine;

namespace yuki
{
    public class TextContainer : MonoBehaviour
    {
        [SerializeField] private GameObject _popupTNT;
        [SerializeField] private GameObject _floatingText;
        public static TextContainer Instance;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
                Instance = this;
        }

        public void ShowPopupText(string content)
        {
            _popupTNT.GetComponent<Animator>().SetTrigger("show");
        }

        public void ShowFloatingText(string content)
        {
            _floatingText.GetComponent<TMP_Text>().SetText(content);
            _floatingText.GetComponent<Animator>().SetTrigger("show");
        }
    }
}
