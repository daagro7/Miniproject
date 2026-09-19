using UnityEngine;

public class Player : MonoBehaviour
{

    public float thrutForce = 10f;
    public float rotationSpeed = 120f;

    private Rigidbody _rigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrutForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);
    }
}
