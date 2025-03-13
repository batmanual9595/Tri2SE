using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour, ICarObserver
{
    public static GameManagerScript Instance { get; private set; }
    public Transform cameraTransform;

    [Header("Prefabs")]
    public GameObject carPrefab;
    public GameObject vanPrefab;
    public GameObject deer;
    public TitleMusicManager music;

    [Header("Spawning")]
    public float spawnInterval = 10f;
    public float minSpawnRadius = 5f;
    public float maxSpawnRadius = 10f;

    [Header("UI")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScore;
    private float score = 0f;
    public float scoreIncreaseRate = 1f;

    private bool isGameOver = false;

    [Header("Car Stats")]
    public float baseSpeed = 5f;
    public float maxSpeed = 20f;
    public float speedMultiplier = 0.1f;

    [Header("References")]
    public AudioSource audioSource;
    public AudioClip[] laughSounds;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(SpawnCarsAtInterval());
        ragdoll deer = FindObjectOfType<ragdoll>();
        if (deer != null)
        
        {
            deer.RegisterObserver(this);
        }
        gameOverPanel.SetActive(false);
    }

    void Update()
    
    {
        if (!isGameOver)
        
        {
            score += Time.deltaTime * scoreIncreaseRate;
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
    }

    public void addPoints(float points)
    {
        score += points;
    }

    IEnumerator SpawnCarsAtInterval()
    {
        while (true)
        {
            SpawnCar();
            yield return new WaitForSeconds(spawnInterval - score / 10);
            yield return new WaitForSeconds(spawnInterval - score / 10);
        }
    }

    void SpawnCar()
    {
        Vector3 deerPosition = deer.transform.position;

        float randomAngle = Random.Range(0f, 360f);

        float randomDistance = Random.Range(minSpawnRadius, maxSpawnRadius);

        Vector3 spawnPosition = deerPosition + new Vector3(
            Mathf.Cos(randomAngle) * randomDistance,
            0f,
            Mathf.Sin(randomAngle) * randomDistance
        );

        float random = Random.value;
        GameObject spawnedCar;

        if (random < 0.7f)
        
        {
            spawnedCar = Instantiate(carPrefab, spawnPosition, Quaternion.identity);
        }

        else
        
        {
            spawnedCar = Instantiate(vanPrefab, spawnPosition, Quaternion.identity);
        }

        ApplyRandomColor(spawnedCar);

        float speedLevel = Mathf.Clamp(baseSpeed + (score * speedMultiplier), baseSpeed, maxSpeed);
        CarBehavior carBehavior = spawnedCar.GetComponent<CarBehavior>();
        if (carBehavior != null)
        {
            carBehavior.SetSpeed(speedLevel);
        }
    }


    void ApplyRandomColor(GameObject car)
    {
        Renderer carRenderer = car.GetComponentInChildren<Renderer>();

        Color randomColor = new Color(Random.value, Random.value, Random.value);
        foreach (Material mat in carRenderer.materials)
        {
            mat.color = randomColor;
        }
    }

    public void onDeerKilled()
    
    {
        if (isGameOver) return;
        spawnInterval = 0f;
        music.StopTitleMusic();
        isGameOver = true;
        StartCoroutine(delayedRestart());
    }

    private IEnumerator delayedRestart()
    
    {
        yield return new WaitForSeconds(2f);
        ShowGameOverPanel();
        LaughSoundEffect();
    }

    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverScore.text = "Score: " + Mathf.FloorToInt(score);
        }
    }

    private void LaughSoundEffect()
    {
        int randomIndex = Random.Range(0, laughSounds.Length);
        AudioClip randomSound = laughSounds[randomIndex];

        audioSource.PlayOneShot(randomSound);
    }
}
