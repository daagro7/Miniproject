using UnityEngine;
using UnityEngine.SceneManagement;

public class Meteor : MonoBehaviour
{

    public GameObject meteorPrefab;
    public float spawnRatePerMinute = 30f;
    public float spawnRateIncrement = 1f;

    public float xLimit;
    public float yLimit;
    public float maxTimeLife = 3f;

    public float minForce = 2f;
    public float maxForce = 5f;

    private float spawnNext = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (meteorPrefab != null && Time.time > spawnNext)
        {
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            spawnRatePerMinute += spawnRateIncrement;

            SpawnAsteroid();
        }
    }

    /**
     * Calculate a random point of the border and spawns the asteroid there.
     * Rotate the sprite and give the asteroid a force to move
     */
    private void SpawnAsteroid()
    {
        Vector2 spawnPos = Vector2.zero;

        // Choose a random border
        int edge = Random.Range(0,4);
        switch (edge)
        {
            case 0: // Up
                spawnPos = new Vector2(Random.Range(-xLimit, xLimit), yLimit);
                break;
            case 1: // Down
                spawnPos = new Vector2(Random.Range(-xLimit, xLimit), -yLimit);
                break;
            case 2: // Left
                spawnPos = new Vector2(-xLimit, Random.Range(-yLimit, yLimit));
                break;
            case 3: // Right
                spawnPos = new Vector2(xLimit, Random.Range(-yLimit, yLimit));
                break;
        }

        // Calculate the direction the asteroid will go
        Vector2 targetPoint = Random.insideUnitCircle * 5f;
        Vector2 randomDir = (targetPoint - spawnPos).normalized;

        // Calculate the rotation so that it points towards rnadomDir
        float angle = (Mathf.Atan2(randomDir.y, randomDir.x) * Mathf.Rad2Deg) + 90f;
        Quaternion spawnRotation = Quaternion.Euler(0, 0, angle);

        GameObject meteor = Instantiate(meteorPrefab, spawnPos, spawnRotation);

        // Apply the force into the rigidbody
        Rigidbody rb = meteor.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float randomForce = Random.Range(minForce, maxForce);
            rb.AddForce(randomDir * randomForce, ForceMode.Impulse);
        }

        Destroy(meteor, maxTimeLife);
    }
}
