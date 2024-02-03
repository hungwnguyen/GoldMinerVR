using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private Transform _target;
        public LineRenderer LineRenderer { get; private set; }

        void Awake()
        {
            LineRenderer = GetComponent<LineRenderer>();
        }

        void Update()
        {
		    LineRenderer.SetPosition(0, _origin.position);
            LineRenderer.SetPosition(1, _target.position);
        }
    }
}
