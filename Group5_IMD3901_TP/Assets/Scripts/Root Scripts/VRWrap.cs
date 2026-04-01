using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRWrap : MonoBehaviour
{
    public float grillDistance = 5f;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void Update()
    {
        // press t to start grilling when wrap is on grill (not held)
        if (Keyboard.current.tKey.wasPressedThisFrame && !isGrabbed)
        {
            PaniniGrill grill = FindObjectsByType<PaniniGrill>(FindObjectsSortMode.None)[0];
            if (grill != null && grill.currentWrap != null && !grill.isCooking)
            {
                float dist = Vector3.Distance(transform.position, grill.transform.position);
                if (dist <= grillDistance)
                {
                    GrillButton button = grill.GetComponentInChildren<GrillButton>();
                    if (button != null) button.Press();
                    else grill.TryStartGrilling();
                    Debug.Log("VR: Grilling started!");
                }
            }
        }

        if (!isGrabbed) return;

        // press 2 to place wrap on grill while holding it
        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            PaniniGrill grill = FindObjectsByType<PaniniGrill>(FindObjectsSortMode.None)[0];
            if (grill != null && !grill.isCooking)
            {
                float dist = Vector3.Distance(transform.position, grill.transform.position);
                Debug.Log("Distance to grill: " + dist);

                if (dist <= grillDistance)
                {
                    // release from hand
                    grabInteractable.interactionManager.CancelInteractableSelection((UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable)grabInteractable);

                    // snap to grill center
                    transform.position = new Vector3(
                        grill.bottomPlate.position.x,
                        grill.bottomPlate.position.y + 0.1f,
                        grill.bottomPlate.position.z
                    );
                    transform.rotation = Quaternion.Euler(0.014f, 91.611f, -90.514f);
                    GetComponent<Rigidbody>().isKinematic = true;
                    grill.currentWrap = gameObject;
                    Debug.Log("VR: Wrap placed on grill!");
                }
            }
        }
    }
}