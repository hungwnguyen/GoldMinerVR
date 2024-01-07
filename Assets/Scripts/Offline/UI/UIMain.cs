using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace yuki
{
    public class UIMain : MonoBehaviour
    {
        [SerializeField] private GameObject _element;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _TNT;
        [SerializeField] private TMP_Text _powerBuff;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _level;
        public static UIMain Instance;

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
            _level.SetText("Level: " + LevelManager.Instance.Level.ToString());
        }

        public void SetPowerBuffText(float powerBuffTime)
        {
            _powerBuff.SetText("Time buff: " + Mathf.RoundToInt(powerBuffTime).ToString());
        }

        public void SetPowerBuffEnable(bool enable)
        {
            _powerBuff.gameObject.SetActive(enable);
        }

        public void SetTime(float time)
        {
            _time.SetText("Time: " + time.ToString());
        }

        public void SetStatus(bool status)
        {
            _element.SetActive(status);
        }
    }
}
