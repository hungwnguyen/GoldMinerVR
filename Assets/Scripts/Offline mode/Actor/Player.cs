using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace yuki
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Button input;
        [SerializeField] private float _score = 0; public float Score { get => _score; 
        set => _score = value > 999999 ? 999999 : value; }
        [SerializeField] private Actor actor;
        public int TNTCount;
        private float _powerBuff; public float PowerBuff { get => _powerBuff; set => _powerBuff = value; }
        private float _diamondBuff; public float DiamondBuff { get => _diamondBuff; set => _diamondBuff = value; }
        private float _rockBuff; public float RockBuff { get => _rockBuff; set => _rockBuff = value; }
        public bool isLucky;
        private bool _rewardFinished; public bool RewardFinished { get => _rewardFinished; set => _rewardFinished = value; }
        public bool isClick, isUseTNT;
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
            isUseTNT = false;
            TNTCount = 9;
            ResetLevel();
            input.onClick.AddListener(OnCLick);
        }

        public void OnCLick(){
            string value = actor.FSM.CurrentState.anim;
            isClick = value.Equals("rotate");
            isUseTNT = value.Length > 5 ? value.Substring(0, 6).Equals("rewind") : false;
        }

        public void ResetLevel()
        {
            transform.position = new Vector3(Screen.Instance.PlayerRect.center.x, Screen.Instance.PlayerRect.yMin, 0);
            _rewardFinished = false;
            _powerBuff = 1;
            _diamondBuff = 1;
            _rockBuff = 1;
            isLucky = false;
        }
    }
}
