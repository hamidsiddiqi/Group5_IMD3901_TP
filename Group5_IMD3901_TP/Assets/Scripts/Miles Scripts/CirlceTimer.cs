using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CirlceTimer : MonoBehaviour
{

    private bool isActive = false;
    private float indicatorTimer;
    private float maxIndicatorTimer;
    public Image radialProgressBar;

    public float time;

    public GameObject DesktopInstructions;
    public GameObject VRInstructions; 

    private void Awake()
    {
       
    }

    // start the countdown
    public void ActivateCountdown(float countdownTime)
    {
        isActive = true; 
        maxIndicatorTimer = countdownTime;
        indicatorTimer = maxIndicatorTimer;
    }

    public void StopCountDown()
    {
        isActive = false; 
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //VRInstructions.SetActive(false);
        //DesktopInstructions.SetActive(false);

        //// show the instructions based on which system youre playing 
        //if (SceneManager.GetActiveScene().name == "Level 1")
        //{
        //    if (TitleScreen.DesktopOrVR == "VR")
        //    {
        //        VRInstructions.SetActive(true);
        //    }
        //    else
        //    {
        //        DesktopInstructions.SetActive(true);
        //    }
               
           
        //}

        // if not activate the timer
        if (SceneManager.GetActiveScene().name == "Ver Four" || SceneManager.GetActiveScene().name == "Level 2" || SceneManager.GetActiveScene().name == "Level 3")
        {
            ActivateCountdown(time);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            if (Keyboard.current.xKey.wasPressedThisFrame)
            {
                ActivateCountdown(time);
                DesktopInstructions.SetActive(false);
                VRInstructions.SetActive(false);
            }
        }

        if (isActive)
        {
            indicatorTimer -= Time.deltaTime;
            radialProgressBar.fillAmount = (indicatorTimer / maxIndicatorTimer);

            if (indicatorTimer <= 0)
            {
                StopCountDown();
            }
        }
    }
}
