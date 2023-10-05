using UnityEngine;
using DatabaseAPI.Account;

public class SoundManager : MonoBehaviour
{
    #region variable
    private static SoundManager _instance;

    public static SoundManager Instance { get => _instance; set { _instance = value; } }

    [SerializeField] public AudioClipData audioClip;
    [SerializeField] private AudioSource BGMusic;
    [SerializeField] private AudioSource FXSound;

    public float fx, bg;

    private GameObject BackGroundMusic;
    #endregion

    private void Awake()
    {
        if (SoundManager._instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            SoundManager._instance = this;
        }
        Application.targetFrameRate = 60;
    }

    #region Create GameObject Music
    public static void CreatePlayBGMusic(AudioClip aClip)
    {
        SoundManager.CreatePlayBGMusic(aClip, _instance.bg, "BGMusic");
    }

    public static void CreatePlayFXSound(AudioClip aClip)
    {
        if (_instance.fx > 0)
            SoundManager.CreatePlayFXSound(aClip, _instance.fx, "FXSound");
    }

    private static void CreatePlayBGMusic(AudioClip aClip, float vol, string objName)
    {
        _instance.BackGroundMusic = new GameObject(objName);
        _instance.BackGroundMusic.tag = "destroy";
        _instance.BackGroundMusic.transform.position = Vector3.zero;
        _instance.BackGroundMusic.AddComponent<AudioSource>();
        AudioSource component = _instance.BackGroundMusic.GetComponent<AudioSource>();
        CopyProperties(Instance.BGMusic, component, aClip, vol);
        if(_instance.bg > 0) 
            component.Play();
        DontDestroyOnLoad(_instance.BackGroundMusic);
    }

    private static void CreatePlayFXSound(AudioClip aClip, float vol, string objName)
    {
        GameObject m_currentAudioFXSound = new GameObject(objName);
        m_currentAudioFXSound.transform.position = Vector3.zero;
        m_currentAudioFXSound.AddComponent<AudioSource>();
        AudioSource component = m_currentAudioFXSound.GetComponent<AudioSource>();
        CopyProperties(_instance.FXSound, component, aClip, vol);
        component.Play();
        UnityEngine.Object.Destroy(m_currentAudioFXSound, aClip.length);
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
    public static void DisableBGMusic()
    {
        _instance.bg = 0;
        _instance.BackGroundMusic.GetComponent<AudioSource>().Stop();
        AccountController.controller.SetStat("BGMusic", int.Parse(_instance.bg * 100 + ""));
    }

    public static void DisableFXSound()
    {
        _instance.fx = 0;
        AccountController.controller.SetStat("FXSound", int.Parse(_instance.fx * 100 + ""));
    }

    public static void EnableBGMusic()
    {
        _instance.bg = 1;
        _instance.BackGroundMusic.GetComponent<AudioSource>().Play();
        AccountController.controller.SetStat("BGMusic", int.Parse(_instance.bg * 100 + ""));
    }

    public static void EnableFXSound()
    {
        _instance.fx = 1;
        AccountController.controller.SetStat("FXSound", int.Parse(_instance.fx * 100 + ""));
    }

    public static void ChangeVolumeBGMusic(float value)
    {
        _instance.bg = value;
        try
        {
            if (!_instance.BackGroundMusic.GetComponent<AudioSource>().isPlaying)
            {
                _instance.BackGroundMusic.GetComponent<AudioSource>().Play();
            }
            _instance.BackGroundMusic.GetComponent<AudioSource>().volume = value;
        }
        catch { }
        
    }

    public static void ChangeVolumeFXSound(float value)
    {
        _instance.fx = value;
    }
    #endregion
    

}
