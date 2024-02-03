using UnityEngine;

namespace yuki
{
    public class Mouse : Rod
    {
        [SerializeField] private Transform[] _patrolPoints;
        private int _patrolDestination;
        bool isFlip;
        
        protected override void Start()
        {
            base.Start();
            _patrolDestination = 1;
            Rotate();
            isFlip = false;
            float size = Vector2.Distance(_patrolPoints[0].position, _patrolPoints[_patrolPoints.Length - 1].position);
            BoxCollider2D rodBox = this.GetComponent<BoxCollider2D>();

            Bounds colBound = rodBox.bounds;
            Vector2 boundMin = colBound.min;
            if (boundMin.x - size < Screen.Instance.PartOneRect.xMin){
                this.transform.parent.position = 
                new Vector3(this.transform.parent.position.x + colBound.size.x / 2 + size, this.transform.parent.position.y, this.transform.parent.position.z);
            }
        }

        public void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[_patrolDestination].position, rodData.speed * Time.deltaTime);
            if(transform.position == _patrolPoints[_patrolDestination].position)
            {
                if (_patrolDestination == _patrolPoints.Length - 1 || _patrolDestination == 0){
                    isFlip = !isFlip;
                }
                if (isFlip){
                    _patrolDestination--;
                } else {
                    _patrolDestination++;
                }
                if (_patrolDestination < _patrolPoints.Length)
                {
                    Rotate();
                }
            }
        }

        private void Rotate(){
            Vector3 direction = (transform.position - _patrolPoints[_patrolDestination].position).normalized;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (isFlip){
                    transform.Rotate(180, 0, 0);
                } 
            }
        }

    }
}