using System.Collections;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class TitleScreen : MonoBehaviour
{

    // public TextMeshProUGUI ButText;

    public float offsetMultiplier = 1f;
    public float smoothTime = .3f;

    public AudioSource hoverSound;
    public AudioSource pressedSound;

    private Vector2 startPosition;
    private Vector3 velocity;

    public TransitionManager transitionManager;

    public GameObject credits;
    public GameObject settings;

    public AudioSource music; 
    public AudioSource StartSound;

    public GameObject LevelSelect;

    public GameObject VRDesktopScreen;
    public GameObject DesktopButton;
    public GameObject VRButton; 

    public static string DesktopOrVR = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        credits.SetActive(false);
        settings.SetActive(false);
        LevelSelect.SetActive(false);
        VRDesktopScreen.SetActive(false);   

       
    }

    public void play(string sceneNumber)
    {
        Debug.Log(DesktopOrVR);
        music.Stop();
        StartSound.Play();
        transitionManager.playGame(sceneNumber);
        // SceneManager.LoadScene("KAIT_ASSETS");
    }

    public void LevelStart(string lvlID)
    {
        SceneManager.LoadScene("Level "+lvlID);
    }

    public void vrorDeskScreen()
    {
        VRDesktopScreen.SetActive(true);
    }

    public void LevelSelectScreen(string DorV)
    {
        DesktopOrVR = DorV; 
        pressedSound.Play();
        LevelSelect.SetActive(true);
    }

    public void Settings()
    {
        pressedSound.Play();
        settings.SetActive(true);
    }

    public void BackSettings()
    {
        pressedSound.Play();
        settings.SetActive(false);
    }

    public void Credits()
    {
        pressedSound.Play();
        credits.SetActive(true);
    }

    public void BackCredits()
    {
        pressedSound.Play();
        credits.SetActive(false);
    }

    public void Quit()
    {
        pressedSound.Play();
        Application.Quit();
     EditorApplication.isPlaying = false;
    }

    public void grow(TextMeshProUGUI hoverBut)
    {
        hoverSound.Play();
        hoverBut.fontSize = 24;

        if (hoverBut.fontSize < 30)
        {
            StartCoroutine(grow2(hoverBut));
        }

    }

    public void shrink(TextMeshProUGUI hoverBut)
    {
        hoverBut.fontSize = 29;

        if (hoverBut.fontSize > 20)
        {
            StartCoroutine(shrink2(hoverBut));
        }

    }

    public IEnumerator grow2(TextMeshProUGUI hoverBut)
    {
        for (int i = 0; i < 5; i++)
        {

            hoverBut.fontSize += 1;
            hoverBut.color = Color.Lerp(hoverBut.color, Color.gray2, 0.5f);
            yield return new WaitForSeconds(0.025f);
        }

    }

    public IEnumerator shrink2(TextMeshProUGUI hoverBut)
    {
        // playButText.fontSize = 24;

        for (int i = 0; i < 5; i++)
        {

            hoverBut.fontSize -= 1;
            hoverBut.color = Color.Lerp(hoverBut.color, Color.black, 0.5f);
            yield return new WaitForSeconds(0.025f);
        }
    }

}