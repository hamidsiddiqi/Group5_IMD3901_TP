using UnityEngine;
using UnityEngine.InputSystem;

public class UseItem : MonoBehaviour
{
    public inHand hand;
    public Camera playerCamera;
    public float interactRange = 5f;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Left click detected, isHolding: " + hand.isHolding);
            if (hand.isHolding)
            {
                Debug.Log("Child count: " + hand.hand.transform.childCount);
                if (hand.hand.transform.childCount > 0)
                {
                    GameObject heldObj = hand.hand.transform.GetChild(0).gameObject;
                    SauceBottle heldSauce = heldObj.GetComponent<SauceBottle>();

                    if (heldSauce != null)
                    {
                        // Build a layermask that ignores whatever layer the held object is on
                        int heldLayer = heldObj.layer;
                        int layerMask = ~(1 << heldLayer);

                        Ray ray = new Ray(playerCamera.transform.position,
                                          playerCamera.transform.forward);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, interactRange, layerMask))
                        {
                            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                            GameObject target = hit.collider.gameObject;
                            if (!target.CompareTag("flatwrap") && target.transform.parent != null)
                            {
                                target = target.transform.parent.gameObject;
                            }

                            if (target.CompareTag("flatwrap"))
                            {
                                heldSauce.ApplySauce(target, hit);
                                Debug.Log("Sauce squirted onto flat wrap!");
                            }
                            else
                            {
                                Debug.Log("Not looking at flat wrap! Hit: " + hit.collider.gameObject.name);
                            }
                        }
                        else
                        {
                            Debug.Log("Raycast hit nothing!");
                        }
                    }
                }
            }
        }
    }
}