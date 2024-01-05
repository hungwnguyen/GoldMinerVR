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

        public Dictionary<RandomBagItem, String> RandomBagDict { get; private set; }
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
                    Debug.Log("Drag");
                    _isDraged = true;
                    _rod = collider.transform;
                    dragable.Draged(this);
                    break;
                }
                
            }
        }

        public void DestroyRod()
        {
            Destroy(_rod.gameObject);
        }

        public void DestroyRodByTNT()
        {
            if(_rod.gameObject.TryGetComponent(out Rod rod))
            {
                if (!rod.IsDestroy)
                {
                    rod.IsDestroy = true;
                }
            }
        }

        public string GetRandomBagItem()
        {
            if(_rod.gameObject.TryGetComponent(out RandomBag randomBag))
            {
                RandomBagItem item = randomBag.RandomItem();
                RandomBagDict = randomBag.CreateDictStr();
                switch (item)
                {
                    case RandomBagItem.TNT_1:
                        PlayerManager.Instance.Items.Add(Item.TNT);
                        return RandomBagDict[RandomBagItem.TNT_1];
                    case RandomBagItem.TNT_2:
                        PlayerManager.Instance.Items.Add(Item.TNT);
                        PlayerManager.Instance.Items.Add(Item.TNT);
                        return RandomBagDict[RandomBagItem.TNT_2];
                    case RandomBagItem.TNT_3:
                        PlayerManager.Instance.Items.Add(Item.TNT);
                        PlayerManager.Instance.Items.Add(Item.TNT);
                        PlayerManager.Instance.Items.Add(Item.TNT);
                        return RandomBagDict[RandomBagItem.TNT_3];
                    case RandomBagItem.STRENGTH_UP:
                        return RandomBagDict[RandomBagItem.STRENGTH_UP];
                    case RandomBagItem.NONE:
                        return RandomBagDict[RandomBagItem.NONE];
                }
            }
            return "";
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(dragPosition.position, dragSize.x / 2);
        }
    }
}
