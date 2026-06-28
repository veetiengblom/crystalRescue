using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int lives = 3;
    public float invulnerabilityTime = 1f;

    [Header("UI")]
    public TextMeshProUGUI livesText;

    private Vector3 respawnPosition;
    private bool isInvulnerable = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        respawnPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateLivesUI();
    }

    public void TakeDamage()
    {
        if (isInvulnerable)
        {
            return;
        }

        lives--;

        Debug.Log("Player damaged. Lives left: " + lives);

        UpdateLivesUI();

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
            return;
        }

        RespawnPlayer();
        StartCoroutine(TemporaryInvulnerability());
    }

    void RespawnPlayer()
    {
        transform.position = respawnPosition;
    }

    IEnumerator TemporaryInvulnerability()
    {
        isInvulnerable = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.gray;
        }

        yield return new WaitForSeconds(invulnerabilityTime);

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.blue;
        }

        isInvulnerable = false;
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }
}