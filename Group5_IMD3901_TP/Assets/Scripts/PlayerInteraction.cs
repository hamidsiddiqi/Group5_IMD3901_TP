using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInteraction : MonoBehaviour
{

    public float interactRange = 5f;
    public Camera playerCamera;
    public CrosshairUI crosshairUIScript;
    public inHand hand;
    public pickIngredient ingredient;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (hand.isHolding)
            {
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    if (hit.collider.CompareTag("customer"))
                    {
                        Debug.Log("hit customer");
                        WrapObject isWrap = hand.objInHand.GetComponent<WrapObject>();
                        if (isWrap != null)
                        {
                            hand.GiveShawarma();
                            Debug.Log("is given");
                            return;
                        }
                    }
                }

                hand.dropObj();
                Debug.Log("drop");
            }
            else
            {
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        hand.pickUpObj(hit.collider.gameObject);
                    }
                    else if (hit.collider.CompareTag("Container"))
                    {
                        ingredient.grabIngredient(hit.collider.gameObject);
                    }
                    else if (hit.collider.CompareTag("customer"))
                    {
                        WrapObject isWrap = hand.GetComponent<WrapObject>();
                        if (isWrap != null)
                        {
                            hand.GiveShawarma();
                        }
                    }
                    else if (hit.collider.CompareTag("scooper"))
                    {
                        hand.pickUpObj(hit.collider.gameObject);
                    }
                }
            }

        }

    }
}
