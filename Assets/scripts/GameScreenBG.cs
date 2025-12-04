using UnityEngine;

public class GameScreenBG : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.StopAllSFX();
        AudioManager.instance.PlayMusic(AudioManager.instance.gameScreenMusic, 0.1f);
    }
}
