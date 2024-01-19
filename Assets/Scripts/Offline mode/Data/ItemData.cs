using UnityEngine;
using yuki;

[CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    [Space(1f), Header("Min of price"), Space(1f)]
    public int minValue;
    [Space(1f), Header("Max of price"), Space(1f)]
    public int maxValue;
    [Space(1f), Header("Effect description"), TextArea(100,10000), Space(1f)]
    public string description;
    [Space(1f), Header("Type"), Space(1f)]
    public Item type;
}
