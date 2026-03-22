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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void play()
    {
        pressedSound.Play();
        transitionManager.playGame();
        // SceneManager.LoadScene("KAIT_ASSETS");
    }

    public void Settings()
    {
        pressedSound.Play();
    }

    public void Credits()
    {
        pressedSound.Play();
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