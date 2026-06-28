using UnityEngine;

public class PlayerChargePlacer : MonoBehaviour
{
    [Header("Charge Settings")]
    public GameObject miningChargePrefab;
    public float tileSize = 1f;

    private bool hasActiveCharge = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceCharge();
        }
    }

    void PlaceCharge()
    {
        if (hasActiveCharge)
        {
            return;
        }
        
        Vector3 chargePosition = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            0f
        );

        GameObject newCharge = Instantiate(miningChargePrefab, chargePosition, Quaternion.identity);

        MiningCharge chargeScript = newCharge.GetComponent<MiningCharge>();
        chargeScript.SetOwner(this);

        hasActiveCharge = true;
    }

    public void ChargeExploded()
    {
        hasActiveCharge = false;
    }
}