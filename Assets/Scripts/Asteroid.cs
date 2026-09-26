using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Asteroid : MonoBehaviour
{

    public bool isMainAsteroid = true;
    public float smallScaleMultiplier = 0.5f;
    public int splitCount = 2;
    public float splitForce = 15f;
    public float splitAngle = 60f;
    public float maxTimeLife = 3f;

    // Despawn limits
    public float xLimit = 7f;
    public float yLimit = 7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (Mathf.Abs(pos.x) > xLimit || Mathf.Abs(pos.y) > yLimit)
        {
            Destroy(gameObject);
        }
    }

    /**
     * Check if the asteroid is the main asteroid, in that case,
     * the asteroid split in two
     */
    public void OnHit(Vector3 bulletDirection)
    {
        if (isMainAsteroid)
        {
            Split(bulletDirection);
        }

        Destroy(gameObject);
    }

    /**
     * Split the asteroid/meteor in two
     */
    private void Split(Vector3 bulletDirection)
    {
        
        Vector3 baseDirection = bulletDirection.normalized;

        float halfAngle = splitAngle / 2f;

        Quaternion leftRotation = Quaternion.Euler(0, 0, halfAngle);
        Quaternion rightRotation = Quaternion.Euler(0, 0, -halfAngle);

        Vector3 dir1 = leftRotation * baseDirection;
        Vector3 dir2 = rightRotation * baseDirection;

        Vector3[] spawnDirections = new Vector3[] { dir1, dir2 };

        for (int i = 0; i < splitCount; i++)
        {
            GameObject smallAsteroid = Instantiate(gameObject, transform.position, Quaternion.identity);
            
            smallAsteroid.transform.localScale = transform.localScale * smallScaleMultiplier;
            
            Asteroid smallAstScript = smallAsteroid.GetComponent<Asteroid>();
            if (smallAstScript != null)
            {
                smallAstScript.isMainAsteroid = false;
            }
            
            Rigidbody rb = smallAsteroid.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddForce(spawnDirections[i] * splitForce, ForceMode.Impulse);
                // Vector3 randomDirection = Random.insideUnitSphere;
                // randomDirection.z = 0;
                // rb.AddForce(randomDirection.normalized * splitForce, ForceMode.Impulse);
            }

            // Destroy(smallAsteroid, maxTimeLife);
        }
    }
}
