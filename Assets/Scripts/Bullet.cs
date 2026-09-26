using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;

    /**
     * Programmed its deactivation if it doesn't collide with anything in X seconds
     */
    private void OnEnable()
    {
        CancelInvoke(nameof(Deactivate));
        
        Invoke(nameof(Deactivate), maxLifeTime);
    }

    /**
     * Canceled the timer to prevent accidental calls when it's saved.
     */
    private void OnDisable()
    {
        CancelInvoke(nameof(Deactivate));
    } 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * targetVector * Time.deltaTime);
    }

    /**
     * Check if collision with an enemy and, in that case,
     * increase the score and destroys the asteroid/meteor and the bullet
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IncreaseScore();

            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid != null)
            {
                asteroid.OnHit(targetVector);
            } else
            {
                Destroy(collision.gameObject);
            }
            
            Destroy(gameObject);
        }
    }

    /**
     * Return the bullet calling the function ReturnBullet of BulletPool
     */
    private void Deactivate()
    {
        BulletPool.Instance.ReturnBullet(gameObject);
    }

    /**
     * Increase the score in 1
     */
    private void IncreaseScore()
    {
        Player.SCORE++;
        Debug.Log(Player.SCORE);
        UpdateScoreText();
    }

    /**
     * Update the score text
     */
    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Score: " + Player.SCORE;
    }
}
