using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [SerializeField] private Vector2 _sizeCheckMutiplier;

        private List<GameObject> gos;
    
        public static Spawner Instance;

        void Awake()
        {
            gos = new List<GameObject>();
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
                Instance = this;
        }

        void Start ()
        {
            if (GameManager.Instance.Level == 1)
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
            SortRodByValue();
            foreach(RodGenerate rod in _rods)
            {
                StartCoroutine(Spaw(rod));
            }
            CalcualateTargetScore();
        }

        public void CalcualateTargetScore(){
            gos.Sort((go1, go2) =>
            {
                return go2.GetComponentInChildren<Rod>(true).Value
                .CompareTo(go1.GetComponentInChildren<Rod>(true).Value);
            });
            int targetScore = 0;
            for (int i = 0; i < gos.Count / 3; i++){
                targetScore += (int) gos[i].GetComponentInChildren<Rod>(true).Value;
            }
            GameManager.Instance.TargetScore += targetScore;
            UIPopup.Instance.SetTargetSocre(GameManager.Instance.TargetScore + "$");
        }
        
        IEnumerator Spaw(RodGenerate rod){
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
                if (i == 1000)
                    yield break;
            }
        }

        public void DestroyAllRod()
        {
            foreach (GameObject go in gos)
            {
                Destroy(go);
            }
            gos.Clear();
        }

        private void SpawnRandomRod(GameObject go, Vector2 position)
        {
            GameObject prefabIns = Instantiate(go, position, Quaternion.identity);
            CheckColliderOutsideScreen(prefabIns);
        }

        private bool CheckCollision(Vector2 pos, GameObject rod)
        {
            BoxCollider2D[] collider2Ds = rod.GetComponentsInChildren<BoxCollider2D>(true);
            BoxCollider2D rodBox = collider2Ds[0];
            Collider2D[] hits = new Collider2D[2];
            int count = Physics2D.OverlapBoxNonAlloc(pos, new Vector2(rodBox.size.x * _sizeCheckMutiplier.x, rodBox.size.y * _sizeCheckMutiplier.y), 0, hits);
            return count < 1;
        }

        private void CheckColliderOutsideScreen(GameObject rod)
        {
            BoxCollider2D[] collider2Ds = rod.GetComponentsInChildren<BoxCollider2D>(true);
            BoxCollider2D rodBox = collider2Ds[0];

            Bounds colBound = rodBox.bounds;
            Vector2 boundMin = colBound.min;
            Vector2 boundMax = colBound.max;
            if (boundMin.x < Screen.Instance.PartOneRect.xMin){
                rod.transform.position = 
                new Vector3(rod.transform.position.x + colBound.size.x / 2, rod.transform.position.y, rod.transform.position.z);
            }
            if ( boundMin.y < Screen.Instance.PartOneRect.yMin){
                rod.transform.position = 
                new Vector3(rod.transform.position.x, rod.transform.position.y + colBound.size.y / 2, rod.transform.position.z);
            }
            if (boundMax.x > Screen.Instance.PartThreeRect.xMax){
                rod.transform.position = 
                new Vector3(rod.transform.position.x - colBound.size.x / 2, rod.transform.position.y, rod.transform.position.z);
            }
            if (boundMax.y > Screen.Instance.PartThreeRect.yMax){
                rod.transform.position = 
                new Vector3(rod.transform.position.x, rod.transform.position.y - colBound.size.y / 2, rod.transform.position.z);
            }
            gos.Add(rod);
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
                return rod2.min > rod1.min ? 1 : 
                rod2.min == rod1.min ? 0 : -1;
            });
        }
    }
}
