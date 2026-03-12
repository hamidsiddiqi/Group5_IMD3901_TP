using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Order : MonoBehaviour
{

    public GameObject ordBubble;
    public GameObject customer;
    public Camera playerCamera;
    public float interactRange = 5f;

    public GameObject list;
    public GameObject pita;

    public GameObject chicken;
    public GameObject falafel;
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

    public ShawarmaTransition shawTrans;
    bool isHoldingShaw = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        list.SetActive(false);
        ordBubble.SetActive(true);

        pita.SetActive(false);
        chicken.SetActive(false);
        beef.SetActive(false);
        falafel.SetActive(false);

        lettuce.SetActive(false);
        tomato.SetActive(false);
        fries.SetActive(false);
        onion.SetActive(false);
        pickle.SetActive(false);

        garlic.SetActive(false);
        hotSauce.SetActive(false);
        top2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        isHoldingShaw = shawTrans.isHolding;

        // Debug.Log(shawTrans.isHolding);

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("customer"))
            {
               if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    if (shawTrans.isHolding == false)
                    {

                        Debug.Log("not yippee!!!!");
                        pita.SetActive(true);
                        chicken.SetActive(true);

                        lettuce.SetActive(true);
                        top2.SetActive(true);
                        tomato.SetActive(true);

                        hotSauce.SetActive(true);

                        list.SetActive(true);
                        ordBubble.SetActive(false);
                    }
                    else if (shawTrans.isHolding == true)
                    {
                        Debug.Log("yipee!!!!");

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
