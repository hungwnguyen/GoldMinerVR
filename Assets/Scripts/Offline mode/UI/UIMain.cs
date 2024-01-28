using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace yuki
{
    public class UIMain : MonoBehaviour
    {
        [Serializable]
        public class MyEvent : UnityEvent { }
        
        [Header("Run when start game"), Space(5f)]
        [FormerlySerializedAs("CustomEvent")]
        [SerializeField] private MyEvent _customEvent = new MyEvent();
        /// <summary>
        /// Run when end game
        /// </summary>
        /// <value></value>
        public MyEvent CustomEvent
        {
            get => _customEvent;
            set { _customEvent = value; }
        }

        [SerializeField] private TMP_Text _target;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _TNT;
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Animator coundown;
        [SerializeField] private EventHandler startGame;

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
            startGame.OnAnimationFinished += Initializtion;
            onSetUI += SetScore;
            onSetUI += SetLevel;
            onSetUI += SetTarget;
            onSetUI += SetTNTCount;
        }

        public void EnventStartGame()
        {
            UISystemProfilerApi.AddMarker("MyEvent.CustomEvent", this);
            _customEvent.Invoke();
            SoundManager.CreatePlayBgMusic(SoundManager.Instance.audioClip.aud_gamePlayMusic[UnityEngine.Random.Range(0, 2)]);
        }

        private void Initializtion(){
            GameManager.Instance.Initialization();
        }

        public void Coundown(){
            this.coundown.SetTrigger("countdown");
        }

        public void SetScore(){
            _score.SetText(Player.Instance.Score.ToString());
        }

        public void SetTNTCount(){
            _TNT.SetText(Player.Instance.TNTCount.ToString());
        }

        public void SetLevel(){
            _level.SetText(GameManager.Instance.Level.ToString());
        }

        public void SetTarget(){
            _target.SetText(GameManager.Instance.TargetScore.ToString());
        }

        public void SetTime(float time = 90)
        {
            _time.SetText(time.ToString());
        }
    }
}
