using UnityEngine;

public class CustomerMovement : MonoBehaviour
{

    public GameObject startPos;
    public GameObject orderPos;
    public GameObject turnPos;
    public GameObject pickUpPos;
    public GameObject leavePos;

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
            if (curMove == 0||curMove==2||curMove==4)
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

            //turn off movement
            isMove = false;

            Invoke("nextMove", 1);

        }
    }
}