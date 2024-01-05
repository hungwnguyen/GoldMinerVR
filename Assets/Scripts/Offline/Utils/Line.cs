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
        private float _height; public float Height { get => _height; set => _height = value; }
        private float _width; public float Width { get => _width; set => _width = value; }

        [SerializeField] private Transform _endPoint;
        public LineRenderer LineRenderer { get; private set; }

        void Awake()
        {
            LineRenderer = GetComponent<LineRenderer>();
            _height = Camera.main.orthographicSize * 2;
            _width = _height * Camera.main.aspect;
        }

        void Start()
        {
            LineRenderer.SetPosition(0, new Vector3(0, Height / 2, 0));
            LineRenderer.SetPosition(1, _endPoint.position);
        }

        void Update()
        {
            LineRenderer.SetPosition(1, _endPoint.position);
        }
    }
}
