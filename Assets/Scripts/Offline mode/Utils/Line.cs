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
        [SerializeField] private int _pointsCount = 10;

        void Awake()
        {
            LineRenderer = GetComponent<LineRenderer>();
            LineRenderer.positionCount = _pointsCount + 2;
            LineRenderer.useWorldSpace = true;
            UpdateLinePositions();
        }

        void Update()
        {
            if (_origin.hasChanged || _target.hasChanged)
            {
                UpdateLinePositions();

                _origin.hasChanged = false;
                _target.hasChanged = false;
            }
        }

        void UpdateLinePositions()
        {
            LineRenderer.SetPosition(0, _origin.position);

            for (int i = 1; i <= _pointsCount; i++)
            {
                float t = i / (float)(_pointsCount + 1);
                Vector3 pointPosition = Vector3.Lerp(_origin.position, _target.position, t);
                LineRenderer.SetPosition(i, pointPosition);
            }

            LineRenderer.SetPosition(_pointsCount + 1, _target.position);
        }
    }
}
