using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomerMovement : MonoBehaviour
{

    public GameObject startPos;
    public GameObject orderPos;
    public GameObject turnPos;
    public GameObject pickUpPos;
    public GameObject leavePos;
    public GameObject orderBubble;
    public GameObject order;
    public GameObject shawarma;

    public int orderNum;
    private GameObject[] moves;
    public int curMove;

    public int onions;
    public int fries;
    public int pickles;
    public int lettuce;
    public int tomatoes;
    public int chicken;
    public int beef;
    public int garlic;
    public int hotSauce;

    public bool isMove;

    public bool gaveOrder = false;

    public AudioSource hello;
    public AudioSource thanks;
    public AudioSource bark;
    public AudioSource chaChing;

    public CustomerMovement nextPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = startPos.transform.position;
        transform.rotation = Quaternion.Euler(0f, -90f, 0f);

        moves = new GameObject[5];

        moves[0] = startPos;
        moves[1] = orderPos;
        moves[2] = turnPos;
        moves[3] = pickUpPos;
        moves[4] = leavePos;

        //isMove = false;
       
        curMove = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if they are moving
        if (isMove==true)
        {
            //if in movement 0, 2, or 4
            if (curMove == 0||curMove==3||curMove==4)
            {
                //adjust z by negative value
                transform.position = transform.position + new Vector3(0f,0f,-0.02f);
            }
            //if in movement 1
            if (curMove == 1)
            {
                //adjust x by pos value
                transform.position = transform.position + new Vector3(0.02f, 0f, 0f);
            }

        }

        if (curMove == 4 && SceneManager.GetActiveScene().name == "Level 1")
        {
                SceneManager.LoadScene("Level 2");
        }

    }

    public void nextMove()
    {
        //if it moved to the counter
        if (curMove == 0)
        {
            //turns away from counter
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            //starts moving again
            isMove = true;
        }
        else if (curMove == 1||curMove==3)
        {
            //turns away from counter
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            //starts moving again
            isMove = true;
        }
        else if (curMove == 2)
        {
            //turns away from counter
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            //does not move until it gets the order so isMove is still false
            isMove = true;
        }
        //if it is at the end
        else if (curMove == 4)
        {
            //reset all the positions
            for(int i =0; i < moves.Length; i++)
            {
                moves[i].SetActive(true);
            }
            //deactivate
            gameObject.SetActive(false);

            

        }
        //increase current move counter
        curMove++;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (isMove == false)
        {
            return;
        }

        //if it hits a locator
        if (collider.gameObject.tag == "locator")
        {
            //deactivate locator so it can move again
            collider.gameObject.SetActive(false); 

            //once it gets to counter show the bubble
            if(curMove == 0)
            {
                orderBubble.SetActive(true);
            }
            if (curMove == 1)
            {
                nextMove();
            }
            else if (curMove == 2)
            {
                isMove = true;
                curMove++;
            }
            else if (curMove == 3)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                isMove = false;
            }
            else if (curMove == 4)
            {
                if (nextPlayer != null)
                {
                    nextPlayer.isMove = true;
                }
                gameObject.SetActive(false);
            }
            else
            {
                isMove = false;
            }
        }
    }

    public void getOrder()
    {
        hello.Play();
        gaveOrder = true; 
        orderBubble.SetActive(false);
        order.SetActive(true);
        Invoke("nextMove", 2);
    }

    public void giveOrder(GameObject wrap)
    {
        WrapObject wo= wrap.GetComponent<WrapObject>();

        if(compareOrder(wo) == false)
        {
            return;
        }

        else
        {
            shawarma.SetActive(true);
            nextMove();
        }

    }

    bool compareOrder(WrapObject wrap)
    {
        if (wrap.onions == onions)
        {
            if (wrap.fries == fries)
            {
                if (wrap.tomatoes == tomatoes)
                {
                    if(wrap.lettuce== lettuce)
                    {
                        if (wrap.pickles == pickles)
                        {
                            if (wrap.chicken == chicken)
                            {
                                if (wrap.beef == beef)
                                {
                                    if(wrap.garlic >= garlic)
                                    {
                                        if(wrap.hotSauce >= hotSauce)
                                        {
                                            if(wrap.isCooked == true)
                                            {
                                                Debug.Log("thats all correct");
                                                order.SetActive(false);
                                                Results.CustomersServed++; 
                                                Results.Money += 10;
                                                chaChing.Play();
                                                thanks.Play();
                                                return true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("you failed");
        bark.Play();
        return false;
    }
}