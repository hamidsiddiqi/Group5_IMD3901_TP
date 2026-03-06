using System.Collections;
using UnityEngine;

public class PaniniGrill : MonoBehaviour
{
    public Transform topPlate;
    public Transform bottomPlate;
    public float cookTime = 4f;
    public Material cookedMaterial;
    public AudioClip sizzleSound;
    public inHand playerHand; // reference to player's inHand

    private GameObject currentWrap;
    private bool isCooking = false;
    private Vector3 topPlateOpenPos;
    private Vector3 topPlateClosedPos;

    void Start()
    {
        topPlateOpenPos = topPlate.position;
        topPlateClosedPos = topPlate.position - new Vector3(0, 0.15f, 0);
    }

    void Update()
    {

        if (playerHand.isHolding)
        {
            if (playerHand.rightHand.transform.childCount > 0)
            {
                float dist = Vector3.Distance(
                    playerHand.rightHand.transform.position,
                    transform.position
                );
                //Debug.Log("Distance to grill: " + dist); 
            }
        }
        // Check if player is holding wrap near grill
        if (!isCooking && playerHand.isHolding)
        {
            // Check if held object is a wrap
            if (playerHand.rightHand.transform.childCount > 0)
            {
                GameObject heldObj = playerHand.rightHand.transform.GetChild(0).gameObject;
                WrapObject wrap = heldObj.GetComponent<WrapObject>();

                // Check if hand is close to grill
                float dist = Vector3.Distance(
                    playerHand.rightHand.transform.position,
                    transform.position
                );

                if (wrap != null && dist < 2.5f)
                {
                    // Auto drop from hand and place on grill
                    playerHand.dropObj();
                    currentWrap = heldObj;

                    // Snap to grill center
                    currentWrap.transform.position = new Vector3(
                    bottomPlate.position.x,
                    bottomPlate.position.y + 0.1f,
                    bottomPlate.position.z
                    );

                    // Disable physics
                    Rigidbody rb = currentWrap.GetComponent<Rigidbody>();
                    if (rb != null) rb.isKinematic = true;

                    StartCoroutine(CookWrap());
                }
            }
        }
    }

    IEnumerator CookWrap()
    {
        isCooking = true;

        // Close grill animation
        float t = 0f;
        while (t < 1f)
        {
            topPlate.position = Vector3.Lerp(
                topPlateOpenPos,
                topPlateClosedPos,
                t
            );
            t += Time.deltaTime * 2f;
            yield return null;
        }
        topPlate.position = topPlateClosedPos;

        // Play sizzle sound
        if (sizzleSound != null)
            AudioSource.PlayClipAtPoint(sizzleSound, transform.position);

        // Wait while cooking
        yield return new WaitForSeconds(cookTime);

        // Change to cooked material
        Renderer rend = currentWrap.GetComponent<Renderer>();
        if (rend != null && cookedMaterial != null)
            rend.material = cookedMaterial;

        // Open grill animation
        t = 0f;
        while (t < 1f)
        {
            topPlate.position = Vector3.Lerp(
                topPlateClosedPos,
                topPlateOpenPos,
                t
            );
            t += Time.deltaTime * 2f;
            yield return null;
        }
        topPlate.position = topPlateOpenPos;

        // Re-enable physics
        Rigidbody rb = currentWrap.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Debug.Log("Wrap is cooked!");
        isCooking = false;
    }
}