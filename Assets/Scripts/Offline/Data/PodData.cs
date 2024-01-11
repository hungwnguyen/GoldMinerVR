using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace yuki
{
    [CreateAssetMenu(fileName = "PodData", menuName = "Data/Pod")]
    public class PodData : ActorData
    {
        public float angleMax = 80f;
        public float rotationSpeed = 2f;
        public float strength = 10f;
    }
}

