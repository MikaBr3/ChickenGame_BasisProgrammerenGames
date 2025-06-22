using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float fireRate = 0.5f; // Time between shots
    public GameObject bulletPrefab; // Sleep je bullet prefab hiernaartoe in Unity

    private Rigidbody2D rb;
    private Vector2 movement;
    private float lastShotTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Beweging input (WASD/pijltjes)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        // Schieten (linkermuisknop)
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShotTime + fireRate)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootDirection = (mousePosition - (Vector2)transform.position).normalized;
            
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetDirection(shootDirection);
            
            lastShotTime = Time.time;
        }
    }

    void FixedUpdate()
    {
        // Fysica-gebaseerde beweging
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}