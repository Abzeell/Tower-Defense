using UnityEngine;

public class GameCompletionBG : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.StopAllSFX();
        AudioManager.instance.PlayMusic(AudioManager.instance.gameCompletionMusic,1.0f);
    }
}
