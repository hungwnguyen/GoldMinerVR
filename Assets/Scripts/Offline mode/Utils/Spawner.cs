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
        public string name;
        public GameObject prefab;
        public Order order;
        public int min;
        public int max;
    }

    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<RodGenerate> _rods = new List<RodGenerate>();
        [SerializeField] private int _numberOfRodGenerate;
        [SerializeField] private Vector2 _sizeCheckMutiplier;

        public static Spawner Instance;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
                Instance = this;
        }

        void Start ()
        {
            if (LevelManager.Instance.Level == 1)
            {
                SortRodByValue();
                SpawnRod();
            }
        }

        void Update()
        {
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                LevelManager.Instance.DestroyAllRod();
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
                    float minPos = Screen.Instance.PartOneRect.yMin;
                    float maxPos = Screen.Instance.PartThreeRect.yMax;

                    GetOrderPos(ref minPos, ref maxPos, rod);

                    Vector2 randomPos = new Vector2(UnityEngine.Random.Range(Screen.Instance.PartOneRect.xMin, Screen.Instance.PartOneRect.xMax), UnityEngine.Random.Range(minPos, maxPos));
                    if (CheckCollision(randomPos, rod.prefab))
                    {
                        SpawnRandomRod(rod.prefab, randomPos);
                        count++;
                    }
                    if (i == 2000)
                        break;
                }

            }

        }

        

        private void SpawnRandomRod(GameObject go, Vector2 position)
        {
            GameObject prefabIns = Instantiate(go, position, Quaternion.identity);
            if (IsColliderOutsideScreen(prefabIns))
            {
                Destroy(prefabIns);
            }
            else
            {
                Rod rod = prefabIns.GetComponentInChildren<Rod>(true);
                if (rod != null)
                {
                    rod.CurrentPosition = position;
                }
            }
        }

        private bool CheckCollision(Vector2 pos, GameObject rod)
        {
            BoxCollider2D[] collider2Ds = rod.GetComponentsInChildren<BoxCollider2D>(true);
            BoxCollider2D rodBox = collider2Ds[0];
            Collider2D[] hits = Physics2D.OverlapBoxAll(pos, new Vector2(rodBox.size.x * _sizeCheckMutiplier.x, rodBox.size.y * _sizeCheckMutiplier.y), 0);
            return hits.Length < 1;
        }

        private bool IsColliderOutsideScreen(GameObject rod)
        {
            BoxCollider2D[] collider2Ds = rod.GetComponentsInChildren<BoxCollider2D>(true);
            BoxCollider2D rodBox = collider2Ds[0];

            Bounds colBound = rodBox.bounds;
            Vector2 boundMin = colBound.min;
            Vector2 boundMax = colBound.max;
            if (boundMin.x > Screen.Instance.PartOneRect.xMin && boundMin.y > Screen.Instance.PartOneRect.yMin && boundMax.x < Screen.Instance.PartThreeRect.xMax && boundMax.y < Screen.Instance.PartThreeRect.yMax)
            {
                return false;
            }

            return true;
        }

        private void GetOrderPos(ref float minPos, ref float maxPos, RodGenerate rod)
        {
            switch (rod.order)
            {
                case Order.ENTIRE:
                    minPos = Screen.Instance.PartOneRect.yMin;
                    maxPos = Screen.Instance.PartThreeRect.yMax;
                    break;
                case Order.PART_ONE:
                    minPos = Screen.Instance.PartOneRect.yMin;
                    maxPos = Screen.Instance.PartOneRect.yMax;
                    break;
                case Order.PART_TWO:
                    minPos = Screen.Instance.PartTwoRect.yMin;
                    maxPos = Screen.Instance.PartTwoRect.yMax;
                    break;
                case Order.PART_THREE:
                    minPos = Screen.Instance.PartThreeRect.yMin;
                    maxPos = Screen.Instance.PartThreeRect.yMax;
                    break;
                case Order.PART_ONE_TWO:
                    minPos = Screen.Instance.PartOneRect.yMin;
                    maxPos = Screen.Instance.PartTwoRect.yMax;
                    break;
                case Order.PART_TWO_THREE:
                    minPos = Screen.Instance.PartTwoRect.yMin;
                    maxPos = Screen.Instance.PartThreeRect.yMax;
                    break;
            }
        }

        private void SortRodByValue()
        {
            _rods.Sort((rod1, rod2) =>
            {
                Rod r1 = rod1.prefab.GetComponentInChildren<Rod>(true);
                Rod r2 = rod2.prefab.GetComponentInChildren<Rod>(true);
                return r2.Value.CompareTo(r1.Value);
            });
        }
    }
}
