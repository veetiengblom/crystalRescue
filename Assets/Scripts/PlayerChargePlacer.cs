using UnityEngine;

public class PlayerChargePlacer : MonoBehaviour
{
    [Header("Charge Settings")]
    public GameObject miningChargePrefab;
    public float tileSize = 1f;

    private bool hasActiveCharge = false;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceCharge();
        }
    }

    void PlaceCharge()
    {
        if (playerHealth != null && playerHealth.IsInvulnerable)
        {
            Debug.Log("Cannot place charge while invulnerable.");
            return;
        }

        if (hasActiveCharge)
        {
            Debug.Log("Cannot place charge, already active.");
            return;
        }

        if (miningChargePrefab == null)
        {
            Debug.LogError("MiningCharge prefab is not assigned on PlayerChargePlacer!");
            return;
        }

        Vector3 chargePosition = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            0f
        );

        GameObject newCharge = Instantiate(miningChargePrefab, chargePosition, Quaternion.identity);

        MiningCharge chargeScript = newCharge.GetComponent<MiningCharge>();

        if (chargeScript != null)
        {
            chargeScript.SetOwner(this);
        }

        hasActiveCharge = true;
    }

    public void ChargeExploded()
    {
        hasActiveCharge = false;
    }
}