using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public void PlayClick()
    {
        if (AudioManager.instance != null)
        {
            // Plays the 'Button Click' sound you assigned in the AudioManager Inspector
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick);
        }
    }
}