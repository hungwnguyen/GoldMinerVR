using System.Collections;
using UnityEngine;

namespace yuki
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private float _timeLevel;
        private int _level = 0; public int Level { get => _level; set => _level = value; }
        private float _currentTime; 
        private float _targetScore; public float TargetScore { get => _targetScore; set => _targetScore = value; }
        private bool _isLevelEnd; public bool IsLevelEnd { get => _isLevelEnd; set => _isLevelEnd = value; }
        private float _offset = 0;
        private float _timeUpdate = 1f;
        private float _currentTimeToUpdate = 0;
        public static LevelManager Instance;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
                Instance = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            Initialization();
        }

        public void StopCountdown()
        {
            StopAllCoroutines();
        }

        public void RestartCoundown()
        {
            StartCoroutine(Countdown());
        }

        void Initialization()
        {
            _isLevelEnd = false;
            _level++;
            _currentTime = _timeLevel;
            CalcualateTargetScore();
            Player.Instance.Initializtion();
            Player.Instance.SetGamePropertyInPlayer(_level, _currentTime, _targetScore);
            UIMain.Instance.SetTime(_currentTime);
            StartCoroutine(Countdown());
        }

        IEnumerator Countdown()
        {
            while (_currentTime > 0)
            {
                Player.Instance.SetGamePropertyInPlayer(_level, _currentTime, _targetScore);
                if (this._currentTime < 12f){
                    SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_dongho);
                }
                _currentTime -= 1;
                UIMain.Instance.SetTime(_currentTime);
                yield return new WaitForSeconds(1.0f);
            }
            CheckIfCountdownEnd();
        }

        private void CheckIfCountdownEnd()
        {
            if (Player.Instance.playerData.Score >= _targetScore)
            {
                LevelEnd();
                UIShop.Instance.SetStatus(true);
            }
            else
            {
                LevelEnd();
                Debug.Log("Game Over!!");
            }
        }

        public void LevelEnd()
        {
            DestroyAllRod();
            SoundManager.Instance.StopAllFXLoop();
            _isLevelEnd = true;
        }

        public void DestroyAllRod()
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Rod");
            foreach (GameObject go in gos)
            {
                Rod rod = go.GetComponentInChildren<Rod>(true);
                if (rod != null)
                {
                    rod.Destroy();
                }
            }
        }

        public void GetPositionAllRod()
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Rod");
            foreach (GameObject go in gos)
            {
                Rod rod = go.GetComponentInChildren<Rod>(true);
                if(rod != null)
                {
                    if(!Player.Instance.playerData.AllRodPositionInScreen.ContainsKey(rod.CurrentPosition))
                    {
                        Player.Instance.playerData.AllRodPositionInScreen.Add(rod.CurrentPosition, rod.rodData.type);
                    }
                }
            }
        }

        private void CalcualateTargetScore()
        {
            if(_level == 1)
            {
                _targetScore = 1000;
            }
            else
            {
                _offset += 1;
                _targetScore += _level * (_targetScore + _offset);
            }
        }

        public void NextLevel()
        {
            Initialization();
            foreach (Item item in Player.Instance.playerData.Bag)
            {
                switch (item)
                {
                    case Item.DIAMOND_UP:
                        Player.Instance.playerData.DiamondBuff = 2;
                        Player.Instance.UseItem(Item.DIAMOND_UP);
                        break;
                    case Item.ROCK_UP:
                        Player.Instance.playerData.RockBuff = 5;
                        Player.Instance.UseItem(Item.ROCK_UP);
                        break;
                    case Item.POWER_UP:
                        Player.Instance.playerData.PowerBuff = 10;
                        Player.Instance.UseItem(Item.POWER_UP);
                        break;
                }
            }
            UIShop.Instance.SetStatus(false);
            Spawner.Instance.SpawnRod();
            GetPositionAllRod();
        }
    }
}
