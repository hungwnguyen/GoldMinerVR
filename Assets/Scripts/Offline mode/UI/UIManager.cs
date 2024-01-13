using UnityEngine;

public class UIManager : MonoBehaviour
{

    public void UISound()
    {
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_touch);
    }

}
