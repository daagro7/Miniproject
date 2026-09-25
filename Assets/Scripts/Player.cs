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

    private Rigidbody _rigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

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

        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrutForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bullet bulletScript = bullet.GetComponent<Bullet>();

            bulletScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } else
        {
            Debug.Log("He colisionado con otra cosa");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {}
    }
}
