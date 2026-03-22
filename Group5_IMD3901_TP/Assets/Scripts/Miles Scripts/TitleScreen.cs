using System.Collections;
using TMPro;

using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class TitleScreen : MonoBehaviour
{

    // public TextMeshProUGUI ButText;

    public float offsetMultiplier = 1f;
    public float smoothTime = .3f;

    private Vector2 startPosition;
    private Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void play()
    {
        Debug.Log("play");
        SceneManager.LoadScene("KAIT_ASSETS");
    }

    public void grow(TextMeshProUGUI hoverBut)
    {
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

            yield return new WaitForSeconds(0.025f);
        }

    }

    public IEnumerator shrink2(TextMeshProUGUI hoverBut)
    {
        // playButText.fontSize = 24;

        for (int i = 0; i < 5; i++)
        {

            hoverBut.fontSize -= 1;

            yield return new WaitForSeconds(0.025f);
        }
    }

}