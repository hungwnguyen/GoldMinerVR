using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Setting : MonoBehaviour
{
    [SerializeField] protected Slider bgmusic, fxsound;
    

    protected virtual void Start()
    {
        bgmusic.value = SoundManager.Instance.bg;
        fxsound.value = SoundManager.Instance.fx;
        ChangeBGMS();
        ChangeFXMS();
    }

    public virtual void LoadScene(string name){
        SceneManager.LoadScene(name);
    }

    public virtual void ChangeBGMS()
    {
        SoundManager.ChangeVolumeBGMusic(bgmusic.value);
    }

    public virtual void ChangeFXMS()
    {
        SoundManager.ChangeVolumeFXSound(fxsound.value);
    }
}
