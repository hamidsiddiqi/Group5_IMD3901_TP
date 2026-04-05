using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class TImer : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    public float time =100;
    public float orignalTime; 

    public GameObject effect;
    public bool isActive;

    public bool start = false;

    public CirlceTimer circTime;

    public TextMeshProUGUI moneyText;

    public GameObject DesktopInstructions;
    public GameObject VRInstructions;

    public InputActionReference vrTriggerAction;

    public void StartTimer(float time)
    {
        isActive = true;
        effect.SetActive(true);
        circTime.ActivateCountdown(time);
        // effect.transform.Find("RadialProgressBar").GetComponent<CircleImage>()ActivateCountdown(time);

        StartCoroutine(EndEffect(time));
    }

    IEnumerator EndEffect(float time)
    {
        yield return new WaitForSeconds(time);
        isActive = false;
        effect.SetActive(false);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        timerText.SetText(time.ToString());

        orignalTime = time;

        

        // show the instructions based on which system youre playing 
        if (SceneManager.GetActiveScene().name == "Level 1")
        {

            VRInstructions.SetActive(false);
            DesktopInstructions.SetActive(false);

            if (TitleScreen.DesktopOrVR == "VR")
            {
                VRInstructions.SetActive(true);
            }
            else
            {
                VRInstructions.SetActive(false);
                DesktopInstructions.SetActive(true);
            }


        }

    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Level 1" && !start)
        {
            
            if (vrTriggerAction != null && vrTriggerAction.action.WasPressedThisFrame())
            {
                start = true;
                DesktopInstructions.SetActive(false);
                VRInstructions.SetActive(false);
            }
            if (Keyboard.current.xKey.wasPressedThisFrame)
            {
                start = true;
                DesktopInstructions.SetActive(false);
                VRInstructions.SetActive(false);
            }
        }
        else
        {
            start = true;
        }

            moneyText.SetText(Results.Money.ToString());

        if (start)
        {
            if (time >= 0 && isActive)
            {
                time -= Time.deltaTime;
            }
            else
            {
                timerText.SetText("Time's up!");
                SceneManager.LoadScene("Results");
            }
        }
       
        timerText.SetText(MathF.Ceiling(time).ToString());
    }

}
