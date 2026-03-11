using System.Collections;
using UnityEngine;

public class PaniniGrill : MonoBehaviour
{
    public Transform topPlate;
    public Transform bottomPlate;
    public float cookTime = 4f;
    public Material cookedMaterial;
    public AudioClip sizzleSound;

    private GameObject currentWrap;
    private bool isCooking = false;
    private Vector3 topPlateOpenPos;
    private Vector3 topPlateClosedPos;

    void Start()
    {
        topPlateOpenPos = topPlate.position;
        topPlateClosedPos = topPlate.position - new Vector3(0, 0.15f, 0);
    }

    // method to detect wrap being placed on grill
    void OnTriggerEnter(Collider other)
    {
        WrapObject wrap = other.GetComponent<WrapObject>();
        if (wrap != null && !isCooking && currentWrap == null)
        {
            currentWrap = other.gameObject;
            
            // snap to grill center
            currentWrap.transform.position = new Vector3(
                bottomPlate.position.x,
                bottomPlate.position.y + 0.1f,
                bottomPlate.position.z
            );
            
            // disable physics
            Rigidbody rb = currentWrap.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
            
            Debug.Log("Wrap placed on grill! Press button to cook.");
        }
    }

    // grilling method - called by button
    public void TryStartGrilling()
    {
        if (currentWrap != null && !isCooking)
        {
            Debug.Log("Button pressed! Starting to grill...");
            StartCoroutine(CookWrap());
        }
        else if (currentWrap == null)
        {
            Debug.Log("No wrap on grill!");
        }
        else if (isCooking)
        {
            Debug.Log("Already cooking!");
        }
    }

    IEnumerator CookWrap()
    {
        isCooking = true;

        // close grill animation
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

        // play sizzle sound
        if (sizzleSound != null)
            AudioSource.PlayClipAtPoint(sizzleSound, transform.position);

        // wait while cooking
        yield return new WaitForSeconds(cookTime);

        // change to cooked material
        Renderer rend = currentWrap.GetComponent<Renderer>();
        if (rend != null && cookedMaterial != null)
            rend.material = cookedMaterial;

        // open grill animation
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

        // re-enable physics
        Rigidbody rb = currentWrap.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Debug.Log("Wrap is cooked!");
        isCooking = false;
        currentWrap = null; // clear wrap reference
    }
}