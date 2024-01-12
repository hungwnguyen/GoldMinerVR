using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float _timeLevel;
        private int _level = 0; public int Level { get => _level; set => _level = value; }
        private float _currentTime;
        private float _targetScore;
        private float _offset = 0;
        public static GameManager Instance { get; private set; }

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        void Start()
        {
            Initialization();
        }

        void Initialization()
        {
            _level++;
            _currentTime = _timeLevel;
            CalcualateTargetScore();
            Player.Instance.Initializtion();
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
            if (Player.Instance.Score >= _targetScore)
            {
                UIMain.Instance.SetStatus(false);
                Spawner.Instance.DestroyAllRod();
                UIShop.Instance.SetStatus(true);
                UIShop.Instance.AddItemToShop();
            }
            else
            {
                //TODO: Add gameover 
                Debug.Log("Game Over!!");
            }
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
            Initialization();
            foreach (Item item in Player.Instance.Bag)
            {
                switch (item)
                {
                    case Item.DIAMOND_UP:
                        Player.Instance.DiamondBuff = 2;
                        Player.Instance.UseItem(Item.DIAMOND_UP);
                        break;
                    case Item.ROCK_UP:
                        Player.Instance.RockBuff = 5;
                        Player.Instance.UseItem(Item.ROCK_UP);
                        break;
                    case Item.POWER_UP:
                        Player.Instance.PowerBuff = 2;
                        Player.Instance.UseItem(Item.POWER_UP);
                        break;
                }
            }
            UIShop.Instance.SetStatus(false);
            UIMain.Instance.SetStatus(true);
            Spawner.Instance.SpawnRod();
        }
    }
}
