using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float tileSize = 1f;
    public LayerMask obstacleLayer;

    private bool canMove = true;

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        Vector2 direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }

        if (direction != Vector2.zero)
        {
            TryMove(direction);
        }
    }

    void TryMove(Vector2 direction)
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = currentPosition + direction * tileSize;

        bool blocked = Physics2D.OverlapCircle(targetPosition, 0.2f, obstacleLayer);

        if (!blocked)
        {
            transform.position = targetPosition;
            CollectCrystalAtCurrentPosition();
            CheckEnemyAtCurrentPosition();
        }
    }

    void CollectCrystalAtCurrentPosition()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.25f);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Crystal"))
            {
                Debug.Log("Crystal collected!");

                Destroy(hit.gameObject);

                if (LevelGoal.Instance != null)
                {
                    LevelGoal.Instance.CollectCrystal();
                }
            }
        }
    }
    void CheckEnemyAtCurrentPosition()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.25f);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                PlayerHealth playerHealth = GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage();
                }
            }
        }
    }
}