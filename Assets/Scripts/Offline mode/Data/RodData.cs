using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{

    [CreateAssetMenu(fileName = "RodData", menuName = "Data/Rod")]
    public class RodData : ActorData
    {
        [Header("Mouse"), Tooltip("For mouse")]
        public float speed = 6.0f;

        [Space(3)]
        [Header("Normal")]
        public float weight = 8;
        public float value = 100;
        public RodType type = RodType.GOLD_100;
        public AudioClip audioClip;
    }
}
