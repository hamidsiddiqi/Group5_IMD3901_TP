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

    public Order ord;
    public GameObject shawObj;

    public AudioSource grabSound;
    public AudioSource dropSound;



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

        if (dropSound != null) dropSound.Play();

        //reset sauce bottle rotation
        if (objInHand.GetComponent<SauceBottle>() != null)
        {
            objInHand.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        }

        //unparent the object and make kinematic false
        objInHand.transform.SetParent(null);
        objInHand.GetComponent<Rigidbody>().isKinematic = false;
        objInHand.GetComponent<Rigidbody>().useGravity = true;
        objInHand.GetComponent<Rigidbody>().WakeUp();

        objInHand.transform.position += mainCamera.transform.forward * 0.1f;
        //if its a wrap don't make it rotate 
        if (objInHand.tag == "flatwrap")
        {
            objInHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }

        //have it move from hand and shoot slightly forward
        Vector3 newPos = objInHand.transform.position;
        newPos.y += 0.3f;
        objInHand.transform.position = newPos;
        objInHand.GetComponent<Rigidbody>().linearVelocity = (mainCamera.transform.forward*2f);

        Debug.Log(objInHand.GetComponent<Rigidbody>().linearVelocity);

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

        if (grabSound != null) grabSound.Play();

        //set variables 
        isHolding =true;
        objInHand= newObject;

        //parent the object to hand and enable kinematics
        objInHand.transform.SetParent(hand.transform,true);
        objInHand.GetComponent<Rigidbody>().isKinematic = true;

        // flip upside down if its a sauce bottle
        if (newObject.GetComponent<SauceBottle>() != null)
        {
            objInHand.transform.localRotation = Quaternion.Euler(270f, 0f, 0f);
            objInHand.transform.localPosition = new Vector3(0f, 100f, 0f);
        }

        if (newObject.CompareTag("knife"))
        {
            objInHand.transform.localRotation = Quaternion.Euler(270f, 0f, 180f);
        }

    }

}
