using UnityEngine;
using UnityEngine.InputSystem;

public class ShawarmaVRTransition : MonoBehaviour
{
    //public float speed = 5f;
    //public float mouseSensitivity = 2f;
    public float interactDistance = 3f;

    //public CharacterController controller;
    public Transform cameraTransform;

    public Transform shawarma;
    public Transform holdPoint;
    public Transform customer; // Drag the Customer Capsule here

    float xRotation = 0f;
    private bool isHolding = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //// --- MOVEMENT & LOOKING (Keep your existing code here) ---
        //Vector2 moveInput = Keyboard.current != null ? new Vector2
        //    (
        //        (Keyboard.current.aKey.isPressed ? -1 : 0) + (Keyboard.current.dKey.isPressed ? 1 : 0),
        //        (Keyboard.current.sKey.isPressed ? -1 : 0) + (Keyboard.current.wKey.isPressed ? 1 : 0)
        //    ) : Vector2.zero;

        //Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        //controller.Move(move * speed * Time.deltaTime);

        //Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        //float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        //float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        //cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.Rotate(Vector3.up * mouseX);

        // --- INTERACTION LOGIC ---
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (!isHolding)
            {
                float distToShawarma = Vector3.Distance(transform.position, shawarma.position);
                if (distToShawarma <= interactDistance)
                {
                    GrabShawarma();
                }
            }
            else
            {
                // Check if we are near the customer to give it to them
                float distToCustomer = Vector3.Distance(transform.position, customer.position);

                if (distToCustomer <= interactDistance)
                {
                    GiveShawarma();
                }
                else
                {
                    DropShawarma();
                }
            }
        }
    }

    void GrabShawarma()
    {
        isHolding = true;
        shawarma.SetParent(holdPoint);
        shawarma.localPosition = new Vector3(0f, 0.2f, 1.8f);
        shawarma.localRotation = Quaternion.Euler(90, 0, 0);

        if (shawarma.GetComponent<Rigidbody>())
        {
            shawarma.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void GiveShawarma()
    {
        Debug.Log("Shawarma delivered to customer!");
        isHolding = false;

        // Parent it to the customer so they "hold" it
        shawarma.SetParent(customer);

        // Position it in front of the customer capsule
        shawarma.localPosition = new Vector3(0f, 0.5f, 0.6f);
        shawarma.localRotation = Quaternion.identity;

        // Keep it kinematic so it doesn't fall off the customer
        if (shawarma.GetComponent<Rigidbody>())
        {
            shawarma.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void DropShawarma()
    {
        isHolding = false;
        shawarma.SetParent(null);
        if (shawarma.GetComponent<Rigidbody>())
        {
            shawarma.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
