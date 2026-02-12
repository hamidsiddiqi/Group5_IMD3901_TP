using UnityEngine;
using System.Collections; // Required for Coroutines

public class ShawarmaGrab : MonoBehaviour
{
    // Drag your objects from the Hierarchy into these slots
    public GameObject shawarmaFlat;
    public GameObject shawarmaReady;

    public float prepareTime = 12f; // Easy to change in the Inspector

    void Start()
    {
        // Set the initial state
        if (shawarmaFlat != null) shawarmaFlat.SetActive(true);
        if (shawarmaReady != null) shawarmaReady.SetActive(false);

        // Start the countdown
        StartCoroutine(PreparationTimer());
    }

    IEnumerator PreparationTimer()
    {
        Debug.Log("Cooking started...");

        // Wait for the specified seconds
        yield return new WaitForSeconds(prepareTime);

        // Swap the objects
        if (shawarmaFlat != null) shawarmaFlat.SetActive(false);
        if (shawarmaReady != null) shawarmaReady.SetActive(true);

        Debug.Log("Shawarma is ready!");
    }
}