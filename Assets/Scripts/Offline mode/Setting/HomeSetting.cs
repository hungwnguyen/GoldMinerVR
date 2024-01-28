public class HomeSetting : Setting
{
    protected override void Start()
    {
        SoundManager.CreatePlayBgMusic(SoundManager.Instance.audioClip.aud_bgMusic[UnityEngine.Random.Range(0, 2)]);
        base.Start();
    }

    public override void LoadScene(string name)
    {
        base.LoadScene(name);
        SoundManager.DisableBGMusic();
    }

    public override void ChangeBGMS()
    {
        base.ChangeBGMS();
        if (bgmusic.value > 0){
            if (!SoundManager.ContinuePlayAllMusic()){
                SoundManager.CreatePlayBgMusic(SoundManager.Instance.audioClip.aud_bgMusic[UnityEngine.Random.Range(0, 2)]);
            }
        }
    }

}