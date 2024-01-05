using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class LevelManager : MonoBehaviour
    {

        void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}
