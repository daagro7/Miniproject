using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    // Pool config
    public GameObject bulletPrefab;
    public int poolSize = 20;
    public Vector3 offscreenPosition = new Vector3(-999f, -999f, 0f);

    private Queue<GameObject> _pool = new Queue<GameObject>();

    /**
     * It automatically executes only once when a script instance is loaded into the scene.
     */
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        InitializePool();
    }

    /**
     * Initialize the pool
     */
    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, offscreenPosition, Quaternion.identity);
            bullet.transform.SetParent(transform);
            bullet.SetActive(false);
            _pool.Enqueue(bullet);
        }
    }

    /**
     * Ask for a bullet from the pool and positions it on the scene.
     *
     * @return GameObject
     */
    public GameObject GetBullet(Vector3 position, Vector3 direction)
    {
        GameObject bullet = null;

        // Buscamos en la cola hasta encontrar una bala que SÍ exista en memoria
        while (_pool.Count > 0)
        {
            bullet = _pool.Dequeue();
            
            // Si la bala no ha sido destruida (no es null), la usamos
            if (bullet != null) 
            {
                break;
            }
        }

        // Si la cola estaba vacía o todas las balas de la cola habían sido destruidas, creamos una nueva
        if (bullet == null)
        {
            bullet = Instantiate(bulletPrefab);
        }

        // Posicionamos y activamos la bala
        bullet.transform.position = position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.SetActive(true);

        // Asignamos la dirección a la bala
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.targetVector = direction;
        }

        return bullet;
    }

    /**
     * Return the bullet to the pool and hide it from the scene.
     */
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = offscreenPosition;
        _pool.Enqueue(bullet);
    }
}