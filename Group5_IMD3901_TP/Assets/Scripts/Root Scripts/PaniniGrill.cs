using System.Collections;
using UnityEngine;

public class PaniniGrill : MonoBehaviour
{
    public Transform topPlate;
    public Transform bottomPlate;
    public float cookTime = 4f;
    public Material cookedMaterial;
    public AudioSource sizzleSound;

    public GameObject currentWrap;
    public bool isCooking = false;
    public bool isCooked = false;
    private Quaternion topPlateOpenRot;
    private Quaternion topPlateClosedRot;

    void Start()
    {
        
        Debug.Log(topPlate.transform.localRotation);
        topPlateOpenRot = topPlate.transform.localRotation;
        //topPlateClosedRot = Quaternion.Euler(35f,270f,0f);
        topPlateClosedRot = Quaternion.Euler(30f, 0f, 0f);
    }

    public void TryStartGrilling()
    {
        Debug.Log("TryStartGrilling called. currentWrap: " + (currentWrap != null ? currentWrap.name : "null") + " isCooking: " + isCooking + " isCooked: " + isCooked);
        if (currentWrap != null && !isCooking && !isCooked)
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
        else if (isCooked)
        {
            Debug.Log("Wrap is already cooked, pick it up!");
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
            wrap.transform.localScale = new Vector3(0.2615956f, 0.5682309f, 0.1921981f); //reset scale

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
        if (currentWrap == null)
        {
            Debug.Log("No wrap to cook!");
            yield break;
        }
        isCooking = true;
        isCooked = false;

        // close grill animation
        float t = 0f;
        while (t < 1f)
        {
            
            topPlate.localRotation = Quaternion.Lerp(topPlateOpenRot, topPlateClosedRot, t);
            t += Time.deltaTime * 2f;
            yield return null;
        }
        topPlate.localRotation = Quaternion.Euler(30, 0, 0);

        // play sizzle sound
        if (sizzleSound != null)
            //AudioSource.PlayClipAtPoint(sizzleSound, transform.position);
            sizzleSound.Play();

        // wait while cooking
        yield return new WaitForSeconds(cookTime);

        // change to cooked material on all child renderers of the wrap prefab
        Renderer[] renderers = currentWrap.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            rend.material.color = rend.material.color * 0.8f;
        }

        // open grill animation
        t = 0f;
        while (t < 1f)
        {
            topPlate.localRotation = Quaternion.Lerp(topPlateClosedRot, topPlateOpenRot, t);
            t += Time.deltaTime * 2f;
            yield return null;
        }
        topPlate.localRotation = topPlateOpenRot;

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