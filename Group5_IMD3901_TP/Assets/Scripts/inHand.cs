using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class inHand : MonoBehaviour
{
    public GameObject objInHand;
    public bool isHolding;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject hand;
    public Camera mainCamera;
    public GameObject customer;

    public GameObject knife;
    public GameObject Scooper;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objInHand = null;
        isHolding = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if you have smth in your hand
        if (objInHand != null && isHolding)
        {
            //if its a scooper
            if (objInHand.tag == "scooper")
            {
                //move the knife to right hand, scooper to left
                knife.transform.position = rightHand.transform.position;
                knife.transform.rotation = Quaternion.Euler(0f,0f,0f);
                //Scooper.transform.position = leftHand.transform.position;
            }
            //any other thing put in the middle
            if (objInHand.GetComponent<SauceBottle>() != null)
            {
                objInHand.transform.position = hand.transform.position + Vector3.up * 1f;
            }
            
            else
            {
                objInHand.transform.position = hand.transform.position;
            }
                
        }
    }

    public void dropObj()
    {
        //if object is empty leave the function
        if (objInHand == null)
        {
            return;
        }
        //reset sauce bottle rotation
        if (objInHand.GetComponent<SauceBottle>() != null)
        {
        objInHand.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        }

        //unparent the object and make kinematic false
        objInHand.transform.SetParent(null);
        objInHand.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("hello");

        //if its a wrap don't make it rotate 
        if (objInHand.tag == "flatwrap")
        {
            objInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        //have it move from hand and shoot slightly forward
        Vector3 newPos = objInHand.transform.position;
        newPos.y += 0.3f;
        objInHand.transform.position = newPos;
        objInHand.GetComponent<Rigidbody>().linearVelocity = (mainCamera.transform.forward*3f);

        //set variables back to nothing in hand
        objInHand =null;
        isHolding=false;

    }

    public void pickUpObj(GameObject newObject)
    {
        //if you have smth in hand leave function
        if(objInHand != null)
        {
            return;
        }

        //set variables 
        isHolding=true;
        objInHand= newObject;

        //parent the object to hand and enable kinematics
        objInHand.transform.SetParent(hand.transform,false);
        objInHand.GetComponent<Rigidbody>().isKinematic = true;

        // flip upside down if its a sauce bottle
        if (newObject.GetComponent<SauceBottle>() != null)
        {
            objInHand.transform.localRotation = Quaternion.Euler(270f, 0f, 0f);
            objInHand.transform.localPosition = new Vector3(0f, 100f, 0f);
        }

    }

    public void GiveShawarma()
    {
        // Parent it to the customer so they "hold" it
        objInHand.transform.SetParent(customer.transform);

        // Position it in front of the customer capsule
        objInHand.transform.localPosition = new Vector3(0f, 0.5f, 0.6f);
        objInHand.transform.localRotation = Quaternion.identity;

        // Keep it kinematic so it doesn't fall off the customer
        if (objInHand.GetComponent<Rigidbody>())
        {
            objInHand.GetComponent<Rigidbody>().isKinematic = true;
        }

        //reset variables 
        objInHand = null;
        isHolding = false;
    }

}
