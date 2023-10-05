using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Serializable]
    public class MyEvent : UnityEvent { }
    [Space(5f), Header("Run on start"), Space(5f)]
    [FormerlySerializedAs("CustomEvent1")]
    [SerializeField] private MyEvent _customEvent1 = new MyEvent();

    public MyEvent CustomEvent1
    {
        get => _customEvent1;
        set { _customEvent1 = value; }
    }

    [SerializeField]
    private TextMeshProUGUI roomID;

    [DllImport("__Internal")]
    private static extern void CopyTextToClipboard(string text);

    [SerializeField] private CountDown windowCountDown;

    private IEnumerator Start()
    {
        while(SceneManager.sceneCount > 1)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        roomID.text = ExampleManager.Instance.GetRoomID();
        EventActive1();
    }
   
    void EventActive1()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent1", this);
        _customEvent1.Invoke();
    }

    public void UpdatePlayerStatus()
    {
        windowCountDown.OpenWindow();
        this.gameObject.SetActive(false);
    }
    
    public void Copy()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        CopyTextToClipboard(roomID.text);
#else
        GUIUtility.systemCopyBuffer = roomID.text;
#endif
    }

    public void ReadyPlay()
    {
        GameManager.Instance.PlayerReadyToPlay();
    }

    public void UISound()
    {
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_touch);
    }

    private PlayerManager GetPlayerView(string entityID)
    {
        if (ExampleManager.Instance.HasEntityView(entityID))
        {
            return ExampleManager.Instance.GetEntityView(entityID) as PlayerManager;
        }

        return null;
    }
}
