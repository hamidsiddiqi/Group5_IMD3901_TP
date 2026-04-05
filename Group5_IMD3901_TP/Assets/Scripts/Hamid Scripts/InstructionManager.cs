
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class InstructionManager : MonoBehaviour
{
    [Header("Instructions")]
    public GameObject desktopInstructions;
    public GameObject vrInstructions;

    [Header("Players")]
    public GameObject desktopPlayer;
    public GameObject vrPlayer;

    [Header("VR Trigger Action")]
    // Drag your 'Select' or 'Trigger' action here from your VR Input Actions
    public InputActionReference vrTriggerAction;

    void Start()
    {
        desktopInstructions.SetActive(false);
        vrInstructions.SetActive(false);

        // Check which player's camera is actually being used by the game
        if (vrPlayer != null && vrPlayer.activeInHierarchy)
        {
            vrInstructions.SetActive(true);
            if (vrTriggerAction != null) vrTriggerAction.action.Enable();

            // Force desktop instructions OFF just in case
            desktopInstructions.SetActive(false);
        }
        else
        {
            desktopInstructions.SetActive(true);
            vrInstructions.SetActive(false);
        }
    }


    void Update()
    {
        // Check for Keyboard X
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            StartGame();
        }

        // Check for VR Trigger press
        if (vrTriggerAction != null && vrTriggerAction.action.WasPressedThisFrame())
        {
            StartGame();
        }
    }

    void StartGame()
    {
        desktopInstructions.SetActive(false);
        vrInstructions.SetActive(false);
        Debug.Log("Instructions closed and game started.");
    }
}