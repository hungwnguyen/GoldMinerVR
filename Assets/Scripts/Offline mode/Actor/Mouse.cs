using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace yuki
{
    public class Mouse : Rod
    {
        [SerializeField] private Transform[] _patrolPoints;
        private int _patrolDestination;
        public int PatrolDestination
        {
            get => _patrolDestination >= 2 ? 0 : _patrolDestination;
            set => _patrolDestination = value;
        }
        
        protected override void Start()
        {
            base.Start();

            _patrolDestination = 0;
            
        }

        public void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[PatrolDestination].position, rodData.speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, _patrolPoints[PatrolDestination].position) < 0.2f)
            {
                PatrolDestination++;
                Flip();
            }

        }

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
        }
    }
}
