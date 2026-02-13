using UnityEngine;

public class milesCutMeat : MonoBehaviour
{
    public GameObject Knife;
    public GameObject Scooper;
    public GameObject meat;
    public GameObject meatPiece; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "meat" )
        {
            // Creates a new meat prefab at the knife's point of intersection 
            Instantiate(meatPiece, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), Quaternion.identity);

            
        }
    }
}
