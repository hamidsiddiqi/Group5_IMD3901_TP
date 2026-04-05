using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 5f;
    public Camera playerCamera;
    public CrosshairUI crosshairUIScript;
    public inHand hand;
    public GameObject playerHand;
    public WrapObject wrap;
    string[] foodTags = { "fries", "lettuce", "tomatoes", "onions", "pickle", "wrap", "flatwrap", "chicken", "beef" };

    void Start()
    {

    }

    PaniniGrill GetNearestGrill()
    {
        PaniniGrill[] grills = FindObjectsByType<PaniniGrill>(FindObjectsSortMode.None);
        PaniniGrill nearest = null;
        float closestDist = Mathf.Infinity;

        foreach (PaniniGrill grill in grills)
        {
            float dist = Vector3.Distance(transform.position, grill.transform.position);
            if (dist < closestDist && dist <= 3f) // max 3 units away
            {
                closestDist = dist;
                nearest = grill;
            }
        }
        return nearest;
    }

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (Physics.Raycast(ray, out hit, interactRange))
            {
                if (hit.collider.CompareTag("customer"))
                {
                    if (hit.collider.GetComponent<CustomerMovement>().gaveOrder == false)
                    {
                        hit.collider.GetComponent<CustomerMovement>().getOrder();
                    }
                    else
                    {
                        if (hand.isHolding)
                        {
                            if (hand.objInHand.tag == "wrap")
                            {
                                hit.collider.GetComponent<CustomerMovement>().giveOrder(hand.objInHand);
                                hand.objInHand.SetActive(false);
                                Destroy(hand.objInHand);
                                hand.objInHand=null;
                                hand.isHolding = false;
                            }
                        }
                    }
                }
            }

            if (hand.isHolding)
            {
                WrapObject isWrap = hand.objInHand.GetComponent<WrapObject>();

                // only place on grill if it's an uncooked wrap
                if (isWrap != null && !isWrap.isCooked && hand.objInHand.CompareTag("wrap"))
                {
                    PaniniGrill grill = GetNearestGrill();
                    if (grill != null && !grill.isCooking)
                    {
                        GameObject wrapToPlace = hand.objInHand;

                        // clear hand first so inHand.Update() stops moving it
                        hand.objInHand = null;
                        hand.isHolding = false;

                        // unparent and kill physics
                        wrapToPlace.transform.SetParent(null);
                        Rigidbody rb = wrapToPlace.GetComponent<Rigidbody>();
                        rb.isKinematic = true;
                        rb.linearVelocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;

                        // place just above the bottom plate
                        wrapToPlace.transform.position = new Vector3(
                            grill.bottomPlate.position.x,
                            grill.bottomPlate.position.y + 0.1f,
                            grill.bottomPlate.position.z
                        );
                        wrapToPlace.transform.rotation = Quaternion.Euler(0.014f, 181.611f, -90.514f);

                        grill.currentWrap = wrapToPlace;
                        Debug.Log("Wrap placed on grill!");
                        return;
                    }
                }
                hand.dropObj();
            }
            else
            {
                // check if there's a cooked wrap on the nearest grill first
                PaniniGrill nearbyGrill = GetNearestGrill();
                if (nearbyGrill != null && nearbyGrill.isCooked)
                {
                    float distToGrill = Vector3.Distance(transform.position, nearbyGrill.transform.position);
                    if (distToGrill <= interactRange)
                    {
                        GameObject cookedWrap = nearbyGrill.TakeWrap();
                        if (cookedWrap != null)
                        {
                            hand.pickUpObj(cookedWrap);
                            Debug.Log("Picked up cooked wrap!");
                            return;
                        }
                    }
                }

                //start grilling if wrap is on grill
                PaniniGrill grillToStart = GetNearestGrill();
                if (grillToStart != null && grillToStart.currentWrap != null && !grillToStart.isCooking)
                {
                    GrillButton button = grillToStart.GetComponentInChildren<GrillButton>();
                    if (button != null) button.Press();
                    else grillToStart.TryStartGrilling();
                    return;
                }

                // otherwise normal raycast pickup
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    Debug.Log("Raycast hit: " + hit.collider.gameObject.name + " tag: " + hit.collider.tag);
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        hand.pickUpObj(hit.collider.gameObject);
                    }
                    //hitting the knife and scoop then pick that up
                    else if (hit.collider.CompareTag("knife"))
                    {
                        hand.pickUpObj(hit.collider.gameObject);
                    }
                    else
                    {
                        for (int i=0; i < foodTags.Length; i++)
                        {
                            if (hit.collider.CompareTag(foodTags[i]))
                            {
                                Debug.Log("grab it");
                                hand.pickUpObj(hit.collider.gameObject);
                            }
                        }
                    }
                }
            }
        }
    }

}