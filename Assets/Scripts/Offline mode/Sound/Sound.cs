using UnityEngine;

public class Sound : MonoBehaviour, IPooledObject
{
    private AudioSource audioSource;
    
    public void OnObjectSpawn()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        audioSource.Play();
        Invoke("DeactivateGameObject", audioSource.clip.length);
    }

    public void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
}
