using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yuki
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _timeLevel;
        private int _level = 1; public int Level { get => _level; 
        set => _level = value > 99999 ? 99999 : value; }
        private float _currentTime; 
        private float _targetScore; public float TargetScore { get => _targetScore; 
        set => _targetScore = value > 999999 ? 999999 : value; }
        public static GameManager Instance;
        [SerializeField] private List<Background> bgs;


        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
                Instance = this;
        }

        private void ChangeBG(){
            foreach(Background bg in bgs){
                bg.SetBG();
            }
        }

        public void ResetGame(){
            Level = 1;
            TargetScore = 0;
            ChangeBG();
        }
       
        public void StopCountdown()
        {
            StopAllCoroutines();
        }
        
        public void RestartCoundown()
        {
            StartCoroutine(Countdown());
        }

        public void Initialization()
        {
            Player.Instance.RewardFinished = false;
            Pod.Instance.FSM.ChangeState(Pod.Instance.RotationState);
            _level++;
            _currentTime = _timeLevel;
            UIMain.Instance.SetTime(_currentTime);
            StartCoroutine(Countdown());
        }

        IEnumerator Countdown()
        {
            while (_currentTime > 0)
            {
                if (this._currentTime <= 10){
                    SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_dongho);
                    UIMain.Instance.Coundown();
                }
                yield return new WaitForSeconds(1.0f);
                _currentTime -= 1;
                UIMain.Instance.SetTime(_currentTime);
            }
            CheckIfCountdownEnd();
        }

        public void CheckIfCountdownEnd()
        {
            Player.Instance.ResetLevel();
            float score = Player.Instance.Score;
            if (score >= _targetScore)
            {
                _targetScore = score;
                SoundManager.DisableAllMusic();
                UIPopup.Instance.PlayPopUpCongat();
                SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_muctieu);
                Pod.Instance.FSM.ChangeState(Pod.Instance.PodIdleState);
                Spawner.Instance.DestroyAllRod();
            }
            else
            {
                UIPopup.Instance.LostAppear();
            }
        }

        public void NextLevel()
        {
            ChangeBG();
            UIShop.Instance.SetStatus(false);
            UIPopup.Instance.ReSetAmin();
            Spawner.Instance.SpawnRodLevel(Level);
        }
    }
}
