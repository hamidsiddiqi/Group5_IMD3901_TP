using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Order : MonoBehaviour
{
    public GameObject ordBubble;

    public Camera playerCamera;
    public float interactRange = 5f; 

    public inHand inPlayerHand;
    bool isHoldingShaw = false;
    public GameObject oih;

    public CustomerMovement movement;

    public PaniniGrill grill;

    public GameObject vrPlayer;
    public GameObject DesktopPlayer;

    public TitleScreen title;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

     
    }

    // Update is called once per frame
    void Update()
    {
        
      //Debug.Log("desktop or VR: "+ title.DesktopOrVR);

      //  if (title.DesktopOrVR == "VR")
      //  {
      //     vrPlayer.SetActive(true);
      //      DesktopPlayer.SetActive(false);
      //  }
      //  else
      //  {
      //      vrPlayer.SetActive(false);
      //      DesktopPlayer.SetActive(true);
      //  }



        oih = inPlayerHand.objInHand;

        
        
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        ordBubble.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2.75f, this.gameObject.transform.position.z);

       
       
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
                        ordBubble.SetActive(false);
                    }
                    if (oih.tag == "wrap")
                    {  
                        Debug.Log("Order Complete");
                        grill.isCooked = false;  
                    }
                    Debug.Log("oih: " + oih.name);
                }
            }

        }


}



}
