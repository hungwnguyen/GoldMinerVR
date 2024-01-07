using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class TryGetComp<T>
    {

        public static T GetComp(GameObject obj) 
        { 
            T comp = obj.GetComponent<T>();
            if(comp == null)
            {
                comp = obj.GetComponentInChildren<T>();
            }
            else
            {
                Debug.Log($"Object {obj.name} doesn't have comp");
            }
            return comp;
        }
    }
}
