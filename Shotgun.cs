using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [Header("Shotgun Settings")]
    public GameObject pelletPrefab;
    public int pelletCount = 5;
    public float spreadAngle = 15f;
    public float shootForce = 10f;
    public float cooldown = 0.5f;

    private float lastShotTime;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastShotTime + cooldown)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    private void Shoot()
    {
        float angleStep = spreadAngle / (pelletCount - 1);
        float startingAngle = -spreadAngle / 2;

        for (int i = 0; i < pelletCount; i++)
        {
            float currentAngle = startingAngle + (angleStep * i);
            GameObject pellet = Instantiate(pelletPrefab, transform.position, Quaternion.Euler(0, 0, currentAngle));
            Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
            rb.AddForce(pellet.transform.up * shootForce, ForceMode2D.Impulse);
        }
    }
}
