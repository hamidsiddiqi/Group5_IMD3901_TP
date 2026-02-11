using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    public float interactRange = 5f;
    public Camera playerCamera;
    public CrosshairUI crosshairUIScript;
    public inHand hand;

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
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        hand.pickUpObj(hit.collider.gameObject);
                    }
                }
            }

        }

    }
}
