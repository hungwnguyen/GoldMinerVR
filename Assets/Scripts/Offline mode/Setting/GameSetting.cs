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

    public void RePlayGame(){
        Pod.Instance.FSM.ChangeState(Pod.Instance.PodIdleState);
        Spawner.Instance.DestroyAllRod();
        Time.timeScale = 1;
        GameManager.Instance.ResetGame();
        Player.Instance.Score = 0;
        Player.Instance.TNTCount = 2;
        Player.Instance.ResetLevel();
        UIShop.Instance.SetStatus(false);
        UIPopup.Instance.ReSetAmin();
        Spawner.Instance.SpawnRodLevel(1);
    }
}
