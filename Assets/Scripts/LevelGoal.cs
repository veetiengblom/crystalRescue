using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    public static LevelGoal Instance;

    private int totalCrystals;
    private int collectedCrystals;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        totalCrystals = GameObject.FindGameObjectsWithTag("CrystalRock").Length;
        collectedCrystals = 0;

        Debug.Log("Crystals: " + collectedCrystals + " / " + totalCrystals);
    }

    public void CollectCrystal()
    {
        collectedCrystals++;

        Debug.Log("Crystals: " + collectedCrystals + " / " + totalCrystals);

        if (collectedCrystals >= totalCrystals)
        {
            Debug.Log("LEVEL COMPLETE! All crystals collected.");
        }
    }
}