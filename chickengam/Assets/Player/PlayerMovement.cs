using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Animator animator;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 0.7f; 
    private float nextFireTime = 0f;

    [Header("Shop")]
    public GameObject shopPanel; // Assign your shop UI panel in Inspector

    void Start()
    {
        animator = GetComponent<Animator>();
        if (shopPanel != null) 
            shopPanel.SetActive(false); // Ensure shop starts closed
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleShop();
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude);
        }
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootDirection = (mousePosition - (Vector2)transform.position).normalized;
            
            Instantiate(bulletPrefab, transform.position, Quaternion.identity)
                .GetComponent<Bullet>().SetDirection(shootDirection);
        }
    }

    // Toggle shop visibility (called by B key)
    public void ToggleShop()
    {
        if (shopPanel == null) return;
        
        bool isShopOpen = !shopPanel.activeSelf;
        shopPanel.SetActive(isShopOpen);
        Time.timeScale = isShopOpen ? 0f : 1f;
    }

    // Close shop explicitly (called by UI button)
    public void CloseShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
            Time.timeScale = 1f; // Unpause game
        }
    }

    public void UpgradeFireRate(float amount)
    {
        fireRate = Mathf.Max(0.1f, fireRate - amount);
        Debug.Log($"Upgraded! Fire rate: {fireRate}");
    }
}