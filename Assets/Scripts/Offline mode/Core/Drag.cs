using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class Drag : MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        private bool _isDraged; public bool IsDraged { get => _isDraged; set => _isDraged = value; }
        private float _slowDown; public float SlowDown { get => _slowDown; set => _slowDown = value; }
        private float _valueEarn; public float ValueEarn { get => _valueEarn; set => _valueEarn = value; }
        private Transform _rod;
        void Start()
        {
            _isDraged = false;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(_isDraged)
            {
                return;
            }
            if (other.TryGetComponent(out IDragable dragable))
            {
                _isDraged = true;
                _rod = other.transform;
                this.GetComponent<Animator>().SetBool("drag", true);
                dragable.Draged(this, _transform);
            }
        }

        public void FinishDrag()
        {
            if (_rod.gameObject.TryGetComponent(out RandomBag randomBag))
            {
                randomBag.GetEffectRandomBag();
            }
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<Animator>().SetBool("drag", false);
            Player.Instance.Score += ValueEarn;
            _slowDown = 0;
            _valueEarn = 0;
            _rod.GetComponent<Rod>().Destroy();
        }

        public void UseTNT()
        {
            if (_rod.gameObject.TryGetComponent(out Rod rod))
            {
                this.GetComponent<Animator>().SetBool("drag", false);
                rod.DestroyByTNT();
            }
        }
    }
}
