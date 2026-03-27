using UnityEngine;

public class WrapObject : MonoBehaviour
{

    public bool isCooked = false;
    
    public int onions;
    public int fries;
    public int pickles;
    public int lettuce;
    public int tomatoes;
    
    void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.position.y - 0.001 < transform.position.y)
        {
            return;
        }

        if(collider.gameObject.tag == "onions")
        {
            onions += 1;
        }
        else if (collider.gameObject.tag == "pickle")
        {
            pickles += 1;
        }
        else if (collider.gameObject.tag == "tomatoes")
        {
            tomatoes += 1;
        }
        else if (collider.gameObject.tag == "fries")
        {
            fries += 1;
        }
        else if (collider.gameObject.tag == "lettuce")
        {
            lettuce += 1;
        }
        else
        {
            return;
        }

        //collider.GetComponent<Rigidbody>().isKinematic = true;
        
        collider.transform.SetParent(transform);

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "onions")
        {
            onions -= 1;
        }
        else if (collider.gameObject.tag == "pickle")
        {
            pickles -= 1;
        }
        else if (collider.gameObject.tag == "tomatoes")
        {
            tomatoes -= 1;
        }
        else if (collider.gameObject.tag == "fries")
        {
            fries -= 1;
        }
        else if (collider.gameObject.tag == "lettuce")
        {
            lettuce -= 1;
        }
        else
        {
            return;
        }
        collider.transform.SetParent(null);

    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "table")
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.name != "pita")
                {
                    child.GetComponent<Rigidbody>().isKinematic = true;
                    Debug.Log(child.name);
                }
                
            }
            Debug.Log("leave table");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "table")
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.name != "pita")
                {
                    child.GetComponent<Rigidbody>().isKinematic = false;
                    Debug.Log(child.name);
                }
            }

            Debug.Log("enter table");
        }
    }

}