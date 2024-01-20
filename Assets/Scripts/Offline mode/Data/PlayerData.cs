using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    [CreateAssetMenu(fileName = "Player", menuName = "Data/Player")]
    public class PlayerData : ScriptableObject
    {
        private int _level = 0; public int Level { get => _level; set => _level = value; }
        private float _currentTime; public float CurrentTime { get => _currentTime; set => _currentTime = value; }
        private float _targetScore; public float TargetScore { get => _targetScore; set => _targetScore = value; }
        private float _score = 0; public float Score { get => _score; set => _score = value; }
        private float _powerBuff; public float PowerBuff { get => _powerBuff; set => _powerBuff = value; }
        private float _diamondBuff; public float DiamondBuff { get => _diamondBuff; set => _diamondBuff = value; }
        private float _rockBuff; public float RockBuff { get => _rockBuff; set => _rockBuff = value; }
        private List<Item> _bag; public List<Item> Bag { get => _bag; set => _bag = value; }
        private Dictionary<Vector2, RodType> _allRodPostitionInScreen; public Dictionary<Vector2, RodType> AllRodPositionInScreen { get => _allRodPostitionInScreen; set => _allRodPostitionInScreen = value; }

    }
}
