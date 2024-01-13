using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameSetting : Setting
{
    [Serializable]
    public class MyEvent : UnityEvent { }
    [Space(2f), Header("Event Surrender"), Tooltip("Call when player click surrender"), Space(2f)]
    [FormerlySerializedAs("CustomEvent0")]
    [SerializeField] private MyEvent _customEvent0 = new MyEvent();
    public MyEvent CustomEvent0
    {
        get => _customEvent0;
        set { _customEvent0 = value; }
    }

    private void EventActive0()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent0", this);
        _customEvent0.Invoke();
    }

    public void BackHome()
    {
        
    }

    private IEnumerator LoadScene(string scene)
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(scene);
    }
}
