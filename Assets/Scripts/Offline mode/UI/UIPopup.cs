using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class UIPopup : MonoBehaviour
{
    [Serializable]
    public class MyEvent : UnityEvent { }
    
    [Space(5f), Header("Run when end game"), Space(5f)]
    [FormerlySerializedAs("CustomEvent")]
    [SerializeField] private MyEvent _customEvent = new MyEvent();
    /// <summary>
    /// Run when end game
    /// </summary>
    /// <value></value>
    public MyEvent CustomEvent
    {
        get => _customEvent;
        set { _customEvent = value; }
    }
    
    public static UIPopup Instance {get; private set;}

    void Awake()
    {
        if (Instance != null){
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    private void EventEndGame()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent", this);
        _customEvent.Invoke();
        Time.timeScale = 0;
        SoundManager.DisableAllMusic();
    }

    public void PauseGame(){
        Time.timeScale = 0;
        SoundManager.PauseAllMusic();
    }

    public void ContinueGame(){
        SoundManager.ContinuePlayAllMusic();
        Time.timeScale = 1;
    }

    public void WinAppear(){
        EventEndGame();
    }

    public void LostAppear(){
        EventEndGame();
    }

    public void TargetAppear(){
    }

    public void MessengerAppear(){
    }

}
