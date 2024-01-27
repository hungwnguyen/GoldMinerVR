using UnityEngine;

public class UIManager : MonoBehaviour
{

    public void UISound()
    {
        SoundManager.CreatePlayFXSound();
    }

    public void UIBuy(){
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_congqua);
    }

    public void UINotBuy(){
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_UnBuy);
    }

}
