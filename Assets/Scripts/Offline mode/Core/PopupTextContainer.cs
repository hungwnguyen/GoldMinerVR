using TMPro;
using UnityEngine;

namespace yuki
{
    public class PopupTextContainer : MonoBehaviour
    {
        [SerializeField] private GameObject _popupText;
        public static PopupTextContainer Instance;

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
    }
}
