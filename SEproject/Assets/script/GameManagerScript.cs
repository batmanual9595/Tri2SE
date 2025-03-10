using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }
    public Transform cameraTransform;

    public GameObject carPrefab;  // The car prefab to spawn
    public GameObject deer;       // The deer GameObject (drag it from the scene)
    public float spawnRadius = 20f; // The radius around the deer to spawn cars
    public float spawnInterval = 10f; // Time interval to spawn cars (in seconds)

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Start spawning cars every spawnInterval seconds
        StartCoroutine(SpawnCarsAtInterval());
    }

    IEnumerator SpawnCarsAtInterval()
    {
        while (true)
        {
            SpawnCar();  // Spawn a car
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval before spawning the next car
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

        GameObject spawnedCar = Instantiate(carPrefab, spawnPosition, Quaternion.identity);

        CarAI carAI = spawnedCar.GetComponent<CarAI>();
        if (carAI != null)
        {
            carAI.SetTarget(deer.transform);
        }
    }
}
