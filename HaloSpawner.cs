using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HaloSpawner : MonoBehaviour
{
    public GameObject haloPrefab; // Assign the halo prefab in the Inspector
    public AudioSource musicSource; // The audio source playing the music
    public int bpm = 120; // Beats per minute of the music
    public float initialSpawnInterval = 1.0f; // Initial interval between spawns
    public Vector3 initialPosition = new Vector3(0, 0, 0); // Starting position for the first halo
    public float rotationX = 0f; // Rotation of the halos around X axis
    public float rotationY = 0f; // Rotation of the halos around Y axis
    public float rotationZ = 0f; // Rotation of the halos around Z axis
    public float randomOffsetX = 5.0f; // Maximum random offset for X axis
    public float randomOffsetZ = 5.0f; // Maximum random offset for Z axis
    public float distanceBetweenHalos = 10.0f; // Distance between each halo
    public float distanceIncreaseRate = 0.1f; // Rate at which distance between halos increases
    public float minSpawnInterval = 0.5f; // Minimum time between each spawn
    public int initialHaloCount = 10; // Number of halos to spawn initially

    public float initialSpeed = 80.0f; // Initial speed of the halos
    public float decelerationRate = 10.0f; // Rate at which the halos slow down
    public float maxZDistance = 50.0f; // Maximum distance on Z axis from which halos can spawn

    private List<GameObject> halos = new List<GameObject>();
    private float nextSpawnTime;
    private float lastSpawnTime;
    private float[] samples = new float[1024];
    private float[] spectrum = new float[1024];
    private float rmsValue;
    private float dbValue;
    private float pitchValue;
    private float threshold = 0.02f; // Adjust this threshold to detect beats
    private float lastBeatTime;
    private bool isBeatDetected;
    private int spawnedHaloCount;

    void Start()
    {
        // Calculate the spawn interval based on BPM
        float spawnInterval = 60.0f / bpm;
        nextSpawnTime = spawnInterval;
        lastSpawnTime = -minSpawnInterval; // Ensure the first halo can spawn immediately

        // Spawn initial halos
        for (int i = 0; i < initialHaloCount; i++)
        {
            SpawnHalo();
        }

        // Start coroutine to increase distance between halos
        StartCoroutine(IncreaseDistanceBetweenHalos());
    }

    void Update()
    {
        AnalyzeAudio();
        if (isBeatDetected && musicSource.time >= nextSpawnTime && Time.time - lastSpawnTime >= minSpawnInterval)
        {
            SpawnHalo();
            nextSpawnTime += 60.0f / bpm; // Update next spawn time based on BPM
            lastSpawnTime = Time.time; // Update last spawn time
        }
    }

    void SpawnHalo()
    {
        // Apply random offset to X position
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetX, randomOffsetX),
            0,
            0
        );

        // Randomly choose +Z or -Z axis for spawning
        float zSpawnOffset = Random.Range(0, 2) == 0 ? maxZDistance : -maxZDistance;
        Vector3 spawnPosition = new Vector3(initialPosition.x + randomOffset.x, initialPosition.y, initialPosition.z + zSpawnOffset);

        Quaternion haloRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
        GameObject halo = Instantiate(haloPrefab, spawnPosition, haloRotation);
        halos.Add(halo);

        // Start the coroutine to move the halo
        StartCoroutine(MoveHalo(halo));

        // Update initial position for next spawn
        initialPosition = GetNextPosition(initialPosition);
    }

    IEnumerator MoveHalo(GameObject halo)
    {
        Vector3 startPosition = halo.transform.position;
        Vector3 endPosition = initialPosition; // Target position for the halo
        float currentSpeed = initialSpeed;

        while (currentSpeed > 0)
        {
            currentSpeed -= decelerationRate * Time.deltaTime; // Decrease the speed
            currentSpeed = Mathf.Max(currentSpeed, 0); // Ensure speed doesn't go below 0

            halo.transform.position = Vector3.MoveTowards(halo.transform.position, endPosition, currentSpeed * Time.deltaTime);
            yield return null;
        }
    }

    Vector3 GetNextPosition(Vector3 currentPosition)
    {
        // Randomly decide the horizontal direction for the next halo (left, right, or no change)
        int horizontalDirection = Random.Range(0, 3);
        switch (horizontalDirection)
        {
            case 0: // Move left
                currentPosition.x -= distanceBetweenHalos * (1 + bpm / 100.0f);
                break;
            case 1: // Move right
                currentPosition.x += distanceBetweenHalos * (1 + bpm / 100.0f);
                break;
            case 2: // No horizontal change
                break;
        }

        // Move downward along the Y-axis
        currentPosition.y -= distanceBetweenHalos * (1 + bpm / 100.0f);

        return currentPosition;
    }

    void AnalyzeAudio()
    {
        musicSource.GetOutputData(samples, 0);
        musicSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        rmsValue = 0;
        for (int i = 0; i < samples.Length; i++)
        {
            rmsValue += samples[i] * samples[i];
        }
        rmsValue = Mathf.Sqrt(rmsValue / samples.Length);
        dbValue = 20 * Mathf.Log10(rmsValue / 0.1f);

        float maxV = 0;
        int maxN = 0;
        for (int i = 0; i < spectrum.Length; i++)
        {
            if (spectrum[i] > maxV && spectrum[i] > threshold)
            {
                maxV = spectrum[i];
                maxN = i;
            }
        }
        pitchValue = maxN * 24000 / spectrum.Length;

        if (rmsValue > threshold && Time.time - lastBeatTime > 60.0f / bpm / 2)
        {
            isBeatDetected = true;
            lastBeatTime = Time.time;
        }
        else
        {
            isBeatDetected = false;
        }
    }

    private IEnumerator IncreaseDistanceBetweenHalos()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f); // Adjust the interval as needed
            distanceBetweenHalos += distanceIncreaseRate;
        }
    }
}