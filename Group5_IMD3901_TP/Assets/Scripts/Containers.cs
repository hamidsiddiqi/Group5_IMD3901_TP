using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Containers : MonoBehaviour
{
    public GameObject fries;
    public GameObject lettuce;
    public GameObject tomato;
    public GameObject pickle;
    public GameObject onion;

    void OnTriggerExit(Collider collider)
    {

        if (collider.TryGetComponent<XRGrabInteractable>(out var interactable))
        {
            Debug.Log("you can grab it");
            if (interactable.isSelected)
            {
                Debug.Log("its been grabbed");
            }
        }

        if(transform.parent.transform.childCount < 9)
        {
            Debug.Log("we missing stuff");
            Vector3 location = transform.position;
            location.x += Random.Range(-0.2f, 0.2f);
            location.y += 0.02f;
            location.z += Random.Range(-0.2f, 0.2f);

            if (collider.tag == "fries")
            {
                Debug.Log("FRIES GONE");
                GameObject replace = Instantiate(fries, location, Quaternion.identity);
                replace.transform.parent = transform.parent.transform;
            }
        }
    }



}
