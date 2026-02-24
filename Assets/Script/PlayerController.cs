using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float flapForce = 5f;
    public AudioClip flapSfx;
    public AudioClip hitSfx;

    Rigidbody2D rb;
    bool isDead = false;
    AudioSource audioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        // Freeze movement until first flap if you want:
        rb.simulated = true;
    }

    void Update()
    {
        if (isDead) return;

        // Input: space, left mouse, or touch
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Flap();
        }
    }

    void Flap()
    {
        // Zero vertical velocity then apply upward impulse for consistent flaps
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);

        if (flapSfx != null)
        {
            audioSource.PlayOneShot(flapSfx);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        Die();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("ScoreZone"))
        {
            ScoreManager.Instance.AddScore(1);
        }
    }

    void Die()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
        // optional: disable physics so it stops moving
        rb.simulated = false;

        if (hitSfx != null)
        {
            audioSource.PlayOneShot(hitSfx);
        }

        GameManager.Instance.GameOver();
    }

    public bool IsDead => isDead;
}
