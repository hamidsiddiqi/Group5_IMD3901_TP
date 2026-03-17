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
    }

    public void getInside()
    {
        Debug.Log(onions);
        Debug.Log(pickles);
        Debug.Log(lettuce);
        Debug.Log(tomatoes);
        Debug.Log(fries);
    }
}