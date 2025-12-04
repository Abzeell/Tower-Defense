using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using MyDataStructures;

[System.Serializable]
public class Wave // a wave class holding an enemyType array to be spawned in a certain wave
{
    public string name;
    public waveSpawner.enemyType[] enemiesInThisWave;
}

public class waveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class enemyType // an enemy type class that holds that name, prefab, count, and spawn delay
    {
        public string name;
        public GameObject prefab;
        public int count = 5;
        public float spawnDelay = 1f;
    }

    [Header("Wave Configuration")]
    public Wave[] waves; // an array of wave classes to hold different wave configurations

    [Header("Timing Settings")]
    public float timeBetweenWaves = 3f;
    public float startDelay = 3f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveInfoText;
    [SerializeField] private float waveMessageDuration = 2f;

    // Internal State
    private int currentWave = 0;
    private Path _path;
    private MyDataStructures.Queue<enemyType> _enemyQueue;
    private int activeEnemies = 0;

    // called when scene starts
    private void Start()
    {
        _path = FindFirstObjectByType<Path>();

        if (_path == null || _path.PathNodes == null || _path.PathNodes.Head == null)
        {
            Debug.LogError("❌ Path not found or empty!");
            enabled = false;
            return;
        }

        _enemyQueue = new MyDataStructures.Queue<enemyType>();
        StartCoroutine(SpawnWaves());
    }

    // wave system

    private IEnumerator SpawnWaves()
    {
        // Delay before wave 1
        if (startDelay > 0)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.waveCountdown);
            yield return StartCoroutine(WaveCountdown(1, startDelay)); // start the countdown before wave 1
        }

        while (currentWave < waves.Length)
        {
            // Wave start message
            ShowWaveMessage($"Wave {currentWave + 1} starting!");

            PrepareQueue();

            // Enemy total for UI
            int enemiesThisWave = _enemyQueue.Count;
            SetWaveText($"Wave {currentWave + 1} — Enemies: {enemiesThisWave}");

            Debug.Log($"🌊 Starting Wave {currentWave + 1}");

            // Spawn enemies in queue
            while (!_enemyQueue.IsEmpty()) // runs as long as enemyQueue is not empty
            {
                enemyType enemyToSpawn = _enemyQueue.Dequeue(); // dequeues enemy objects in the enemyQueue one by one

                if (enemyToSpawn.prefab != null)
                    SpawnEnemy(enemyToSpawn.prefab); // spawn the enemy that was dequeued

                yield return new WaitForSeconds(enemyToSpawn.spawnDelay); // wait for the spawn delay (assigned in the inspector)
            }

            Debug.Log($"⏳ Wave {currentWave + 1} spawned. Waiting for all enemies..."); // message after spawning all enemies in a wave

            yield return new WaitUntil(() => activeEnemies == 0); // Wait until all enemies die

            Debug.Log($"✅ Wave {currentWave + 1} cleared!"); // message after clearing a wave

            currentWave++; // increment to next wave

            if (currentWave < waves.Length) // if there are still consecutive waves
            {
                yield return StartCoroutine(WaveCountdown(currentWave + 1, timeBetweenWaves)); // repeat everything above for next waves
            }
            else // all waves completed
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.waveComplete); // play celebration music
                SetWaveText("All waves completed!");
                
                yield return new WaitForSeconds(4f); // wait for delay before switcing (so the music can play to the end)

                Time.timeScale = 1f; // make sure game is running before switching scenes

                SceneManager.LoadScene("Game Complete Screen"); // go to game complete scene
            }
        }
    }

    // wave countdown
    private IEnumerator WaveCountdown(int waveNumber, float seconds)
    {
        float timer = seconds;

        AudioManager.instance.PlaySFX(AudioManager.instance.waveCountdown); // play countdown sfx

        while (timer > 0)
        {
            SetWaveText($"Wave {waveNumber} starting in {Mathf.Ceil(timer)}..."); // display countdown text
            yield return new WaitForSeconds(1f);
            timer -= 1f; // decrement for count down text
        }

        // Final alert
        SetWaveText($"Wave {waveNumber} starting!");
        yield return new WaitForSeconds(waveMessageDuration);

        // Clear text
        SetWaveText("");
    }

    // queue preparation
    private void PrepareQueue()
    {
        _enemyQueue.Clear(); // make sure queue is cleared

        Wave waveData = waves[currentWave]; // gets current wave number and gets all the enemies in the array of that index

        foreach (var e in waveData.enemiesInThisWave) // get each enemy prefab in the wave class
        {
            if (e.prefab == null)
            {
                Debug.LogWarning($"⚠️ Missing prefab for enemy type: {e.name}");
                continue;
            }

            for (int i = 0; i < e.count; i++)
                _enemyQueue.Enqueue(e); // enqueues them into the array for next wave
        }
    }

    // enemy spawning
    private void SpawnEnemy(GameObject prefab)
    {
        Vector3 spawnPos = _path.PathNodes.Head.Value.transform.position; // gets the head of the path and spawns the enemies into that
        GameObject instance = Instantiate(prefab, spawnPos, Quaternion.identity); // instantiate

        Enemy enemyComp = instance.GetComponent<Enemy>(); // stores it into enemyComp for variable for subscribing to the lambda functions bellow

        if (enemyComp == null)
        {
            Debug.LogWarning($"⚠️ Spawned prefab {prefab.name} has no Enemy component.");
            return;
        }

        activeEnemies++; // increment for counter
        Debug.Log($"➕ Enemy spawned. ActiveEnemies = {activeEnemies}");

        // Subscribe to death/path end events
        enemyComp.OnKilled += OnEnemyRemoved; // access each enemy spawned's onKilled action and subscribes the onEnemyRemoved function
        enemyComp.OnPathReachedEnd += OnEnemyRemoved; // access each enemy spawned's onPathReachedEnd action and subscribes the onEnemyRemoved function
    }

    private void OnEnemyRemoved()
    {
        activeEnemies = Mathf.Max(0, activeEnemies - 1); // decrements the enemy counter
        Debug.Log($"➖ Enemy removed. ActiveEnemies = {activeEnemies}");
    }

    // UI handling
    private void ShowWaveMessage(string message)
    {
        SetWaveText(message);
        StartCoroutine(ClearWaveMessageAfterDelay());
    }

    private void SetWaveText(string message)
    {
        if (waveInfoText != null)
            waveInfoText.text = message;
    }

    private IEnumerator ClearWaveMessageAfterDelay()
    {
        yield return new WaitForSeconds(waveMessageDuration);
        if (waveInfoText != null)
            waveInfoText.text = "";
    }
}
