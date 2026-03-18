using System.Collections.Generic;
using UnityEngine;

public class Containers : MonoBehaviour
{
    private HashSet<Rigidbody> objectsInside = new HashSet<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null && !objectsInside.Contains(rb))
        {
            objectsInside.Add(rb);
            rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null && objectsInside.Contains(rb))
        {
            objectsInside.Remove(rb);
            rb.isKinematic = false;
        }
    }

}
