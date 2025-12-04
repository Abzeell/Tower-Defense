using UnityEngine;

public class TitleScreenBG : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.StopAllSFX(); //stop all sfx from previous scene
        AudioManager.instance.PlayMusic(AudioManager.instance.titleScreenMusic, 1.0f); 
    }
}
