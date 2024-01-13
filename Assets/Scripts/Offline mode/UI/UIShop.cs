using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace yuki
{
    public class UIShop : MonoBehaviour
    {
        [SerializeField] private GameObject _element;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Transform _content;
        [SerializeField] private List<GameObject> _prefab;
        public static UIShop Instance;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _nextLevelButton.onClick.AddListener(NextLevel);
        }

        private void NextLevel() 
        {
            GameManager.Instance.NextLevel();
        }

        public void AddItemToShop()
        {
            int rand = UnityEngine.Random.Range(0, _prefab.Count);
            for (int i = 0; i < _prefab.Count; i++)
            {
                if (i == rand)
                    continue;
                Instantiate(_prefab[i], _content);
            }
        }

        public void SetStatus(bool status)
        {
            _element.SetActive(status);
        }

        
    }
}
