using TMPro;
using UnityEngine;

namespace yuki
{
    public class TextContainer : MonoBehaviour
    {
        [SerializeField] private GameObject _popupText;
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
            var go = Instantiate(_popupText, transform.position, Quaternion.identity, transform);
            go.GetComponent<TMP_Text>().SetText(content);
        }

        public void ShowFloatingText(string content)
        {
            var go = Instantiate(_floatingText, transform.position, Quaternion.identity, transform);
            go.GetComponent<TMP_Text>().SetText(content);
        }
    }
}
