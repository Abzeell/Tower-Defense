using UnityEngine;

public class GameoverScreenBG : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.StopAllSFX();
        AudioManager.instance.PlayMusic(AudioManager.instance.gameOverMusic,1.0f);
    }
}
