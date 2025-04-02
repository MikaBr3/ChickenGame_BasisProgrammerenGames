using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab; // Sleep je bullet prefab hiernaartoe in Unity

    void Update()
    {
        // Beweging (WASD/pijltjes)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Schieten (linkermuisknop)
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootDirection = (mousePosition - (Vector2)transform.position).normalized;
            
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetDirection(shootDirection);
        }
    }
}