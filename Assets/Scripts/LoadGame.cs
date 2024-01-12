using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LoadGame : MonoBehaviour
{
    [Serializable]
    public class MyEvent : UnityEvent { }
    [SerializeField] private Michsky.MUIP.ProgressBar progressBar;
    [SerializeField] private string sceneName = null;

    [Space(2f), Header("Run only one time when game start"), Space(2f)]
    [FormerlySerializedAs("CustomEvent2")]
    public MyEvent CustomEvent2 = new MyEvent();

    private void EventActive2()
    {
        UISystemProfilerApi.AddMarker("MyEvent.CustomEvent2", this);
        CustomEvent2.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAsync(sceneName));
        //PlayerPrefs.DeleteAll();
    }

    private IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (op.progress <= 0.9f)
        {
            //Wait until the scene is loaded
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1.5f);
        progressBar.currentPercent = 100;
        yield return new WaitForSeconds(0.25f);
        EventActive2();
        yield return new WaitForSeconds(1);
        op.allowSceneActivation = true;
        if (PlayerPrefs.HasKey("Player name"))
        {
            SceneManager.UnloadSceneAsync(0);
        }
    }

}