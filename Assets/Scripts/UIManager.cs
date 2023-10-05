using System.Collections;
using UnityEngine;
using TMPro;
using DatabaseAPI.Account;
using UnityEngine.Serialization;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class UIManager : MonoBehaviour
{

    public void UISound()
    {
        SoundManager.CreatePlayFXSound(SoundManager.Instance.audioClip.aud_touch);
    }

}
