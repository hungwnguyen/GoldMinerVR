using UnityEngine;
using UnityEngine.UI;
using yuki;

public class GameSetting : Setting
{
    [SerializeField] protected Sprite fxloop, fxSound;
    private Sprite fxloopOrigin, fxSoundOrigin;
    [SerializeField] protected Image fxloopImage, fxSoundImage;

    protected override void Start()
    {
        fxloopOrigin = fxloopImage.sprite;
        fxSoundOrigin = fxSoundImage.sprite;
        base.Start();
    }

    public override void LoadScene(string name)
    {
        base.LoadScene(name);
        Time.timeScale = 1;
        SoundManager.DisableAllMusic();
    }

    public override void ChangeBGMS()
    {
        if (bgmusic.value == 0){
            fxloopImage.sprite = fxloop;
        } else {
            fxloopImage.sprite = fxloopOrigin;
        }
        base.ChangeBGMS();
    }

    public override void ChangeFXMS()
    {
        if (fxsound.value == 0){
            fxSoundImage.sprite = fxSound;
        } else {
            fxSoundImage.sprite = fxSoundOrigin;
        }
        base.ChangeFXMS();
    }
}
