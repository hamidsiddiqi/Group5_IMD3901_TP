using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRSauceBottle : MonoBehaviour
{
    public SauceBottle sauceBottle;
    public float interactRange = 7f;
    public InputActionReference triggerAction; // drag in the trigger input action

    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        GetComponent<Rigidbody>().isKinematic = true; // make the bottle kinematic while held
        Debug.Log("Bottle grabbed!");
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        transform.localRotation = Quaternion.Euler(90f, 0f, 0f); // reset rotation on release
        Debug.Log("Bottle released!");
    }

    void Update()
    {
        if (!isGrabbed) return;

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            Debug.Log("Ray direction: " + Vector3.down);
            int layerMask = ~(1 << LayerMask.NameToLayer("SauceSplat"));
            Ray ray = new Ray(transform.position + Vector3.down * 0.5f, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactRange, layerMask, QueryTriggerInteraction.Ignore))
            {

                //ignore if we hit the bottle itself
                if (hit.collider.gameObject == gameObject || hit.collider.transform.IsChildOf(transform))
                {
                    Debug.Log("Hit self, ignoring...");
                    return;
                }

                GameObject target = hit.collider.gameObject;
                if (!target.CompareTag("flatwrap") && target.transform.parent != null)
                    target = target.transform.parent.gameObject;

                if (target.CompareTag("flatwrap"))
                {
                    Debug.Log("Hit point Y: " + hit.point.y + " target: " + target.name);
                    sauceBottle.ApplySauce(target, hit);
                    Debug.Log("VR Sauce applied!");
                }
                else
                {
                    Debug.Log("VR Raycast hit: " + hit.collider.gameObject.name);
                }
            }
            else
            {
                Debug.Log("VR Raycast hit nothing!");
            }
        }
    }
}
    
