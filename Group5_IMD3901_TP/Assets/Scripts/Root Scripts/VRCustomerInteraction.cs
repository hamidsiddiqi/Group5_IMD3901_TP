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
        //1 key to get customer order in VR
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactRange))
            {
                if (hit.collider.CompareTag("customer"))
                {
                    CustomerMovement customer = hit.collider.GetComponent<CustomerMovement>();
                    if (customer != null)
                    {
                        if (!customer.gaveOrder)
                            customer.getOrder();
                        else
                        {
                            inHand hand = FindObjectsByType<inHand>(FindObjectsSortMode.None)[0];
                            if (hand != null && hand.isHolding && hand.objInHand.CompareTag("wrap"))
                            {
                                customer.giveOrder(hand.objInHand);
                                hand.objInHand.SetActive(false);
                                Destroy(hand.objInHand);
                                hand.objInHand = null;
                                hand.isHolding = false;
                            }
                        }
                    }
                }
            }
        }
    }
}