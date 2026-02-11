using UnityEngine;
using UnityEngine.UIElements;

public class inHand : MonoBehaviour
{
    GameObject objInHand;
    public bool isHolding;
    public GameObject leftHand;
    public GameObject rightHand;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objInHand = null;
        isHolding = false;
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
        objInHand=null;
        isHolding=false;
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

    }

}
