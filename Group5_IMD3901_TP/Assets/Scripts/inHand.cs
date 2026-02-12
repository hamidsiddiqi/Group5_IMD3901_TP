using UnityEngine;
using UnityEngine.UIElements;

public class inHand : MonoBehaviour
{
    GameObject objInHand;
    public bool isHolding;
    public GameObject leftHand;
    public GameObject rightHand;
    public bool isIngred;
    public Camera mainCamera;



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
            objInHand.transform.position = rightHand.transform.position;
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


}
