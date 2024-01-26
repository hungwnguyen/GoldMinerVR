using UnityEngine;
using UnityEngine.UI;

public abstract class Setting : MonoBehaviour
{
    [SerializeField] protected Slider bgmusic, fxsound;
    [SerializeField] protected Sprite fxloop, fxSound;
    private Sprite fxloopOrigin, fxSoundOrigin;
    [SerializeField] protected Image fxloopImage, fxSoundImage;

    protected virtual void Start()
    {
        bgmusic.value = SoundManager.Instance.bg;
        fxsound.value = SoundManager.Instance.fx;
        fxloopOrigin = fxloopImage.sprite;
        fxSoundOrigin = fxSoundImage.sprite;
        ChangeBGMS();
        ChangeFXMS();
    }

    public void ChangeBGMS()
    {
        if (bgmusic.value == 0){
            fxloopImage.sprite = fxloop;
        } else {
            fxloopImage.sprite = fxloopOrigin;
        }
        SoundManager.ChangeVolumeBGMusic(bgmusic.value);
    }

    public void ChangeFXMS()
    {
        if (fxsound.value == 0){
            fxSoundImage.sprite = fxSound;
        } else {
            fxSoundImage.sprite = fxSoundOrigin;
        }
        SoundManager.ChangeVolumeFXSound(fxsound.value);
    }
}
