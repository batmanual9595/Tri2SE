using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour, ICarObserver
{
    public static GameManagerScript Instance { get; private set; }
    public Transform cameraTransform;

    public GameObject carPrefab;
    public GameObject vanPrefab;
    public GameObject deer;
    public float spawnRadius = 20f;
    public float spawnInterval = 10f;
    public GameObject gameOverPanel;

    public TextMeshProUGUI scoreText;
    private float score = 0f;
    public float scoreIncreaseRate = 1f;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(SpawnCarsAtInterval());
        ragdoll deer = FindObjectOfType<ragdoll>();
        if (deer != null){
            deer.RegisterObserver(this);
        }
        gameOverPanel.SetActive(false);
    }

    void Update(){
        if (!isGameOver){
            score += Time.deltaTime * scoreIncreaseRate;
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
    }

    IEnumerator SpawnCarsAtInterval()
    {
        while (true)
        {
            SpawnCar();
            yield return new WaitForSeconds(spawnInterval - score/10);
        }
    }

    void SpawnCar()
    {
        Vector3 deerPosition = deer.transform.position;

        float randomAngle = Random.Range(0f, 360f);
        Vector3 spawnPosition = deer.transform.position + new Vector3(
            Mathf.Cos(randomAngle) * spawnRadius,
            0f,
            Mathf.Sin(randomAngle) * spawnRadius
        );

        float random = Random.value;
        GameObject spawnedCar;
        if (random < 0.7){
            spawnedCar = Instantiate(carPrefab, spawnPosition, Quaternion.identity);
        }
        else{
            spawnedCar = Instantiate(vanPrefab, spawnPosition, Quaternion.identity);
        }

        CarAI carAI = spawnedCar.GetComponent<CarAI>();
        if (carAI != null)
        {
            carAI.SetTarget(deer.transform);
        }
    }

    public void onDeerKilled(){
        if (isGameOver) return;
        isGameOver = true;
        StartCoroutine(delayedRestart());
    }

    private IEnumerator delayedRestart(){
        yield return new WaitForSeconds(2f);
        ShowGameOverPanel();
    }

    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

}
