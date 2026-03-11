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
        //makes a ray from camera forward
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        //did you hit the e key
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            //checks if there is smth in the hand
            if (hand.isHolding)
            {
                //see what our ray cast is hitting
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    //if we are hitting the customer we need to see if we have the wrap
                    if (hit.collider.CompareTag("customer"))
                    {
                        //checks if there is a wrap script
                        WrapObject isWrap = hand.objInHand.GetComponent<WrapObject>();                       
                        if (isWrap != null)
                        {
                            //give shawarma to customer
                            hand.GiveShawarma();
                            return;
                        }
                    }
                }

                //if you arent hitting the customer and holding a wrap then drop item
                hand.dropObj();
            }

            //if you arentt holding somethingg
            else
            {
                //check what our raycast is hitting
                if (Physics.Raycast(ray, out hit, interactRange))
                {

                    //check for grill button
                    GrillButton button = hit.collider.GetComponent<GrillButton>();
                    if (button != null)
                    {
                        button.Press();
                        Debug.Log("Pressed grill button!");
                        return;
                    }

                    //hitting smth interactable then pick it up
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        hand.pickUpObj(hit.collider.gameObject);
                    }
                    //hitting a container make an instance of that food and grab it
                    else if (hit.collider.CompareTag("Container"))
                    {
                        ingredient.grabIngredient(hit.collider.gameObject);
                    }
                    //hitting the knife and scoop then pick that up
                    else if (hit.collider.CompareTag("scooper"))
                    {
                        hand.pickUpObj(hit.collider.gameObject);
                    }
                }
            }

        }

    }
}
