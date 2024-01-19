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

        private Vector3 _bound; public Vector3 Bound { get => _bound; set => _bound = value; }
        private bool _isDraged; public bool IsDraged { get => _isDraged; set => _isDraged = value; }
        private float _slowDown; public float SlowDown { get => _slowDown; set => _slowDown = value; }
        private float _valueEarn; public float ValueEarn { get => _valueEarn; set => _valueEarn = value; }
        private bool _getStrength; public bool GetStrength { get => _getStrength; set => _getStrength = value; }
        public bool GetTNT {get; set;}
        private Transform _rod;
        void Start()
        {
            _isDraged = false;
            _bound = GetComponent<Renderer>().bounds.size;
            GetTNT = false;
            GetStrength = false;
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
                dragable.Draged(this, transform);
            }
        }

        public void FinishDrag()
        {
            if (_rod.gameObject.TryGetComponent(out RandomBag randomBag))
            {
                string randBagItem = randomBag.GetEffectRandomBag();
                if (randBagItem.Equals(RandomBagItem.STRENGTH_UP.ToString()))
                {
                    _getStrength = true;
                    Invoke("StrengthCoundown", 0.8f);
                }
                else if (randBagItem == RandomBagItem.TNT.ToString()){
                    GetTNT = true;
                }
            }
            else
            {
                TextContainer.Instance.ShowFloatingText(_valueEarn + "$");
            }
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<Animator>().SetBool("drag", false);
            Player.Instance.Score += ValueEarn;
            _slowDown = 0;
            _valueEarn = 0;
            _rod.GetComponent<Rod>().Destroy();
        }

        void StrengthCoundown(){
            Player.Instance.RewardFinished = true;
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
