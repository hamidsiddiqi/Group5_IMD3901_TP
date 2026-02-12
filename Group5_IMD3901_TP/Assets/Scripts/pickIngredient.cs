using UnityEngine;

public class pickIngredient : MonoBehaviour
{
    public GameObject onion;
    public GameObject tomato;
    public GameObject pickle;
    public GameObject yellow;
    public GameObject pita;
    public GameObject hand;
    public inHand grabHand;




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
        GameObject newFood = null;
        if( container.name == "onion")
        {
            newFood = Instantiate(onion, hand.transform);
        }
        else if (container.name == "tomato"){
            newFood = Instantiate(tomato, hand.transform);
        }
        else if (container.name == "pickle")
        {
            newFood = Instantiate(pickle, hand.transform);
        }
        else if (container.name == "pitaPile")
        {
            newFood = Instantiate(pita, hand.transform);
        }
        else
        {
            newFood = Instantiate(yellow, hand.transform);
        }
            grabHand.isIngred = true;
        grabHand.pickUpObj(newFood);

    }



}
