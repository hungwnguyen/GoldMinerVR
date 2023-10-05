using DatabaseAPI.Account;
using UnityEngine;
using UnityEngine.UI;

public abstract class Setting : MonoBehaviour
{
    [SerializeField] protected Slider bgmusic, fxsound;

    protected virtual void Start()
    {
        bgmusic.value = SoundManager.Instance.bg;
        fxsound.value = SoundManager.Instance.fx;
    }

    public virtual void SaveChangeSetting()
    {
        #region Save sound
        float value = bgmusic.value * 100;
        AccountController.controller.SetStat("BGMusic", (int)value);
        value = fxsound.value * 100;
        AccountController.controller.SetStat("FXSound", (int)value);
        #endregion
    }

    public void ChangeBGMS()
    {
        SoundManager.ChangeVolumeBGMusic(bgmusic.value);
    }

    public void ChangeFXMS()
    {
        SoundManager.ChangeVolumeFXSound(fxsound.value);
    }
}
