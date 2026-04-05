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
        //if it leaves and its vr 
        if (collider.TryGetComponent<XRGrabInteractable>(out var interactable))
        {
            Debug.Log("you can grab it");
            if (interactable.isSelected)
            {
                Debug.Log("its been grabbed");
            }
        }

        //if the container has less than 9 objects in it 
        if(transform.parent.transform.childCount < 9)
        {
            //make location
            Vector3 location = transform.position;
            //randomize x & z, plus move up y slightly
            location.x += Random.Range(-0.2f, 0.2f);
            location.y += 0.03f;
            location.z += Random.Range(-0.2f, 0.2f);

            //check what left the box and replace it 
            if (collider.tag == "fries")
            {
                GameObject replace = Instantiate(fries, location, Quaternion.identity);
                replace.transform.parent = transform.parent.transform;
            }
            else if (collider.tag == "onions")
            {
                GameObject replace = Instantiate(onion, location, Quaternion.identity);
                replace.transform.parent = transform.parent.transform;
            }
            else if (collider.tag == "lettuce")
            {
                GameObject replace = Instantiate(lettuce, location, Quaternion.identity);
                replace.transform.parent = transform.parent.transform;
            }
            else if (collider.tag == "tomatoes")
            {
                GameObject replace = Instantiate(tomato, location, Quaternion.identity);
                replace.transform.parent = transform.parent.transform;
            }
            else if (collider.tag == "pickle")
            {
                GameObject replace = Instantiate(pickle, location, Quaternion.identity);
                replace.transform.parent = transform.parent.transform;
            }
        }
    }
}
