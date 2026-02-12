using UnityEngine;

public class SauceBottle : MonoBehaviour
{
    public GameObject saucePrefab; //small sphere
    public Transform spoutPosition; //where sauce comes out
    public float dispenseInterval = 0.2f; //time between sauce drops

    private float lastDispenseTime;
    private bool isHeld = false;
    private Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //follow mouse if held
        if (isHeld)
        {
            FollowMouse();

            //dispense sauce while holding left mouse
            if (Input.GetMouseButton(0) && Time.time > lastDispenseTime + dispenseInterval)
            {
                DispenseSauce();
                lastDispenseTime = Time.time;
            }
        }
    }

    void OnMouseDown()
    {
        //pick up bottle
        isHeld = true;
    }

    void OnMouseUp()
    {
        //drop bottle
        isHeld = false;
    }

    void FollowMouse()
    {
        //keep bottle at fixed distance from camera
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition = ray.GetPoint(2f); //2 units from camera
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
    }

    void DispenseSauce()
    {
        GameObject sauce = Instantiate(saucePrefab, spoutPosition.position, Quaternion.identity);

        //add slight downward force
        Rigidbody rb = sauce.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.down * 3f, ForceMode.Impulse);
        }

        //destroy after 3 seconds to avoid clutter
        Destroy(sauce, 3f);
    }
}
