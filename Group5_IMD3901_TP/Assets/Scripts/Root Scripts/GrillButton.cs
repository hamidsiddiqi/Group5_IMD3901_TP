using UnityEngine;

public class GrillButton : MonoBehaviour
{
    public PaniniGrill grill;
    public Transform buttonCube; // visual cube that moves
    private Vector3 buttonUpPos;
    private Vector3 buttonDownPos;
    private bool isPressed = false;

    void Start()
    {
        buttonUpPos = buttonCube.localPosition;
        buttonDownPos = buttonUpPos - new Vector3(0, 0.05f, 0); // moves down
    }

    public void Press()
    {
        if (!isPressed)
        {
            isPressed = true;
            
            // animate button down
            buttonCube.localPosition = buttonDownPos;
            
            // tell grill to cook
            if (grill != null)
            {
                grill.TryStartGrilling();
            }
            
            // pop back up after 0.5 seconds
            Invoke("Release", 0.5f);
        }
    }

    void Release()
    {
        buttonCube.localPosition = buttonUpPos;
        isPressed = false;
    }
}