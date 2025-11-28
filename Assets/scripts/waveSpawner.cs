using System;
using System.Collections;
using UnityEngine;
using TMPro;
using MyDataStructures;

[System.Serializable]
public class Wave
{
    public string name;
    public waveSpawner.enemyType[] enemiesInThisWave;
}

public class waveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class enemyType
    {
        public string name;
        public GameObject prefab;
        public int count = 5;
        public float spawnDelay = 1f;
    }

    [Header("Wave Configuration")]
    public Wave[] waves;

    [Header("Timing Settings")]
    public float timeBetweenWaves = 5f;
    public float startDelay = 3f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI waveInfoText;
    [SerializeField] private float waveMessageDuration = 2f;

    // Internal State
    private int currentWave = 0;
    private Path _path;
    private MyDataStructures.Queue<enemyType> _enemyQueue;
    private int activeEnemies = 0;

    // ──────────────────────────────────────────────
    // UNITY LIFECYCLE
    // ──────────────────────────────────────────────
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

    // ──────────────────────────────────────────────
    // CORE WAVE SYSTEM
    // ──────────────────────────────────────────────
    private IEnumerator SpawnWaves()
    {
        // Delay before wave 1
        if (startDelay > 0)
        {
            yield return StartCoroutine(WaveCountdown(1, startDelay));
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
            while (!_enemyQueue.IsEmpty())
            {
                enemyType enemyToSpawn = _enemyQueue.Dequeue();

                if (enemyToSpawn.prefab != null)
                    SpawnEnemy(enemyToSpawn.prefab);

                yield return new WaitForSeconds(enemyToSpawn.spawnDelay);
            }

            Debug.Log($"⏳ Wave {currentWave + 1} spawned. Waiting for all enemies...");

            // Wait until all enemies die
            yield return new WaitUntil(() => activeEnemies == 0);

            Debug.Log($"✅ Wave {currentWave + 1} cleared!");

            currentWave++;

            // Between waves
            if (currentWave < waves.Length)
            {
                yield return StartCoroutine(WaveCountdown(currentWave + 1, timeBetweenWaves));
            }
            else
            {
                SetWaveText("🎉 All waves completed!");
            }
        }
    }

    // ──────────────────────────────────────────────
    // WAVE COUNTDOWN
    // ──────────────────────────────────────────────
    private IEnumerator WaveCountdown(int waveNumber, float seconds)
    {
        float timer = seconds;

        while (timer > 0)
        {
            SetWaveText($"Wave {waveNumber} starting in {Mathf.Ceil(timer)}...");
            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        // Final alert
        SetWaveText($"Wave {waveNumber} starting!");
        yield return new WaitForSeconds(waveMessageDuration);

        // Clear text
        SetWaveText("");
    }

    // ──────────────────────────────────────────────
    // QUEUE PREPARATION
    // ──────────────────────────────────────────────
    private void PrepareQueue()
    {
        _enemyQueue.Clear();

        Wave waveData = waves[currentWave];

        foreach (var e in waveData.enemiesInThisWave)
        {
            if (e.prefab == null)
            {
                Debug.LogWarning($"⚠️ Missing prefab for enemy type: {e.name}");
                continue;
            }

            for (int i = 0; i < e.count; i++)
                _enemyQueue.Enqueue(e);
        }
    }

    // ──────────────────────────────────────────────
    // ENEMY SPAWNING
    // ──────────────────────────────────────────────
    private void SpawnEnemy(GameObject prefab)
    {
        Vector3 spawnPos = _path.PathNodes.Head.Value.transform.position;
        GameObject instance = Instantiate(prefab, spawnPos, Quaternion.identity);

        Enemy enemyComp = instance.GetComponent<Enemy>();

        if (enemyComp == null)
        {
            Debug.LogWarning($"⚠️ Spawned prefab {prefab.name} has no Enemy component.");
            return;
        }

        activeEnemies++;
        Debug.Log($"➕ Enemy spawned. ActiveEnemies = {activeEnemies}");

        // Subscribe to death/path end events
        enemyComp.OnKilled += OnEnemyRemoved;
        enemyComp.OnPathReachedEnd += OnEnemyRemoved;
    }

    private void OnEnemyRemoved()
    {
        activeEnemies = Mathf.Max(0, activeEnemies - 1);
        Debug.Log($"➖ Enemy removed. ActiveEnemies = {activeEnemies}");
    }

    // ──────────────────────────────────────────────
    // UI HANDLING
    // ──────────────────────────────────────────────
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
