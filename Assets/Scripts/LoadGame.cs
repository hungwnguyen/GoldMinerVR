using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private Michsky.MUIP.ProgressBar progressBar;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(LoadSceneAsync("Login"));
    }

    private IEnumerator LoadSceneAsync(string scene)
    {
        yield return new WaitForSeconds(3f);
        Scene currScene = SceneManager.GetActiveScene();
        AsyncOperation op = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (op.progress <= 0.9f)
        {
            //Wait until the scene is loaded
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
        yield return new WaitForSeconds(0.5f);
        progressBar.currentPercent = 100;
        yield return new WaitForSeconds(0.25f);
        SceneManager.UnloadSceneAsync(currScene);
    }

}
