using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class Explode : MonoBehaviour
    {
        [SerializeField] private Transform _explodePoint;
        [SerializeField] private Vector2 _explodeSize;
        [SerializeField] private LayerMask _whatIsCanExplode;

        public void Exploding()
        {
            Collider2D[] colliders = Physics2D.OverlapCapsuleAll(_explodePoint.position, _explodeSize, 0, _whatIsCanExplode);

            foreach(Collider2D collider in colliders)
            {
                if(collider.TryGetComponent(out Rod rod))
                {
                    if (!rod.IsDestroy)
                    {
                        rod.IsDestroy = true;
                        if (collider.TryGetComponent(out IExplodeable exploreable))
                        {
                            exploreable.Exploded();
                        }
                    }
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_explodePoint.position, _explodeSize.x / 2);
        }
    }
}
