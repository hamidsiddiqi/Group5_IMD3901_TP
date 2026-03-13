using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class VRPlayerInteractions : MonoBehaviour
{
    public InputActionProperty grabButtonAction;
    public GameObject hand;
    public pickIngredient pickIng;
    public XRDirectInteractor handInteract;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("anything?");
        if(collider.gameObject.tag == "Container")
        {
            Debug.Log("collide with container");
            if (handInteract.hasSelection == false)
            {
                Debug.Log("nothing in hand");
                if (grabButtonAction.action.IsPressed())
                {
                    pickIng.grabIngredient(collider.gameObject, hand, 0);
                    Debug.Log("grab food");
                }
            }
        }
    }
}
