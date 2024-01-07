using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private float _timeLevel;
        private int _level; public int Level { get => _level; set => _level = value; }
        public static LevelManager Instance;
        private float _currentTime;
        private float _targetScore;
        private float _offset;

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            _level = 1;
            _currentTime = _timeLevel;
            _offset = 0;
            Initialization();
        }

        void Initialization()
        {
            CalcualateTargetScore();
            UIMain.Instance.SetTime(_currentTime);
            StartCoroutine(Countdown());
        }

        IEnumerator Countdown()
        {
            while (_currentTime > 0)
            {
                _currentTime -= 1;

                UIMain.Instance.SetTime(_currentTime);

                yield return new WaitForSeconds(1.0f);
            }
            CheckIfCountdownEnd();
        }

        private void CheckIfCountdownEnd()
        {
            if (Pod.Instance.PowerBuff != 1)
            {
                Pod.Instance.PowerBuff = 1;
                Pod.Instance.PowerBuffTime = 0;
            }
            Pod.Instance.SetStatus(false);
            UIMain.Instance.SetStatus(false);
            Spawner.Instance.DestroyAllRod();
            UIShop.Instance.SetStatus(true);
            UIShop.Instance.AddItemToShop();
            //if (PlayerManager.Instance.Score >= _targetScore)
            //{
            //    if(Pod.Instance.PowerBuff != 1)
            //    {
            //        Pod.Instance.PowerBuff = 1;
            //        Pod.Instance.PowerBuffTime = 0;
            //    }
            //Pod.Instance.SetStatus(false);
            //    UIMain.Instance.SetStatus(false);
            //    Spawner.Instance.DestroyAllRod();
            //    UIShop.Instance.SetStatus(true);
            //    UIShop.Instance.AddItemToShop();
            //}
            //else
            //{
            //    //TODO: Add gameover 
            //    Debug.Log("Game Over!!");
            //}
        }

        private int CalcualateTargetScore()
        {
            if(_level == 1)
            {
                _targetScore = 1000;
            }
            else
            {
                _offset += 200;
                _targetScore += _level * (_targetScore + _offset);
            }
            return (int) _targetScore;
        }

        public void NextLevel()
        {
            Debug.Log("Okk");
            _currentTime = _timeLevel;
            _level++;
            _targetScore = CalcualateTargetScore();
            foreach(Item item in PlayerManager.Instance.Items)
            {
                switch (item)
                {
                    case Item.DIAMOND_UP:
                        Pod.Instance.DiamondBuff = 2;
                        break;
                    case Item.ROCK_UP:
                        Pod.Instance.RockBuff = 5;
                        break;
                    case Item.POWER_UP:
                        Pod.Instance.PowerBuff = 2;
                        Pod.Instance.PowerBuffTime = 30;
                        Pod.Instance.StartPowerBuff();
                        break;
                }
            }
            Pod.Instance.SetStatus(true);
            UIShop.Instance.SetStatus(false);
            UIMain.Instance.SetStatus(true);
            Spawner.Instance.SpawnRod();
            Initialization();
        }
    }
}
