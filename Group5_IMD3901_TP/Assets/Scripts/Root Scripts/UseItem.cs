using UnityEngine;
using UnityEngine.InputSystem;

public class UseItem : MonoBehaviour
{
    public inHand hand; // reference to the inHand script that tracks what the player is holding
    public Camera playerCamera; // the player's camera, used to fire the raycast
    public float interactRange = 5f; // how far the raycast reaches

    void Update()
    {
        // check if the player left clicked
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // check if the player is holding something
            if (hand.isHolding)
            {
                // check if there is actually an object in the hand
                if (hand.hand.transform.childCount > 0)
                {
                    // get the object in the hand
                    GameObject heldObj = hand.hand.transform.GetChild(0).gameObject;

                    // check if the held object is a sauce bottle
                    SauceBottle heldSauce = heldObj.GetComponent<SauceBottle>();

                    if (heldSauce != null)
                    {
                        // fire a ray from the camera forward
                        Ray ray = new Ray(playerCamera.transform.position,
                                          playerCamera.transform.forward);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, interactRange))
                        {
                            // if the ray hits the bottle itself, ignore it
                            // (the bottle is held in front of the camera so it would block the ray)
                            if (hit.collider.gameObject == heldObj ||
                                hit.collider.transform.IsChildOf(heldObj.transform))
                            {
                                return;
                            }

                            // get the object the ray hit
                            GameObject target = hit.collider.gameObject;

                            // if the hit object doesn't have the flatwrap tag, check its parent
                            // (in case we hit a child object of the flat wrap)
                            if (!target.CompareTag("flatwrap") && target.transform.parent != null)
                            {
                                target = target.transform.parent.gameObject;
                            }

                            // if we're looking at the flat wrap, apply the sauce
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
                    }
                }
            }
        }
    }
}