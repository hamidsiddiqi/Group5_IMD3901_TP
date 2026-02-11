using UnityEngine;

public class pickIngredient : MonoBehaviour
{
    public GameObject onion;
    public GameObject tomato;
    public GameObject pickle;
    public GameObject pita;
    public GameObject hand;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void grabIngredient(GameObject container)
    {
        if( container.name == "onion")
        {
            Instantiate(onion, hand.transform);
        }
        else if (container.name == "tomato"){
            Instantiate(tomato, hand.transform);
        }
        else if (container.name == "pickle")
        {
            Instantiate(pickle, hand.transform);
        }
        else if (container.name == "pita")
        {
            Instantiate(pita, hand.transform);
        }

    }



}
