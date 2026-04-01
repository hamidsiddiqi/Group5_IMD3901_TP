using UnityEngine;

public class WrapObject : MonoBehaviour
{

    public bool isCooked = false;
    
    public int onions;
    public int fries;
    public int pickles;
    public int lettuce;
    public int tomatoes;
    public int chicken;
    public int beef;
    public int garlic;
    public int hotSauce;
    public bool isGrilled=true;
    
    void OnTriggerEnter(Collider collider)
    {
        //if the object us underneath the peta get outtaaa here
        if(collider.transform.position.y - 0.001 < transform.position.y)
        {
            return;
        }

        //check what entered and add it to the pita
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
        else if (collider.gameObject.tag == "chicken")
        {
            chicken += 1;
        }
        else if (collider.gameObject.tag == "beef")
        {
            beef += 1;
        }
        else if (collider.gameObject.tag == "garlic")
        {
            garlic += 1;
        }
        else if (collider.gameObject.tag == "hotSauce")
        {
            hotSauce += 1;
        }
        else
        {
            return;
        }

        //parent to the pita
        collider.transform.SetParent(transform);

    }

    void OnTriggerExit(Collider collider)
    {
        //if it leaves compare tag and remove from pita
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
        else if (collider.gameObject.tag == "chicken")
        {
            chicken -= 1;
        }
        else if (collider.gameObject.tag == "beef")
        {
            beef -= 1;
        }
        else if (collider.gameObject.tag == "garlic")
        {
            garlic -= 1;
        }
        else if (collider.gameObject.tag == "hotSauce")
        {
            hotSauce -= 1;
        }
        else
        {
            return;
        }
        //unparent it
        collider.transform.SetParent(null);

    }

    void OnCollisionExit(Collision collision)
    {
        //if it leaves the table
        if(collision.gameObject.tag == "table")
        {
            //go through each child and turn on kinematic
            foreach (Transform child in gameObject.transform)
            {
                if (child.name != "pita")
                {
                    Rigidbody rb = child.GetComponent<Rigidbody>();
                    // Only change settings if the Rigidbody actually exists
                    if (rb != null) 
                    {
                        rb.isKinematic = false;
                    }
                }
                
            }
            Debug.Log("leave table");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //if it lands on table
        if (collision.gameObject.tag == "table")
        {
            //go through each child and turn off kinematic
            foreach (Transform child in gameObject.transform)
            {
                if (child.name != "pita")
                {
                    Rigidbody rb = child.GetComponent<Rigidbody>();

                    // Only change settings if the Rigidbody actually exists
                    if (rb != null) 
                    {
                        rb.isKinematic = false;
                    }
                }
            }

            Debug.Log("enter table");
        }
    }

}