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
        private bool isReady;
        private LineRenderer lineRenderer;

        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            isReady = false;
        }

        void Update()
        {
            if (isReady){
                lineRenderer.SetPosition(0, _origin.position);
                lineRenderer.SetPosition(1, _target.position);
            }   
        }

        public void ChangeStatus(bool value = true){
            isReady = value;
        }
    }
}
