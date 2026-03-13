using UnityEngine;

public class pickIngredient : MonoBehaviour
{
    public GameObject onion;
    public GameObject tomato;
    public GameObject pickle;
    public GameObject lettuce;
    public GameObject fries;
    public GameObject pita;
    public inHand grabHand;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void grabIngredient(GameObject container, GameObject hand, int isDesktop)
    {
        GameObject newFood = null;
        if( container.name == "onionContainer")
        {
            newFood = Instantiate(onion, hand.transform);
        }
        else if (container.name == "tomatoContainer"){
            newFood = Instantiate(tomato, hand.transform);
        }
        else if (container.name == "pickleContainer")
        {
            newFood = Instantiate(pickle, hand.transform);
        }
        else if (container.name == "PitaPile")
        {
            newFood = Instantiate(pita, hand.transform);
        }
        else if (container.name == "FriesContainer")
        {
            newFood = Instantiate(fries, hand.transform);
        }
        else
        {
            newFood = Instantiate(lettuce, hand.transform);
        }

        if(isDesktop ==1)
        {
            grabHand.isIngred = true;
            grabHand.pickUpObj(newFood);
        }

    }



}
