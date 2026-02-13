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
            if (hand.isHolding)
            {
                Ray ray = new Ray(playerCamera.transform.position,
                                  playerCamera.transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    SauceBottle sauce = hit.collider.GetComponent<SauceBottle>();
                    if (sauce != null)
                    {
                        // Get the wrap from right hand
                        if (hand.rightHand.transform.childCount > 0)
                        {
                            GameObject heldObj = hand.rightHand.transform.GetChild(0).gameObject;
                            if (heldObj.GetComponent<WrapObject>() != null)
                            {
                                sauce.ApplySauce(heldObj);
                            }
                            else
                            {
                                Debug.Log("Held object is not a wrap!");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Not looking at a sauce bottle!");
                    }
                }
                else
                {
                    Debug.Log("Raycast hit nothing!");
                }
            }
            else
            {
                Debug.Log("Not holding anything!");
            }
        }
    }
}