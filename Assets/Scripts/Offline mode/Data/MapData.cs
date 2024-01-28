
using System;
using UnityEngine;
using yuki;

[Serializable]
public class RodGenerateData {
    public RodGenerateData(Order order, int min, int max, string name){
        this.order = order;
        this.min = min;
        this.max = max;
        this.name = name;
    }
    public Order order;
    [Range(0, 50)]
    public int min;
    [Range(0, 100)]
    public int max;
    public string name;
}

[CreateAssetMenu(fileName = "MapDataSO", menuName = "Data/MapDataSO")]
public class MapData : ScriptableObject
{
    public RodGenerateData [] _rods;
    [Range(0, 1)]
    public float _sizeCheck;
}
