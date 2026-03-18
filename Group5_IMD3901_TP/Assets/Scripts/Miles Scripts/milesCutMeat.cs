using UnityEngine;
using UnityEngine.Windows;

public class milesCutMeat : MonoBehaviour
{
    public GameObject Knife;
    public GameObject Scooper;
    public GameObject ChickenPiece;
    public GameObject BeefPiece; 

    public GameObject beefSkewer;
    public GameObject chickenSkewer; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
        chickenSkewer.transform.Rotate(Vector3.up * 5f * Time.deltaTime);
        beefSkewer.transform.Rotate(Vector3.up * 5f * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BeefSkewer" )
        {
            // Creates a new meat prefab at the knife's point of intersection 
            Instantiate(BeefPiece, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), Quaternion.identity);
            
        }

        if (other.gameObject.tag == "ChickenSkewer")
        {
            // Creates a new meat prefab at the knife's point of intersection 
            Instantiate(ChickenPiece, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position), Quaternion.identity);

        }

        if (other.gameObject.tag == "Scooper_Area")
        {
          
            Debug.Log("meat collide");

        }
    }
}
