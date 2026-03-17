using System.Collections;
using UnityEngine;

public class PaniniGrill : MonoBehaviour
{
    public Transform topPlate;
    public Transform bottomPlate;
    public float cookTime = 4f;
    public Material cookedMaterial;
    public AudioClip sizzleSound;

    public GameObject currentWrap;
    public bool isCooking = false;
    public bool isCooked = false;
    private Vector3 topPlateOpenPos;
    private Vector3 topPlateClosedPos;

    void Start()
    {
        topPlateOpenPos = topPlate.position;
        topPlateClosedPos = topPlate.position - new Vector3(0, 0.15f, 0);
    }

    public void TryStartGrilling()
    {
        if (currentWrap != null && !isCooking)
        {
            Debug.Log("Starting to grill...");
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

    // called by PlayerInteraction when player presses E near grill with cooked wrap
    public GameObject TakeWrap()
    {
        if (currentWrap != null && isCooked)
        {
            GameObject wrap = currentWrap;

            // unparent from grill
            wrap.transform.SetParent(null);

            // re-enable physics
            Rigidbody rb = wrap.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            currentWrap = null;
            isCooked = false;
            return wrap;
        }
        return null;
    }

    IEnumerator CookWrap()
    {
        isCooking = true;
        isCooked = false;

        // close grill animation
        float t = 0f;
        while (t < 1f)
        {
            topPlate.position = Vector3.Lerp(topPlateOpenPos, topPlateClosedPos, t);
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
            topPlate.position = Vector3.Lerp(topPlateClosedPos, topPlateOpenPos, t);
            t += Time.deltaTime * 2f;
            yield return null;
        }
        topPlate.position = topPlateOpenPos;

        // mark wrap as cooked on WrapObject
        WrapObject wrapObj = currentWrap.GetComponent<WrapObject>();
        if (wrapObj != null)
            wrapObj.isCooked = true;

        // parent wrap to grill so it stays perfectly still, no physics fighting
        currentWrap.transform.SetParent(transform);
        Rigidbody wrb = currentWrap.GetComponent<Rigidbody>();
        if (wrb != null)
            wrb.isKinematic = true;

        isCooked = true;
        isCooking = false;
        Debug.Log("Wrap is cooked! Press E near grill to pick it up.");
    }
}