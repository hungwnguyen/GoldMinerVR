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
        [SerializeField] private Transform _target;
        [SerializeField] private Vector2 _offset;
        public LineRenderer LineRenderer { get; private set; }

        void Awake()
        {
            LineRenderer = GetComponent<LineRenderer>();
        }

        void Start()
        {
            LineRenderer.positionCount = 2;
            LineRenderer.SetPosition(0, Screen.Instance.PlayerRect.center + _offset);
        }

        void Update()
        {
            
            LineRenderer.SetPosition(1, _target.position);
        }
    }
}
