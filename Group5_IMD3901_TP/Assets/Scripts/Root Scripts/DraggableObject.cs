using UnityEngine;

public class DraggableObject : MonoBehaviour
{

    private bool isDragging = false;
    private Camera mainCamera;
    private float distanceFromCamera = 2f;
    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;

        //calculate offset from mouse to object
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 mouseWorldPos = ray.GetPoint(distanceFromCamera);
        offset = transform.position - mouseWorldPos;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 mouseWorldPos = ray.GetPoint(distanceFromCamera);
            transform.position = mouseWorldPos + offset;
        }
    }
}
