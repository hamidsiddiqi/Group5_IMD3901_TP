using System.Collections;
using UnityEngine;

public class PaniniGrill : MonoBehaviour

{
    public Transform topPlate; //top cube of grill
    public float cookTime = 4f;
    public Material rawMaterial;
    public Material cookedMaterial;
    public AudioClip sizzleSound;

    private GameObject currentWrap;
    private bool isCooking = false;
    private Vector3 topPlateOpenPos;
    private Vector3 topPlateClosedPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        topPlateOpenPos = topPlate.position;
        topPlateClosedPos = topPlate.position - new Vector3(0, 0.15f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        //when wrap is placed on grill
        if (other.CompareTag("Wrap") && !isCooking)
        {
            currentWrap = other.gameObject;

            //make warp stop moving (if it has Rigidbody)
            Rigidbody rb = currentWrap.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            //disable wrap's drag script if it has one
            DraggableObject drag = currentWrap.GetComponent<DraggableObject>();
            if (drag != null)
            {
                drag.enabled = false;
            }

            StartCoroutine(CookWrap());
        }
    }

    IEnumerator CookWrap()
    {
        isCooking = true;

        //close grill
        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            topPlate.position = Vector3.Lerp(topPlateOpenPos, topPlateClosedPos, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        topPlate.position = topPlateClosedPos;

        //play sizzle sound
        if (sizzleSound != null)
        {
            AudioSource.PlayClipAtPoint(sizzleSound, transform.position);
        }

        //wait while cooking
        yield return new WaitForSeconds(cookTime);

        //change wrap material to cooked
        Renderer renderer = currentWrap.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = cookedMaterial;
        }

        //open grill
        elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            topPlate.position = Vector3.Lerp(topPlateClosedPos, topPlateOpenPos, elapsedTime / 0.5f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        topPlate.position = topPlateOpenPos;

        //re-enable dragging on wrap
        DraggableObject drag = currentWrap.GetComponent<DraggableObject>();
        if (drag != null)
        {
            drag.enabled = true;
        }

        Rigidbody rb = currentWrap.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        isCooking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
