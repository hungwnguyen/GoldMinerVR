using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Instance = this;
        }

        public void ShowPopupText(string content)
        {
            var go = Instantiate(_popupText, transform.position, Quaternion.identity, transform);
            go.GetComponent<TMP_Text>().SetText(content);
        }
    }
}
