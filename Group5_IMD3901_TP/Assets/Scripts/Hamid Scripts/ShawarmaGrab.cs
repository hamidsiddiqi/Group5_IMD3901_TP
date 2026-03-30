using UnityEngine;
using UnityEngine.InputSystem;

public class ShawarmaGrab : MonoBehaviour
{
    public GameObject shawarmaReady;
    public Transform player;
    public Transform vrPlayer;
    public float wrapDistance = 5f;

    void Start()
    {
        if (shawarmaReady != null) shawarmaReady.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Transform activePlayer = (vrPlayer != null && vrPlayer.gameObject.activeInHierarchy) ? vrPlayer : player;
            float distance = Vector3.Distance(activePlayer.position, transform.position);
            Debug.Log("Pita: " + gameObject.name + " Distance: " + distance + " wrapDistance: " + wrapDistance);

            if (distance <= wrapDistance)
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;

                if (Physics.SphereCast(ray, 0.3f, out hit, wrapDistance))
                {
                    Debug.Log("SphereCast hit: " + hit.collider.gameObject.name);
                    ShawarmaGrab hitGrab = hit.collider.GetComponent<ShawarmaGrab>();
                    if (hitGrab == null)
                        hitGrab = hit.collider.GetComponentInParent<ShawarmaGrab>();

                    if (hitGrab == this)
                    {
                        WrapShawarma();
                    }
                }
            }
        }
    }

    void WrapShawarma()
    {
        Debug.Log("Shawarma Wrapped!");

        GameObject wrap = Instantiate(shawarmaReady, this.transform.position, Quaternion.identity);
        wrap.transform.Rotate(0f, 0f, -90f);
        wrap.SetActive(true);
        this.gameObject.SetActive(false);

        WrapObject currentWrap = this.GetComponent<WrapObject>();
        WrapObject newWrap = wrap.GetComponent<WrapObject>();

        newWrap.onions = currentWrap.onions;
        newWrap.fries = currentWrap.fries;
        newWrap.tomatoes = currentWrap.tomatoes;
        newWrap.lettuce = currentWrap.lettuce;
        newWrap.pickles = currentWrap.pickles;
        newWrap.onions = currentWrap.onions;
        newWrap.chicken = currentWrap.chicken;
        newWrap.beef = currentWrap.beef;
        newWrap.garlic = currentWrap.garlic;
        newWrap.hotSauce = currentWrap.hotSauce;

        Destroy(this.gameObject);
    }
}