using System.Collections;
using UnityEngine;

public class MiningCharge : MonoBehaviour
{
    [Header("Explosion Settings")]
    public GameObject explosionTilePrefab;
    public float explosionDelay = 2f;
    public float explosionDuration = 0.35f;
    public int explosionRange = 1;
    public float tileSize = 1f;
    public float checkRadius = 0.25f;

    private PlayerChargePlacer owner;

    public void SetOwner(PlayerChargePlacer player)
    {
        owner = player;
    }

    void Start()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(explosionDelay);

        CreateExplosion();

        if (owner != null)
        {
            owner.ChargeExploded();
        }

        Destroy(gameObject);
    }

    void CreateExplosion()
    {
        // Center explosion always appears where the charge is
        CreateExplosionTile(transform.position);

        Vector2[] directions =
        {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right
        };

        foreach (Vector2 direction in directions)
        {
            for (int i = 1; i <= explosionRange; i++)
            {
                Vector3 explosionPosition = transform.position + (Vector3)(direction * tileSize * i);

                // Do not create explosion on walls
                if (HasObjectWithTag(explosionPosition, "Wall"))
                {
                    break;
                }

                CreateExplosionTile(explosionPosition);

                // Explosion destroys rocks, but does not continue past them
                if (HasObjectWithTag(explosionPosition, "Rock") || HasObjectWithTag(explosionPosition, "CrystalRock"))
                {
                    break;
                }
            }
        }
    }

    void CreateExplosionTile(Vector3 position)
    {
        GameObject explosionTile = Instantiate(explosionTilePrefab, position, Quaternion.identity);
        Destroy(explosionTile, explosionDuration);
    }

    bool HasObjectWithTag(Vector3 position, string tagName)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, checkRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag(tagName))
            {
                return true;
            }
        }

        return false;
    }
}