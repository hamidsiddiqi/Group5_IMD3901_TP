using UnityEngine;

public class CustomerMovement : MonoBehaviour
{

    public Transform startPos;
    public Transform orderPos;
    public Transform turnPos;
    public Transform pickUpPos;
    public Transform leavePos;

    public int orderNum;
    private Transform[] moves;
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

    public bool move;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = startPos.position;
        transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        moves = new Transform[5];

        moves[0] = startPos;
        moves[1] = orderPos;
        moves[2] = turnPos;
        moves[3] = pickUpPos;
        moves[4] = leavePos;

        move = false;
       
        curMove = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (moves == null)
        {
            return;
        }

        if (move==true)
        {
            if (curMove == 1 || curMove == 3 || curMove == 4)
            {
                transform.position = transform.position + new Vector3(0f,0f,-0.02f);
                if (curMove == 3 && transform.position == moves[curMove].position)
                {
                    // transform.rotation = Quaternion.Euler(0f, -180f, 0f);
                    Debug.Log("rotate");
                }
            }
            else if (curMove == 2)
            {
                transform.position = transform.position + new Vector3(-0.02f, 0f, -0f);
            }
        }
        
    }

    public void nextMove()
    {
        if (curMove == 1)
        {
            //transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            Debug.Log("rotate here");
        }
        else if (curMove == 2 || curMove == 4)
        {
            //transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            Debug.Log("rotate once more");
        }
        curMove++;
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("enter");
        if (move = false)
        {
            return;
        }

        if (collider.gameObject.tag == "locator")
        {
            move = false;
            Invoke("nextMove()", 2);
        }
    }
}