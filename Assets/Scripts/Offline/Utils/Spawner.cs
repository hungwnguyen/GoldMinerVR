using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace yuki
{
    [Serializable]
    public class RodGenerate {
        public RodType type;
        public GameObject prefab;
        public int min;
        public int max;
    }

    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<RodGenerate> _rods = new List<RodGenerate>();
        [SerializeField] private int _numberOfRodGenerate;
        [SerializeField] private Vector2 _offsetScreen;

        private float _height;
        private float _width;

        private float _top;
        private float _bottom;
        private float _left;
        private float _right;

        private System.Random _random = new System.Random();
        private float _sumWeight;
        public static Spawner Instance;

        void Awake()
        {
            Instance = this;
        }

        void Start ()
        {
            Initialization();
            if (LevelManager.Instance.Level == 1)
            {
                SpawnRod();
            }
        }

        void Update()
        {
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                DestroyAllRod();
                SpawnRod();
            }
        }

        public void SpawnRod()
        {
            foreach(RodGenerate rod in _rods)
            {
                int randNumberOfRodGenerate = UnityEngine.Random.Range(rod.min, rod.max);
                int count = 0;
                int i = 0;
                while(count < randNumberOfRodGenerate)
                {
                    i++;
                    Vector2 randomPos = new Vector2(UnityEngine.Random.Range(_left, _right), UnityEngine.Random.Range(_bottom, _top));
                    if (CheckCollision(randomPos))
                    {
                        SpawnRandomRod(rod.prefab, randomPos);
                        count++;
                    }
                    if (i == 2000)
                        break;
                }
                
            }
            
        }

        public void DestroyAllRod()
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Rod");
            foreach (GameObject go in gos)
            {
                Destroy(go);
            }
        }

        private void SpawnRandomRod(GameObject go, Vector2 position)
        {
            GameObject prefabIns = Instantiate(go, position, Quaternion.identity);
            if (!CheckColliderOutsideScreen(prefabIns))
            {
                Destroy(prefabIns);
            }
        }

        //private int GetRandomRodIndex()
        //{
        //    double r = _random.NextDouble() * _sumWeight;
        //    for(int i=0; i<_rods.Count; i++)
        //    {
        //        if (_rods[i].weight >= r)
        //        {
        //            return i;
        //        }
        //    }
        //    return 0;
        //}

        private void Initialization()
        {
            _height = Camera.main.orthographicSize * 2;
            _width = _height * Camera.main.aspect;
            _top = _height / 2 - 2.5f - _offsetScreen.y;
            _bottom = -_height / 2 + _offsetScreen.y;
            _left = -_width / 2 + _offsetScreen.x;
            _right = _width / 2 - _offsetScreen.x;
            _sumWeight = 0f;
            //foreach(RodGenerate rod in _rods)
            //{
            //    _sumWeight += rod.chance;
            //    rod.weight = _sumWeight;
            //}
        }

        private bool CheckCollision(Vector2 pos)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(pos, 1.5f);
            return hits.Length < 1;
        }

        private bool CheckColliderOutsideScreen(GameObject obj)
        {
            Collider2D[] cols = obj.GetComponentsInParent<Collider2D>(true);
            Collider2D col = cols[cols.Length - 1];
            

            if (col != null)
            {
                Bounds colBound = col.bounds;
                Vector2 boundMin = colBound.min;
                Vector2 boundMax = colBound.max;
                if(boundMin.x > _left && boundMin.y < _top && boundMax.x < _right && boundMax.y > _bottom)
                {
                    return true;
                }
            }

            return false;
        }

        public void GetRandomChane()
        {
            
        }
    } 
}
