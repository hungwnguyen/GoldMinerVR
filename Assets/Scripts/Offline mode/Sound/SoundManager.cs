﻿using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region variable
    private static SoundManager _instance;
    private ObjectPooler<AudioClip> _pool;
    public static SoundManager Instance { get => _instance; set { _instance = value; } }

    [SerializeField] public AudioClipData audioClip;
    [SerializeField] private AudioSource BGMusic;
    [SerializeField] private AudioSource FXSound;

    public float fx, bg;

    private Dictionary<string, AudioSource> FXLoop;
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
        }
        _pool = new ObjectPooler<AudioClip>();
        FXLoop = new Dictionary<string, AudioSource>();
        Instance._pool.OnSpawned += Instance.CustomSpawnHandler;
        fx = PlayerPrefs.GetFloat("fx", 1);
        bg = PlayerPrefs.GetFloat("bg", 0.4f);
        //CreatePlayBGMusic(audioClip.BGMusic);
    }

    #region Create GameObject Music
    
    public static void CreatePlayFXSound(AudioClip aClip)
    {
        if (_instance.fx > 0)
            Instance._pool.SpawnFromPool(aClip);
    }

    public static void CreatePlayFXLoop(AudioClip aClip)
    {
        if (!Instance.FXLoop.ContainsKey(aClip.name)){
            GameObject obj = new GameObject(aClip.name);
            obj.transform.position = Vector3.zero;
            obj.AddComponent<AudioSource>();
            Instance.FXLoop.Add(aClip.name, obj.GetComponent<AudioSource>());
            CopyProperties(Instance.BGMusic, Instance.FXLoop[aClip.name], aClip, Instance.fx);
            Instance.PlayFXLoop(aClip);
            DontDestroyOnLoad(obj);
        }
        else{
            Instance.PlayFXLoop(aClip);
        }
    }

    private void PlayFXLoop(AudioClip aClip){
        if (bg > 0){
            Instance.FXLoop[aClip.name].volume = bg;
            Instance.FXLoop[aClip.name].Play();
        }
    }

    public void StopFXLoop(AudioClip aClip){
        if (this.FXLoop != null){
            if (this.FXLoop.ContainsKey(aClip.name))
                this.FXLoop[aClip.name].Stop();
        }
    }

    public GameObject CustomSpawnHandler(AudioClip aClip){
        GameObject m_currentAudioFXSound = new GameObject(aClip.name);
        m_currentAudioFXSound.AddComponent<AudioSource>();
        m_currentAudioFXSound.AddComponent<Sound>();
        AudioSource component = m_currentAudioFXSound.GetComponent<AudioSource>();
        CopyProperties(_instance.FXSound, component, aClip, Instance.fx);
        DontDestroyOnLoad(m_currentAudioFXSound);
        return m_currentAudioFXSound;
    }

    static void CopyProperties(AudioSource sourceToCopyFrom, AudioSource newSource, AudioClip aClip, float vol)
    {
        // Copy all the properties from the sourceToCopyFrom AudioSource to the new AudioSource
        newSource.clip = aClip;
        newSource.volume = vol;
        newSource.pitch = sourceToCopyFrom.pitch;
        newSource.panStereo = sourceToCopyFrom.panStereo;
        newSource.spatialBlend = sourceToCopyFrom.spatialBlend;
        newSource.reverbZoneMix = sourceToCopyFrom.reverbZoneMix;
        newSource.dopplerLevel = sourceToCopyFrom.dopplerLevel;
        newSource.spread = sourceToCopyFrom.spread;
        newSource.minDistance = sourceToCopyFrom.minDistance;
        newSource.maxDistance = sourceToCopyFrom.maxDistance;
        newSource.playOnAwake = sourceToCopyFrom.playOnAwake;
        newSource.loop = sourceToCopyFrom.loop;
        newSource.rolloffMode = sourceToCopyFrom.rolloffMode;
        newSource.outputAudioMixerGroup = sourceToCopyFrom.outputAudioMixerGroup;

        // Copy the custom rolloff curve if applicable
        if (newSource.rolloffMode == AudioRolloffMode.Custom)
        {
            AnimationCurve volumeRolloffCurve = sourceToCopyFrom.GetCustomCurve(AudioSourceCurveType.CustomRolloff);
            newSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, volumeRolloffCurve);
        }
    }
#endregion

    #region Sound Setting

    public static void PauseAllMusic(){
        foreach(AudioSource audioSource in _instance.FXLoop.Values){
            if (audioSource.gameObject.activeSelf){
                audioSource.Pause();
            }
        }
        foreach(GameObject go in _instance._pool.poolDictionary.Values){
            if (go.activeSelf){
                go.GetComponent<AudioSource>().Pause();
            }
        }
    }

    public static void ContinuePlayAllMusic(){
        foreach(AudioSource audioSource in _instance.FXLoop.Values){
            if (audioSource.gameObject.activeSelf){
                audioSource.Play();
            }
        }
        foreach(GameObject go in _instance._pool.poolDictionary.Values){
            if (go.activeSelf){
                go.GetComponent<AudioSource>().Play();
            }
        }
    }

    public static void DisableBGMusic()
    {
        _instance.bg = 0;
        foreach(AudioSource audioSource in _instance.FXLoop.Values){
            if (audioSource.gameObject.activeSelf){
                audioSource.Stop();
            }
        }
    }

    public static void DisableFXSound()
    {
        _instance.fx = 0;
    }

    public static void EnableBGMusic(string name)
    {
        _instance.bg = 1;
        if (Instance.FXLoop.ContainsKey(name)){
            Instance.FXLoop[name].Play();
        }
    }

    public static void EnableFXSound()
    {
        _instance.fx = 1;
    }

    public static void ChangeVolumeBGMusic(float value)
    {
        _instance.bg = value;
        foreach(AudioSource audioSource in _instance.FXLoop.Values){
            audioSource.volume = value;
        }
        
    }

    public static void ChangeVolumeFXSound(float value)
    {
        _instance.fx = value;
    }
    #endregion
    

}