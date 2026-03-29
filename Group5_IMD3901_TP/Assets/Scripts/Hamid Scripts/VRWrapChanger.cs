using UnityEngine;
using UnityEngine.InputSystem;

public class VRWrapChanger : MonoBehaviour
{
    [Header("Detection Settings")]
    public WrapObject flatPitaScript; // Drag the Pita object that has WrapObject.cs here

    [Header("Wrapped Result")]
    public GameObject wrappedShawarmaPrefab;
    public Transform spawnLocation;

    void Start()
    {
        if (wrappedShawarmaPrefab != null && wrappedShawarmaPrefab.scene.name != null)
        {
            wrappedShawarmaPrefab.SetActive(false);
        }
    }

    void Update()
    {
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            WrapPlacedIngredients();
        }
    }

    void WrapPlacedIngredients()
    {
        if (flatPitaScript == null)
        {
            Debug.LogError("Please drag a Pita with the WrapObject script into the Flat Pita Script slot!");
            return;
        }

        // Check the integer counts inside WrapObject to see if anything was added
        bool hasIngredients = (flatPitaScript.onions > 0 || flatPitaScript.fries > 0 ||
                               flatPitaScript.pickles > 0 || flatPitaScript.lettuce > 0 ||
                               flatPitaScript.tomatoes > 0 || flatPitaScript.chicken > 0 ||
                               flatPitaScript.beef > 0 || flatPitaScript.garlic > 0 ||
                               flatPitaScript.hotSauce > 0);

        if (hasIngredients)
        {
            // Hide the pita (this hides all the parented ingredients too!)
            flatPitaScript.gameObject.SetActive(false);

            if (wrappedShawarmaPrefab != null)
            {
                // Spawn the wrap at the pita's current position
                Instantiate(wrappedShawarmaPrefab, flatPitaScript.transform.position, flatPitaScript.transform.rotation);
            }

            Debug.Log("Shawarma wrapped successfully based on WrapObject data!");
        }
        else
        {
            Debug.Log("WrapObject reports 0 ingredients on this pita.");
        }
    }
}