using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float tileSize = 1f;
    public float moveInterval = 1f;
    public LayerMask obstacleLayer;

    [Header("Player Detection")]
    public float checkRadius = 0.25f;

    private bool isMoving = false;

    void Start()
    {
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveInterval);

            TryRandomMove();
            CheckForPlayer();
        }
    }

    void TryRandomMove()
    {
        if (isMoving)
        {
            return;
        }

        Vector2[] directions =
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        int randomIndex = Random.Range(0, directions.Length);
        Vector2 direction = directions[randomIndex];

        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = currentPosition + direction * tileSize;

        bool blocked = Physics2D.OverlapCircle(targetPosition, 0.2f, obstacleLayer);

        if (!blocked)
        {
            transform.position = targetPosition;
        }
    }

    void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Debug.Log("Enemy touched player! Restarting level.");

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}