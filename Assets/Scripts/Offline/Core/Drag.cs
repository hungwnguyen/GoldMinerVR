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
        [SerializeField] private Vector2 dragSize;
        private bool _isDraged; public bool IsDraged { get => _isDraged; set => _isDraged = value; }
        private float _slowDown; public float SlowDown { get => _slowDown; set => _slowDown = value; }
        private float _valueEarn; public float ValueEarn { get => _valueEarn; set => _valueEarn = value; }
        private Transform _rod;
        void Start()
        {
            _isDraged = false;
        }

        public void Dragging()
        {
            if (_isDraged) return;
            Collider2D[] colliders = Physics2D.OverlapCapsuleAll(dragPosition.position, dragSize, 0, whatIsDragable);

            foreach(Collider2D collider in colliders)
            {
                if(collider.TryGetComponent(out IDragable dragable))
                {
                    _isDraged = true;
                    _rod = collider.transform;
                    dragable.Draged(this);
                    break;
                }
                
            }
        }

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

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(dragPosition.position, dragSize.x / 2);
        }
    }
}
