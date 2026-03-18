using UnityEngine;

public class Containers : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider+"enter");
        collider.GetComponent<Rigidbody>().isKinematic = true;
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log(collider);
        collider.GetComponent<Rigidbody>().isKinematic = false;
    }

}
