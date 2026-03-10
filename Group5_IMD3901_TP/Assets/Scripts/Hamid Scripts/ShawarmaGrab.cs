using UnityEngine;
using UnityEngine.InputSystem;

public class ShawarmaGrab : MonoBehaviour
{
    public GameObject shawarmaFlat;
    public GameObject shawarmaReady;

    public Transform player; // Drag your Player object here
    public float wrapDistance = 3f;

    void Start()
    {
        // Initial state: Flat is visible, Ready is hidden
        if (shawarmaFlat != null) shawarmaFlat.SetActive(true);
        if (shawarmaReady != null) shawarmaReady.SetActive(false);
    }

    void Update()
    {
        // Check for Spacebar press
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // Check if player is close enough to the table/shawarma
            float distance = Vector3.Distance(player.position, transform.position);

            if (distance <= wrapDistance)
            {
                WrapShawarma();
            }
        }
    }

    void WrapShawarma()
    {
        if (shawarmaFlat.activeSelf) // Only wrap if it hasn't been wrapped yet
        {
            Debug.Log("Shawarma Wrapped!");
            shawarmaFlat.SetActive(false);
            shawarmaReady.SetActive(true);
        }
    }
}