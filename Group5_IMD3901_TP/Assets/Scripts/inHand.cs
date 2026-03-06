using UnityEngine;
using UnityEngine.UIElements;

public class inHand : MonoBehaviour
{
    public GameObject objInHand;
    public bool isHolding;
    public GameObject leftHand;
    public GameObject rightHand;
    public bool isIngred;
    public Camera mainCamera;
    public GameObject customer;

    public GameObject knife;
    public GameObject Scooper;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objInHand = null;
        isHolding = false;
        isIngred = false;
    }

    // Update is called once per frame
    void Update()
    {



        if (objInHand != null)
        {

            if (objInHand.tag == "scooper")
            {
                knife.transform.position = rightHand.transform.position;
                knife.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                Scooper.transform.position = leftHand.transform.position;
            }
            else
            {
                objInHand.transform.position = rightHand.transform.position;
            }
                
        }
    }

    public void dropObj()
    {
        if (objInHand == null)
        {
            return;
        }

        objInHand.transform.SetParent(null);
        objInHand.GetComponent<Rigidbody>().isKinematic = false;

        if (isIngred)
        {
            if (objInHand.tag == "wrap")
            {
                objInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            }
            Vector3 newPos = objInHand.transform.position;
            newPos.y += 0.5f;
            objInHand.transform.position = newPos;
            objInHand.GetComponent<Rigidbody>().linearVelocity = (mainCamera.transform.forward*5f);

        }

        objInHand =null;
        isHolding=false;
        isIngred=false;
    }

    public void pickUpObj(GameObject newObject)
    {
        if(objInHand != null)
        {
            return;
        }
        isHolding=true;
        objInHand= newObject;
        objInHand.transform.SetParent(rightHand.transform,false);
        objInHand.GetComponent<Rigidbody>().isKinematic = true;

    }

    public void GiveShawarma()
    {
        Debug.Log("Shawarma delivered to customer!");
        isHolding = false;

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

        objInHand = null;
        isHolding = false;
        isIngred = false;
    }

}
