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
        [SerializeField] private LayerMask whatIsDragable;
        [SerializeField] private Transform dragPosition;
        //[SerializeField] private Vector2 dragSize;
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
            if (other.TryGetComponent(out IDragable dragable))
            {
                _isDraged = true;
                _rod = other.transform;
                dragable.Draged(this, _transform);
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        //public void Dragging()
        //{
        //    if (_isDraged) return;
        //    Collider2D collider = Physics2D.OverlapCapsule(dragPosition.position, dragSize, CapsuleDirection2D.Vertical, whatIsDragable);

        //    if (collider.TryGetComponent(out IDragable dragable))
        //    {
        //        _isDraged = true;
        //        _rod = collider.transform;
        //        dragable.Draged(this, _transform);
        //    }
        //}

        public void FinishDrag()
        {
            if(_rod.gameObject.TryGetComponent(out Mouse mouse))
            {
                Destroy(mouse.Parent.gameObject);
            }
            else if(_rod.gameObject.TryGetComponent(out RandomBag randomBag))
            {
                randomBag.GetEffectRandomBag();
            }
            this.GetComponent<BoxCollider2D>().enabled = true;
            Player.Instance.Score += ValueEarn;
            _slowDown = 0;
            _valueEarn = 0;
            Destroy(_rod.gameObject);
        }

        public void UseTNT()
        {
            if(_rod.gameObject.TryGetComponent(out Rod rod))
            {
                if (!rod.IsDestroy)
                {
                    rod.IsDestroy = true;
                }
                else
                {
                    if (rod.gameObject.TryGetComponent(out Mouse mouse))
                    {
                        Destroy(mouse.Parent.gameObject);
                    }
                    Destroy(rod.gameObject);
                }
            }
        }
#if UNITY_EDITOR
        //void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(dragPosition.position, dragSize.x / 2);
        //}
    }
#endif
}
