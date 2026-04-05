using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{

    public static int CustomersServed = 0;
    public static int Money = 0;

    public TextMeshProUGUI customerText;
    public TextMeshProUGUI MoneyText;

    public AudioSource hoverSound;
    public AudioSource press;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        customerText.SetText(CustomersServed.ToString());
        MoneyText.SetText(Money.ToString());
    }

    public void GoToTitle()
    {
        press.Play();
        SceneManager.LoadScene("TitleScreen");
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
