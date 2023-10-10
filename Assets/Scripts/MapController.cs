using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Serializable]
    public class Row
    {
        public GameObject[] Column;
    }

    [Space(2f), Header("Prefab Target list"), Space(2f)]
    [SerializeField]
    private List<GameObject> targetPrefab;
    [Space(2f), Header("Map game row outside and column inside"), Space(2f)]
    [SerializeField]
    private Row[] map;

    public void CreateMap(int row, int column, int id)
    {
        try
        {
            Instantiate(targetPrefab[id - 1], map[row - 1].Column[column].transform);
        } catch { }
    }
}
