using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class PopupText : MonoBehaviour
    {
        private float _timeExist = 3;

        void Awake()
        {
            _timeExist = 3;
        }

        void Start()
        {
            Destroy(gameObject, _timeExist);
        }
    }
}
