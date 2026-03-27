using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.FilePathAttribute;

public class ShawarmaGrab : MonoBehaviour
{
    public GameObject shawarmaReady;

    public Transform player; // Drag your Player object here
    public float wrapDistance = 15f;

    void Start()
    {
        // Initial state: Flat is visible, Ready is hidden
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
        Debug.Log("Shawarma Wrapped!");

        // spawn wrap at same position as flat wrap
        GameObject wrap = Instantiate(shawarmaReady, this.transform.position, Quaternion.identity);
        wrap.transform.Rotate(0f,0f, -90f);
        wrap.SetActive(true);
        this.gameObject.SetActive(false);
        /*
        Rigidbody rb = shawarmaReady.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        */
        //Destroy(this.gameObject);
    }
}