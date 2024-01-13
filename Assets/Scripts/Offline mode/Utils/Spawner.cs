﻿using System;
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
            foreach(RodGenerate rod in _rods)
            {
                int randNumberOfRodGenerate = UnityEngine.Random.Range(rod.min, rod.max);
                int count = 0;
                int i = 0;
                while(count < randNumberOfRodGenerate)
                {
                    i++;
                    float minPos = Screen.Instance.GameplayRect.yMin;
                    float maxPos = Screen.Instance.GameplayRect.yMax;

                    GetOrderPos(ref minPos, ref maxPos, rod);

                    Vector2 randomPos = new Vector2(UnityEngine.Random.Range(Screen.Instance.GameplayRect.xMin, Screen.Instance.GameplayRect.xMax), UnityEngine.Random.Range(minPos, maxPos));
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
                if(boundMin.x > Screen.Instance.GameplayRect.xMin && boundMin.y < Screen.Instance.GameplayRect.yMax && boundMax.x < Screen.Instance.GameplayRect.xMax && boundMax.y > Screen.Instance.GameplayRect.yMin)
                {
                    return true;
                }
            }

            return false;
        }

        private void GetOrderPos(ref float minPos, ref float maxPos, RodGenerate rod)
        {
            switch (rod.order)
            {
                case Order.ENTIRE:
                    minPos = Screen.Instance.GameplayRect.yMin;
                    maxPos = Screen.Instance.GameplayRect.yMax;
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
                    minPos = Screen.Instance.PartTwoRect.yMin;
                    maxPos = Screen.Instance.PartOneRect.yMax;
                    break;
                case Order.PART_TWO_THREE:
                    minPos = Screen.Instance.PartThreeRect.yMin;
                    maxPos = Screen.Instance.PartTwoRect.yMax;
                    break;
            }
        }

    }
}