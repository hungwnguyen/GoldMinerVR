using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace yuki
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Button input;
        [SerializeField] private float _score = 0; public float Score { get => _score; set => _score = value; }
        private List<Item> _bag; public List<Item> Bag { get => _bag; set => _bag = value; }
        private float _powerBuff; public float PowerBuff { get => _powerBuff; set => _powerBuff = value; }
        private float _diamondBuff; public float DiamondBuff { get => _diamondBuff; set => _diamondBuff = value; }
        private float _rockBuff; public float RockBuff { get => _rockBuff; set => _rockBuff = value; }
        private bool _rewardFinished; public bool RewardFinished { get => _rewardFinished; set => _rewardFinished = value; }
        public bool isClick;
        public static Player Instance;

        void Awake()
        {
            Application.targetFrameRate = 60;
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
                Instance = this;
            isClick = false;
            input.onClick.AddListener(OnCLick);
        }

        public void OnCLick(){
            isClick = true;
        }

        public float GetItemNumber(Item item)
        {
            return _bag.Count(i => i == item);
        }

        public void UseItem(Item item)
        {
            for (int i = 0; i < _bag.Count; i++)
            {
                if (_bag[i] == item)
                {
                    _bag.RemoveAt(i);
                    break;
                }
            }
        }

        public void Initializtion()
        {
            transform.position = new Vector3(Screen.Instance.PlayerRect.center.x, Screen.Instance.PlayerRect.yMin, 0);
            _rewardFinished = false;
            _powerBuff = 1;
            _diamondBuff = 1;
            _rockBuff = 1;
            _bag = new List<Item>();
            _bag.Add(Item.TNT);
            _bag.Add(Item.TNT);
            _bag.Add(Item.TNT);
            UIMain.Instance.onSetUI();
        }
    }
}
