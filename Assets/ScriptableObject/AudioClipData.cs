using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipData", menuName = "Audio Clip Data", order = 1)]
public class AudioClipData : ScriptableObject
{
    [Space(1f), Header("Get gift music"), Space(1f)] 
    public AudioClip aud_congqua;
    [Space(1f), Header("Monney update music"), Space(1f)] 
    public AudioClip aud_congtien;
    [Space(1f), Header("Clock music"), Space(1f)] 
    public AudioClip aud_dongho;
    [Space(1f), Header("Gameover music"), Space(1f)] 
    public AudioClip aud_fail;
    [Space(1f), Header("Get rock music"), Space(1f)] 
    public AudioClip aud_getda;
    [Space(1f), Header("Get diamon music"), Space(1f)] 
    public AudioClip aud_getkimcuong;
    [Space(1f), Header("Get bigest gold music"), Space(1f)] 
    public AudioClip aud_getvanglon;
    [Space(1f), Header("Get smallest gold music"), Space(1f)] 
    public AudioClip aud_getvangnho;
    [Space(1f), Header("Pull the rope music"), Space(1f)] 
    public AudioClip aud_keoday;
    [Space(1f), Header("Bomb music"), Space(1f)] 
    public AudioClip aud_luudan;
    [Space(1f), Header("Background music"), Space(1f)] 
    public AudioClip BGMusic;
    [Space(1f), Header("Target music"), Space(1f)] 
    public AudioClip aud_muctieu;
    [Space(1f), Header("TNT music"), Space(1f)] 
    public AudioClip aud_notnt;
    [Space(1f), Header("Release the ropes music"), Space(1f)] 
    public AudioClip aud_thaday;
    [Space(1f), Header("Button music"), Space(1f)] 
    public AudioClip aud_touch;
    [Space(1f), Header("Win music"), Space(1f)] 
    public AudioClip aud_win;
    [Space(1f), Header("Laughter music"), Space(1f)]
    public AudioClip laughter;
    [Space(1f), Header("Welcom music"), Space(1f)]
    public AudioClip welcom;
}