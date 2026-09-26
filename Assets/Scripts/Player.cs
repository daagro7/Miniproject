using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float thrutForce = 10f;
    public float rotationSpeed = 120f;
    public GameObject gun, bulletPrefab;
    public static int SCORE = 0;

    public float xBorderLimit = 6f;
    public float yBorderLimit = 6f;

    public GameObject pauseMenu;
    private bool isPaused = false;

    public GameObject gameOverMenu;

    private Rigidbody _rigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();
        }

        // Infinite space
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit;
        if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit;
        if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit+1;
        if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit;
        transform.position = newPos;

        // Player movement
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrutForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        // Firing bullets
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BulletPool.Instance.GetBullet(gun.transform.position, transform.right);
            // GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            // Bullet bulletScript = bullet.GetComponent<Bullet>();

            // bulletScript.targetVector = transform.right;
        }
    }

    /**
     * Check if collision with an enemy and, in that case,
     * the game is restarted due to having lost the game.
     */
    private void OnCollisionEnter(Collision collision)
    {
        // Collision with Enemy (Asteroid/Meteor)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameOver();
        } else
        {
            Debug.Log("He colisionado con otra cosa");
        }
    }

    /**
     * Check if there is a collision with a bullet and, in that case,
     * the collision is ignore.
     */
    private void OnTriggerEnter(Collider collision)
    {
        // No collision with Bullet
        if (collision.gameObject.CompareTag("Bullet")) {}
    }

    /**
     * Active the pause menu and pause the time
     */
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    /**
     * Resume the game
     */
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    /**
     * Active the game over menu and pause the time
     */
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
        SCORE = 0;
        isPaused = true;
    }
}
