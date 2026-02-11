using UnityEngine;
using UnityEngine.InputSystem;

public class MilesInteract : MonoBehaviour
{

    public float interactRange = 5f;
    public Camera playerCamera;
    public CrosshairUI crosshairUIScript;

    public GameObject knife;
    public GameObject Scooper;
    public GameObject leftHand;
    public GameObject rightHand;

    public bool knifePickup = false; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("Knife&Scoop"))
            {
                // crosshairUIScript.SetInteract(true);

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {

                    Debug.Log("meme");
                    knifePickup = true;
                    knife.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                    Scooper.transform.rotation = Quaternion.Euler(0f, 90f, 0f);



                }

                return;
            }
        }

        if (knifePickup)
        {
            knife.transform.position = rightHand.transform.position; 
            Scooper.transform.position = leftHand.transform.position;


        }


        // crosshairUIScript.SetInteract(false);


    }
}
