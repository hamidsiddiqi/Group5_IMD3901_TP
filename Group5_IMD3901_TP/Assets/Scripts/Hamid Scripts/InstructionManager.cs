using UnityEngine;
using UnityEngine.InputSystem;

public class InstructionManager : MonoBehaviour
{
    [Header("Instructions")]
    public GameObject desktopInstructions;
    public GameObject vrInstructions;

    [Header("Players")]
    public GameObject desktopPlayer;
    public GameObject vrPlayer;

    void Start()
    {
        desktopInstructions.SetActive(false);
        vrInstructions.SetActive(false);

        // Checking which player is currently active in the scene and show their instructions
        if (vrPlayer != null && vrPlayer.activeInHierarchy)
        {
            vrInstructions.SetActive(true);
        }
        else if (desktopPlayer != null && desktopPlayer.activeInHierarchy)
        {
            desktopInstructions.SetActive(true);
        }
    }

    void Update()
    {
        // If X is pressed, hide everything to begin the game
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        desktopInstructions.SetActive(false);
        vrInstructions.SetActive(false);

        Debug.Log("Instructions Hidden");
        // You can add logic here to enable player movement if it was frozen
    }
}