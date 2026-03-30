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
    public GameObject pita;

    public GameObject chicken;
    public GameObject beef;

    public GameObject lettuce;
    public GameObject tomato;
    public GameObject fries;
    public GameObject onion;
    public GameObject pickle;

    public GameObject top2;

    public GameObject garlic;
    public GameObject hotSauce;

    public int protein;
    public int[] toppings = { 0,0,0,0,0};
    public int[] sauces = { 0, 0 };

    public inHand inPlayerHand;
    bool isHoldingShaw = false;
    public GameObject oih;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        list.SetActive(false);
        //ordBubble.SetActive(true);

        pita.SetActive(false);
        chicken.SetActive(false);
        beef.SetActive(false);

        lettuce.SetActive(false);
        tomato.SetActive(false);
        fries.SetActive(false);
        onion.SetActive(false);
        pickle.SetActive(false);

        garlic.SetActive(false);
        hotSauce.SetActive(false);
        top2.SetActive(false);

        currentCustomer = Instantiate(hat1, new Vector3(-3.61f, 1f, 5.88f), Quaternion.Euler(0f,-90f,0f));
        CurrentOrdBubble = Instantiate(ordBubble, new Vector3(-3.61f, 1f, 5.88f), Quaternion.Euler(90f, 0f, 0f));

       
    }

    // Update is called once per frame
    void Update()
    {
        if (inPlayerHand.objInHand != null)
        {
            oih = inPlayerHand.objInHand;
        }
        
        
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
                        pita.SetActive(true);
                        chicken.SetActive(true);

                        lettuce.SetActive(true);
                        top2.SetActive(true);
                        tomato.SetActive(true);

                        hotSauce.SetActive(true);

                        list.SetActive(true);
                        CurrentOrdBubble.SetActive(false);
                    }


                    if (oih.name == "wrap")
                    {
                        Debug.Log("Order Complete");
                        pita.SetActive(false);
                        chicken.SetActive(false);

                        lettuce.SetActive(false);
                        top2.SetActive(false);
                        tomato.SetActive(false);

                        hotSauce.SetActive(false);

                        list.SetActive(false);
                    }

                
                    
                }
            }

        }


}



}
