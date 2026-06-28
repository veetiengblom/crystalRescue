using System.Collections;
using UnityEngine;

public class EnemyChargePlacer : MonoBehaviour
{
    [Header("Enemy Charge Settings")]
    public GameObject miningChargePrefab;
    public float placeCheckInterval = 2.5f;
    public float placeChance = 0.35f;

    private bool hasActiveCharge = false;

    void Start()
    {
        StartCoroutine(ChargeLoop());
    }

    IEnumerator ChargeLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(placeCheckInterval);

            float randomValue = Random.Range(0f, 1f);

            if (randomValue <= placeChance && !hasActiveCharge)
            {
                PlaceCharge();
            }
        }
    }

    void PlaceCharge()
    {
        if (miningChargePrefab == null)
        {
            Debug.LogError("Enemy miningChargePrefab is missing!");
            return;
        }

        Vector3 chargePosition = new Vector3(
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            0f
        );

        Instantiate(miningChargePrefab, chargePosition, Quaternion.identity);

        Debug.Log("Enemy placed a mining charge!");

        hasActiveCharge = true;

        StartCoroutine(ResetActiveCharge());
    }

    IEnumerator ResetActiveCharge()
    {
        yield return new WaitForSeconds(2.5f);
        hasActiveCharge = false;
    }
}