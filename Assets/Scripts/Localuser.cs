using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Localuser : MonoBehaviour
{
    [Serializable]
    public class MyEvent : UnityEvent { }

    [Space(2f), Header("Run only one time when game start"), Space(2f)]
    [FormerlySerializedAs("CustomEvent2")]
    public MyEvent CustomEvent2 = new MyEvent();

    [Space(2f), Header("Run only one time when open app : open"), Space(2f)]
    [FormerlySerializedAs("CustomEvent3")]
    public MyEvent CustomEvent3 = new MyEvent();

    [Space(2f), Header("Run only one time when open app : close"), Space(2f)]
    [FormerlySerializedAs("CustomEvent4")]
    public MyEvent CustomEvent4 = new MyEvent();

    [SerializeField]
    private TMP_InputField playerName;

    [SerializeField]
    private TextMeshProUGUI welcome;

    [SerializeField]
    private string LobbyScene = "Lobby";

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2.5f);
        if (!PlayerPrefs.HasKey("Player name"))
        {
            yield return new WaitForSeconds(0.6f);
            EventActive3();
        }
        else
        {
            StartCoroutine(LoadSceneAsync(LobbyScene));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void EventActive4()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent4", this);
        CustomEvent4.Invoke();
    }

    private void EventActive3()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent3", this);
        CustomEvent3.Invoke();
    }

    private void EventActive2()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent2", this);
        CustomEvent2.Invoke();
    }

    [Obsolete]
    public void SetPlayerName()
    {
        if (!string.IsNullOrEmpty(playerName.text))
        {
            StartCoroutine(Setname());
        }

    }

    [Obsolete]
    private IEnumerator Setname()
    {
        PlayerPrefs.SetString("Player name", playerName.text);
        EventActive4();
        StartCoroutine(LoadSceneAsync("Lobby"));
        yield return new WaitForSeconds(0.5f);
        SceneManager.UnloadSceneAsync(0);
    }

    public void LoadSound()
    {
        SoundManager.Instance.bg = PlayerPrefs.GetInt("BGMusic", 100) * 0.01f;
        SoundManager.Instance.fx = PlayerPrefs.GetInt("FXSound", 100) * 0.01f;
        SoundManager.CreatePlayBGMusic(SoundManager.Instance.audioClip.BGMusic);
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.welcom);
    }

    private IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        welcome.text = "Welcome " + PlayerPrefs.GetString("Player name");
        yield return new WaitForSeconds(1);
        LoadSound();
        EventActive2();
        op.allowSceneActivation = true;
        while (op.progress <= 0.9f)
        {
            //Wait until the scene is loaded
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2.6f);
        SceneManager.UnloadSceneAsync(1);
    }
}
