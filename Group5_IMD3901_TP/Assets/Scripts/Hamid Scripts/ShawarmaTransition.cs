using UnityEngine;
using UnityEngine.InputSystem;

public class ShawarmaTransition : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float interactDistance = 3f;

    public CharacterController controller;
    public Transform cameraTransform;

    public Transform shawarma;
    public Transform holdPoint;

    float xRotation = 0f;
    private bool isHolding = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("Scene has started!");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Scene is updating!");

        Vector2 moveInput = Keyboard.current != null ? new Vector2
            (
                (Keyboard.current.aKey.isPressed ? -1 : 0) + (Keyboard.current.dKey.isPressed ? 1 : 0),
                (Keyboard.current.sKey.isPressed ? -1 : 0) + (Keyboard.current.wKey.isPressed ? 1 : 0)
            ) : Vector2.zero;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);


        
        
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("E was pressed!"); // Check the Console for this!

            if (!isHolding)
            {
                float distance = Vector3.Distance(transform.position, shawarma.position);
                Debug.Log("Distance to Shawarma: " + distance); // This tells us if you're close enough

                if (distance <= interactDistance)
                {
                    GrabShawarma();
                }
                else
                {
                    Debug.Log("Too far away! Increase Interact Distance.");
                }
            }
            else
            {
                DropShawarma();
            }
        }




    }

    //void GrabShawarma()
    //{
    //    isHolding = true;
    //    // Parent the shawarma to the hold point
    //    shawarma.SetParent(holdPoint);
    //    // Reset local position/rotation so it sits perfectly in the hold point
    //    shawarma.localPosition = Vector3.zero;
    //    shawarma.localRotation = Quaternion.identity;

    //    // If your shawarma has a Rigidbody, disable gravity so it doesn't fall while held
    //    if (shawarma.GetComponent<Rigidbody>())
    //    {
    //        shawarma.GetComponent<Rigidbody>().isKinematic = true;
    //    }
    //}

    void GrabShawarma()
    {
        isHolding = true;
        shawarma.SetParent(holdPoint);

        // Instead of Vector3.zero, let's give it a slight offset 
        // so it sits nicely in the bottom right of the screen
        shawarma.localPosition = new Vector3(0f, 0.2f, 1.8f);
        shawarma.localRotation = Quaternion.Euler(90, 0, 0); // Rotates it to look side-ways

        if (shawarma.GetComponent<Rigidbody>())
        {
            shawarma.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void DropShawarma()
    {
        isHolding = false;
        shawarma.SetParent(null); // Unparent

        if (shawarma.GetComponent<Rigidbody>())
        {
            shawarma.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}

