using System.Collections;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TitleScreen : MonoBehaviour
{

    public float offsetMultiplier = 1f;
    public float smoothTime = .3f;

    private Vector2 startPosition;
    private Vector3 velocity; 

    private Color greyhighlight = new Color(150, 150, 150);

    public GameObject CreditsScreen;
    public GameObject TitleCanvas; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        CreditsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update() 
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
      
      
    }

    public void play() {
        Debug.Log("play");
    }

    public void settings()
    {

    }
    public void Credits()
    {
        CreditsScreen.SetActive(true);
        CreditsScreen.transform.SetAsFirstSibling();
    }

    public void CreditsBack()
    {
        CreditsScreen.SetActive(false);
    }


    public void Quit()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

    public void grow(TextMeshProUGUI HoverText)
    {

        HoverText.fontSize = 24;

        if (HoverText.fontSize < 30)
        {
            
            StartCoroutine(grow2(HoverText));

        }
        
    }

    public void shrink(TextMeshProUGUI HoverText)
    {
        HoverText.fontSize = 29;

        if (HoverText.fontSize > 20)
        {

            StartCoroutine(shrink2(HoverText));

        }

    }

    public IEnumerator grow2(TextMeshProUGUI HoverText)
    {
        for (int i = 0; i < 5; i++)
        {

            HoverText.fontSize += 1;
            HoverText.color = Color.Lerp(HoverText.color, Color.gray, 0.5f);
            yield return new WaitForSeconds(0.025f);
        }

    }

    public IEnumerator shrink2(TextMeshProUGUI HoverText)
    {

        for (int i = 0; i < 5; i++)
        {

            HoverText.fontSize -= 1;
            HoverText.color = Color.Lerp(HoverText.color, Color.black, 0.5f);
            yield return new WaitForSeconds(0.025f);
        }
    }
}
