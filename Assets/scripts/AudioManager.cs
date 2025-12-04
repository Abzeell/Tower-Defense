using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton Instance
    public static AudioManager instance;

    [Header("Audio Sources")]
    // Create 3 AudioSource components on the object and drag them here
    public AudioSource musicSource;   // Check 'Loop' in Inspector
    public AudioSource sfxSource;     // Uncheck 'Loop'
    public AudioSource loopFxSource;  // Uncheck 'Loop' (we control this via code)

    [Header("Music Tracks")]
    public AudioClip titleScreenMusic; // TitleScreen_bgMusic.mp3
    public AudioClip gameOverMusic;    // GameOver_bgMusic.mp3
    public AudioClip gameCompletionMusic; //gameCompletion.mp3
    public AudioClip gameScreenMusic; // gameScreenBGMusic.mp3

    [Header("UI & Wave Sounds")]
    public AudioClip buttonClick;      // button_click-fx.mp3
    public AudioClip waveCountdown;    // wave_countdown_sfx.mp3
    public AudioClip waveComplete;     // wave_complete_fx.wav
    public AudioClip insufficientFunds; // insufficientFunds.mp3

    [Header("Gameplay & Combat")]
    public AudioClip towerBuild;       // building_tower_chaChing_sfx.mp3
    public AudioClip arrowShoot;       // arrow_shoot_fx.mp3
    public AudioClip wizardImpact;     // wizard_projectile_impact_fx.mp3
    public AudioClip playerDamage;     // player_takeDamage_sfx.mp3
    public AudioClip lowHealth;        // lowHealth_sfx.mp3

    void Awake()
    {
        // 1. Check if an instance already exists
        if (instance == null)
        {
            // 2. Make THIS the main instance
            instance = this;
        
            // 3. IMPORTANT: Tell Unity not to kill this specific object
            DontDestroyOnLoad(gameObject);
        }
        else
        {
             // 4. If another AudioManager exists, destroy this new one
            Destroy(gameObject);
        }
    }
    // music system
    public void PlayMusic(AudioClip clip, float volume)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    // sound effects system
    public void PlaySFX(AudioClip clip)
    {

        sfxSource.PlayOneShot(clip);
    }

    public void StopAllSFX()
    {       
    // Stop the one-shot sound effects (explosions, clicks)
    if (sfxSource != null) 
    {
        sfxSource.Stop();
    }

    // Stop the looping sound effects (alarms, fire, wind)
    if (loopFxSource != null) 
    {
        loopFxSource.Stop();
    }

    if (musicSource != null) 
    {
        musicSource.Stop();
    }
}
}