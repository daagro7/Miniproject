using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteor : MonoBehaviour
{

    public GameObject meteorPrefab;
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;
    public float xLimit;
    public float maxTimeLife = 3f;

    private float spawnNext = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;

            spawnRatePerMinute += spawnRateIncrement;

            float rand = Random.Range(-xLimit, xLimit);

            Vector2 spawnPosition = new Vector2(rand, 8f);

            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

            Destroy(meteor, maxTimeLife);
        }
    }
}
