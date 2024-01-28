using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace yuki
{
    [Serializable]
    public class RodGenerate {
        public string name;
        public GameObject prefab;
        public Order order;
        [Range(0, 20)]
        public int min;
        [Range(0, 100)]
        public int max;
    }

    public class Spawner : MonoBehaviour
    {
        public bool isEdit;
        public bool isClick = false;
        [SerializeField] private List<RodGenerate> _rods = new List<RodGenerate>();
        [Range(0, 1f)]
        [SerializeField] private float _sizeCheckMutiplier;
        private string resourceFolder = "MapDataSO/MapDataSO ";
        private List<GameObject> gos;
        [Range(1, 100)]
        public int currentLevel = 1;
        public List<MapData> loadedMapAssets;
    
        public static Spawner Instance;

        #if UNITY_EDITOR

        public void ClickMe(){
            if (isClick){
                isClick = false;
                //WriteToScriptableObject();
            }
        }

        void WriteToScriptableObject(){
            loadedMapAssets[currentLevel - 1]._rods = new RodGenerateData[_rods.Count];
            for(int i = 0; i < _rods.Count; i++){
                loadedMapAssets[currentLevel - 1]._rods[i] = new RodGenerateData(
                    _rods[i].order, _rods[i].min, _rods[i].max, _rods[i].name
                );
            }
            loadedMapAssets[currentLevel - 1]._sizeCheck = _sizeCheckMutiplier;
            currentLevel++;
        }

        
        #endif

        #region MonoBehavior Callback

        void OnEnable()
        {
            ReadResourcesToScriptableObject();
        }

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
            SpawnRodLevel(1);
        }
        #endregion

        #if UNITY_EDITOR
        void Update()
        {
            if (isEdit){
                ClickMe();
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    DestroyAllRod();
                    SpawnRod();
                }
                if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    DestroyAllRod();
                    SpawnRodLevel(currentLevel);
                }
            }
        }
        #endif

        void ReadResourcesToScriptableObject()
        {
            loadedMapAssets = new List<MapData>();
            for (int i = 1; i <= 10; i++)
            {
                // Load tài nguyên từ tên tệp
                MapData data = Resources.Load<MapData>(resourceFolder + i);

                // Kiểm tra xem tài nguyên được load có hợp lệ không
                if (data != null)
                {
                    // Thêm tài nguyên vào danh sách
                    loadedMapAssets.Add(data);
                }
            }
        }

        IEnumerator Freeze(){
            yield return new WaitForSeconds(2);
            foreach(GameObject go in gos.ToList()){
                try {
                    go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | 
                    RigidbodyConstraints2D.FreezePosition;
                } catch {
                    gos.Remove(go);
                }
            }
        }

        public void CalcualateTargetScore(){
            gos.Sort((go1, go2) =>
            {
                return go2.GetComponentInChildren<Rod>(true).Value
                .CompareTo(go1.GetComponentInChildren<Rod>(true).Value);
            });
            int targetScore = 0;
            int size = gos.Count / 3 < 10 ? gos.Count / 3 : UnityEngine.Random.Range(8, 12);
            for (int i = 0; i < size; i++){
                targetScore += (int) gos[i].GetComponentInChildren<Rod>(true).Value;
            }
            GameManager.Instance.TargetScore += targetScore;
            UIPopup.Instance.SetTargetSocre(GameManager.Instance.TargetScore + "$");
        }
    
        public void SpawnRod()
        {
            SortRodByValue();
            foreach(RodGenerate rod in _rods)
            {
                StartCoroutine(Spaw(rod));
            }
            CalcualateTargetScore();
            StartCoroutine(Freeze());
        }

        public void SpawnRodLevel(int level){
            level = (level - 1) % 10;
            foreach(RodGenerate rod in _rods)
            {
                foreach(RodGenerateData data in loadedMapAssets[level]._rods.ToList()){
                    if (data.name.Equals(rod.name)){
                        rod.order = data.order;
                        rod.min = data.min;
                        rod.max = data.max;
                        break;
                    }
                }
            }
            this._sizeCheckMutiplier = loadedMapAssets[level]._sizeCheck;
            SpawnRod();
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
                if (i == 666)
                    yield break;
            }
        }

        public void DestroyAllRod()
        {
            foreach (GameObject go in gos)
            {
                Destroy(go);
            }
            GameObject [] mouses = GameObject.FindGameObjectsWithTag("Rod");
            foreach(GameObject mouse in mouses){
                DestroyImmediate(mouse);
            }
            gos.Clear();
        }

        private void SpawnRandomRod(GameObject go, Vector2 position)
        {
            GameObject prefabIns = Instantiate(go, position, Quaternion.identity);
            gos.Add(prefabIns);
            CheckColliderOutsideScreen(prefabIns);
        }

        private bool CheckCollision(Vector2 pos, GameObject rod)
        {
            BoxCollider2D[] collider2Ds = rod.GetComponentsInChildren<BoxCollider2D>(true);
            BoxCollider2D rodBox = collider2Ds[0];
            Collider2D[] hits = new Collider2D[2];
            int count = Physics2D.OverlapBoxNonAlloc(pos, new Vector2(rodBox.size.x * _sizeCheckMutiplier, rodBox.size.y * _sizeCheckMutiplier), 0, hits);
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
