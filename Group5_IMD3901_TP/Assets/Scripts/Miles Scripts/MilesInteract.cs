using UnityEngine;
using UnityEngine.InputSystem;

public class MilesInteract : MonoBehaviour
{

    public float interactRange = 5f;
    public Camera playerCamera;
    public CrosshairUI crosshairUIScript;
    public milesInHand hand;

    public milesPickIngredent ingredient;

    public GameObject knifeScoop;

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
                hand.dropObj();
                Debug.Log("drop");
            }
            else
            {
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    Debug.Log(hit.collider.gameObject.name);

                    if (hit.collider.CompareTag("Interactable"))
                    {
                        hand.pickUpObj(hit.collider.gameObject);
                    }
                    else if (hit.collider.CompareTag("Container"))
                    {
                        ingredient.grabIngredient(hit.collider.gameObject);
                    }
                    
                }
            }

        }

    }
}
