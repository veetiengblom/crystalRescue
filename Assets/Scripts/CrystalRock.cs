using UnityEngine;

public class CrystalRock : MonoBehaviour
{
    public GameObject crystalPrefab;

    public void BreakRock()
    {
        Debug.Log("Crystal rock destroyed!");

        Instantiate(crystalPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}