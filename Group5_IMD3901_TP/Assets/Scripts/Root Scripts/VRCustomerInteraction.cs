using UnityEngine;
using UnityEngine.InputSystem;

public class VRCustomerInteraction : MonoBehaviour
{
    public Camera vrCamera;
    public float interactRange = 5f;

    void Start()
    {
        vrCamera = Camera.main;
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            GameObject heldWrap = GetHeldWrap();
            Debug.Log("Held wrap: " + (heldWrap != null ? heldWrap.name : "null"));

            Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
            RaycastHit hit;

            int layerMask = heldWrap != null ? ~(1 << heldWrap.layer) : Physics.DefaultRaycastLayers;

            if (Physics.Raycast(ray, out hit, interactRange, layerMask))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name + " tag: " + hit.collider.tag);

                if (hit.collider.CompareTag("customer"))
                {
                    CustomerMovement customer = hit.collider.GetComponent<CustomerMovement>();
                    if (customer != null)
                    {
                        if (!customer.gaveOrder)
                            customer.getOrder();
                        else
                        {
                            Debug.Log("Customer has given order, checking for held wrap...");
                            if (heldWrap != null)
                            {
                                Debug.Log("Found held wrap: " + heldWrap.name);
                                customer.giveOrder(heldWrap);
                                heldWrap.SetActive(false);
                                Destroy(heldWrap);
                            }
                            else
                            {
                                Debug.Log("No held wrap found to give to customer.");
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Raycast hit nothing");
            }
        }
    }

    GameObject GetHeldWrap()
    {
        var grabInteractables = FindObjectsByType<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>(FindObjectsSortMode.None);
        foreach (var grab in grabInteractables)
        {
            if (grab.isSelected && grab.CompareTag("wrap"))
                return grab.gameObject;
        }
        return null;
    }
}