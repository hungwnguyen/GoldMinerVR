using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace yuki
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Button input;
        [SerializeField] private float _score = 0; public float Score { get => _score; set => _score = value; }
        [SerializeField] private Actor actor;
        public List<Item> Bag;
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
            Bag = new List<Item>();
            
            Bag.Add(Item.TNT);
            Bag.Add(Item.TNT);
            Bag.Add(Item.TNT);
            Bag.Add(Item.TNT);
            Bag.Add(Item.TNT);
            Bag.Add(Item.TNT);
            Bag.Add(Item.TNT);
        }

        public void OnCLick(){
            isClick = actor.FSM.CurrentState.anim.Equals("rotate");
        }

        public float GetItemNumber(Item item)
        {
            return Bag.Count(i => i == item);
        }

        public void UseItem(Item item)
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                if (Bag[i] == item)
                {
                    Bag.RemoveAt(i);
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
            UIMain.Instance.onSetUI();
        }
    }
}
