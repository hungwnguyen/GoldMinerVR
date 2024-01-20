using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Drawing;

namespace yuki
{
    public class Line1 : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private Transform _target;
        [SerializeField] private float _lineWidth;

        private void Update()
        {
            var draw = Draw.ingame;
            using (draw.WithLineWidth(_lineWidth))
                draw.Line(_origin.position, _target.position, Color.black);

        }
    }
}
