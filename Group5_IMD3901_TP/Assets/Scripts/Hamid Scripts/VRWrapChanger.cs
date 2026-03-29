//using UnityEngine;
//using UnityEngine.InputSystem;

//public class VRWrapChanger : MonoBehaviour
//{
//    [Header("Detection Settings")]
//    public Transform flatPita;        // The pita bread on the table
//    public float detectionRadius = 0.5f; // How close ingredients must be to count

//    [Header("Ingredients to Check")]
//    // Drag all your possible toppings (pickles, fries, etc.) here
//    public GameObject[] allToppings;

//    [Header("Wrapped Result")]
//    public GameObject wrappedShawarmaPrefab; // Your wrapped prefab
//    public Transform spawnLocation;          // Where the wrap appears

//    void Start()
//    {
//        // Ensure the wrapped version is hidden at the very start
//        if (wrappedShawarmaPrefab.scene.name != null)
//        {
//            wrappedShawarmaPrefab.SetActive(false);
//        }
//    }

//    void Update()
//    {
//        if (Keyboard.current.uKey.wasPressedThisFrame)
//        {
//            WrapPlacedIngredients();
//        }
//    }

//    void WrapPlacedIngredients()
//    {
//        bool ingredientPlaced = false;

//        foreach (GameObject topping in allToppings)
//        {
//            if (topping != null && topping.activeSelf)
//            {
//                // Check distance between this topping and the pita
//                float distance = Vector3.Distance(topping.transform.position, flatPita.position);

//                if (distance <= detectionRadius)
//                {
//                    // It's on the pita! Hide it.
//                    topping.SetActive(false);
//                    ingredientPlaced = true;
//                }
//            }
//        }

//        // If we found ingredients on the pita, hide the pita and show the wrap
//        if (ingredientPlaced)
//        {
//            flatPita.gameObject.SetActive(false);

//            // Spawn the wrap (or just activate it if it's already in the scene)
//            if (wrappedShawarmaPrefab != null)
//            {
//                GameObject wrap = Instantiate(wrappedShawarmaPrefab, spawnLocation.position, spawnLocation.rotation);
//                wrap.SetActive(true);
//            }

//            Debug.Log("Shawarma wrapped successfully!");
//        }
//        else
//        {
//            Debug.Log("No ingredients found on the pita to wrap!");
//        }
//    }
//}

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