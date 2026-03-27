using UnityEngine;
using UnityEngine.InputSystem;

public class ShawarmaGrab : MonoBehaviour
{
    public GameObject shawarmaFlat;
    public GameObject shawarmaReady;

    public Transform player; // Drag your Player object here
    public float wrapDistance = 15f;

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
            Debug.Log("Space pressed! Distance: " + distance + " wrapDistance: " + wrapDistance);
            
            if (distance <= wrapDistance)
            {
                WrapShawarma();
            }
        }
    }

    void WrapShawarma()
    {
        if (shawarmaFlat.activeSelf)
        {
            Debug.Log("Shawarma Wrapped!");
        
            // spawn wrap at same position as flat wrap
            shawarmaReady.transform.position = shawarmaFlat.transform.position;
            shawarmaReady.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        
            shawarmaFlat.SetActive(false);
            shawarmaReady.SetActive(true);

            Rigidbody rb = shawarmaReady.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }
}