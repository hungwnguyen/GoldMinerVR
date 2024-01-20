using System;
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
        [SerializeField] private TMP_Text _target;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _TNT;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _level;

        public delegate void OnSetUI();
        public OnSetUI onSetUI;
        
        public static UIMain Instance;

        void Awake()
        {
            if (Instance != null){
                Destroy(this);
            }
            else{
                Instance = this;
            }
            onSetUI += SetScore;
            onSetUI += SetLevel;
            onSetUI += SetTarget;
            onSetUI += SetTNTCount;
        }

        public void SetScore(){
            _score.SetText(Player.Instance.playerData.Score.ToString());
        }

        public void SetTNTCount(){
            _TNT.SetText(Player.Instance.GetItemNumber(Item.TNT).ToString());
        }

        public void SetLevel(){
            _level.SetText(LevelManager.Instance.Level.ToString());
        }

        public void SetTarget(){
            _target.SetText(LevelManager.Instance.TargetScore.ToString());
        }

        public void SetTime(float time = 90)
        {
            _time.SetText(time.ToString());
        }
    }
}
