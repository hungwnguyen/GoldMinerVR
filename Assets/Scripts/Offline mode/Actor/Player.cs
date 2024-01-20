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
        [SerializeField] public PlayerData playerData;
        
        private bool _rewardFinished; public bool RewardFinished { get => _rewardFinished; set => _rewardFinished = value; }
        public bool isClick;
        public static Player Instance;

        void Awake()
        {
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
            if (isClick) return;
            isClick = true;
        }

        public float GetItemNumber(Item item)
        {
            return playerData.Bag.Count(i => i == item);
        }

        public void UseItem(Item item)
        {
            for (int i = 0; i < playerData.Bag.Count; i++)
            {
                if (playerData.Bag[i] == item)
                {
                    playerData.Bag.RemoveAt(i);
                    break;
                }
            }
        }

        public void Initializtion()
        {
            transform.position = new Vector3(Screen.Instance.PlayerRect.center.x, Screen.Instance.PlayerRect.yMin, 0);
            _rewardFinished = false;
            playerData.PowerBuff = 0;
            playerData.DiamondBuff = 1;
            playerData.RockBuff = 1;
            playerData.Bag = new List<Item>();
            playerData.AllRodPositionInScreen = new Dictionary<Vector2, RodType>();
            UIMain.Instance.onSetUI();
        }

        public void SetGamePropertyInPlayer(int level, float currentTime, float targetScore)
        {
            playerData.Level = level;
            playerData.CurrentTime = currentTime;
            playerData.TargetScore = targetScore;
        }
    }
}
