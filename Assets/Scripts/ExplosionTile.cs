using UnityEngine;

public class ExplosionTile : MonoBehaviour
{
    public float checkRadius = 0.25f;

    void Start()
    {
        CheckForObjects();
    }

    void CheckForObjects()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("CrystalRock"))
            {
                CrystalRock crystalRock = hit.GetComponent<CrystalRock>();

                if (crystalRock != null)
                {
                    crystalRock.BreakRock();
                }
            }
            else if (hit.CompareTag("Rock"))
            {
                Debug.Log("Normal rock destroyed!");
                Destroy(hit.gameObject);
            }
            else if (hit.CompareTag("Enemy"))
            {
                Debug.Log("Enemy destroyed!");
                Destroy(hit.gameObject);
            }
            else if (hit.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage();
                }
            }
        }
    }
}