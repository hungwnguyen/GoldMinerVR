﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;

namespace yuki
{
    public class UIMain : MonoBehaviour
    {
        [SerializeField] private GameObject _element;
        [SerializeField] private TMP_Text _target;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _TNT;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _level;
        
        public static UIMain Instance;

        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
            _score.SetText(Player.Instance.Score.ToString());
            _TNT.SetText(Player.Instance.GetItemNumber(Item.TNT).ToString());
            _level.SetText(GameManager.Instance.Level.ToString());
            _target.SetText(GameManager.Instance.TargetScore.ToString());
        }

        public void SetTime(float time)
        {
            _time.SetText(time.ToString());
        }

        public void SetStatus(bool status)
        {
            _element.SetActive(status);
        }

        
    }
}
