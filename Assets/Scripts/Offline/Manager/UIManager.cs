using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace yuki
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _TNT;
        [SerializeField] private TMP_Text _powerBuff;
        public static UIManager Instance;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            SetPowerBuffEnable(false);
        }

        void Update()
        {
            _score.SetText("$ " + PlayerManager.Instance.Score.ToString());
            _TNT.SetText("TNT: " + PlayerManager.Instance.GetTNTNumber().ToString());
        }

        public void SetPowerBuffText(float powerBuffTime)
        {
            _powerBuff.SetText("Time buff: " + Mathf.RoundToInt(powerBuffTime).ToString());
        }

        public void SetPowerBuffEnable(bool enable)
        {
            _powerBuff.enabled = enable;
        }
    }
}
