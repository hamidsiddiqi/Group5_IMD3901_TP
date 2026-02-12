using UnityEngine;

public class placePita : MonoBehaviour
{
    public GameObject pitaLoc;
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
        if (collider.gameObject.tag == "wrap")
        {
            collider.gameObject.transform.position=pitaLoc.transform.position;
            
        }
    }
}
