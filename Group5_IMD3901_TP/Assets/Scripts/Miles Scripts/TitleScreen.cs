using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TitleScreen : MonoBehaviour
{
    // public Button playBut; 
    public TextMeshProUGUI playButText;

    public float offsetMultiplier = 1f;
    public float smoothTime = .3f;

    private Vector2 startPosition;
    private Vector3 velocity; 
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update() 
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
      
      
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Cursor Entering " + name);
        // Place the function/action you want to occur here
         
            Debug.Log("meme");
        
    }

    public void play() {
        Debug.Log("play");
    }

    public void grow()
    {
        playButText.fontSize = 24;

        if (playButText.fontSize < 30)
        {
            
            StartCoroutine(grow2());

        }
        
    }

    public void shrink()
    {
        playButText.fontSize = 29;

        if (playButText.fontSize > 20)
        {

            StartCoroutine(shrink2());

        }

    }

    public IEnumerator grow2()
    {
        for (int i = 0; i < 5; i++)
        {
            
            playButText.fontSize += 1;
            yield return new WaitForSeconds(0.025f);
        }

        Debug.Log(playButText.fontSize);

    }

    public IEnumerator shrink2()
    {
        // playButText.fontSize = 24;

        for (int i = 0; i < 5; i++)
        {

            playButText.fontSize -= 1;
            yield return new WaitForSeconds(0.025f);
        }
    }
}
