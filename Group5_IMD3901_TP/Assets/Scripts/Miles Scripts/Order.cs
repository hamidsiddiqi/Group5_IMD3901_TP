using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Order : MonoBehaviour
{

    public GameObject hat1;
    public GameObject hat2;
    public GameObject ponytail1;
    public GameObject ponytail2;
    public GameObject currentCustomer; 
    public GameObject ordBubble;
    private GameObject CurrentOrdBubble;

    public Camera playerCamera;
    public float interactRange = 5f;

    public GameObject list;
   

    public int protein;
    public int[] toppings = { 0,0,0,0,0};
    public int[] sauces = { 0, 0 };

    public inHand inPlayerHand;
    bool isHoldingShaw = false;
    public GameObject oih;

    public PaniniGrill grill;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        list.SetActive(false);
        //ordBubble.SetActive(true);

        currentCustomer = Instantiate(hat1, new Vector3(-3.61f, 1f, 5.88f), Quaternion.Euler(0f,-90f,0f));
        CurrentOrdBubble = Instantiate(ordBubble, new Vector3(-3.61f, 1f, 5.88f), Quaternion.Euler(90f, 0f, 0f));

       
    }

    // Update is called once per frame
    void Update()
    {

            oih = inPlayerHand.objInHand;

        
        
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        CurrentOrdBubble.transform.position = new Vector3 (currentCustomer.transform.position.x, currentCustomer.transform.position.y + 2.75f, currentCustomer.transform.position.z);

       
       
        // Debug.Log(shawTrans.isHolding);
        

        if (Physics.Raycast(ray, out hit, interactRange))
        {

            if (hit.collider.CompareTag("customer"))
            {
               if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    
                    if (oih == null)
                    {

                        Debug.Log("not yippee!!!!");
                       

                        list.SetActive(true);
                        CurrentOrdBubble.SetActive(false);
                    }


                    if (oih.name == "wrap(Clone)" && grill.isCooked)
                    {
                      
                            Debug.Log("Order Complete");
                            list.SetActive(false);
                            grill.isCooked = false;
                        
                       
                    }
                    Debug.Log("oih: " + oih.name);


                }
            }

        }


}



}
