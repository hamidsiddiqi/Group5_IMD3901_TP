using TMPro;
using UnityEngine;

public class Results : MonoBehaviour
{

    public static int CustomersServed = 0;
    public static int Money = 0;

    public TextMeshProUGUI customerText;
    public TextMeshProUGUI MoneyText;
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
}
