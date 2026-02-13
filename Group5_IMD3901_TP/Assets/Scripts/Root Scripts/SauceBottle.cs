using UnityEngine;

public class SauceBottle : MonoBehaviour
{
    public Color sauceColor = Color.white;

    public void ApplySauce(GameObject wrap)
    {
        Renderer rend = wrap.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = Color.Lerp(rend.material.color, sauceColor, 0.4f);
            Debug.Log("Sauce applied! Color changed.");
        }
        else
        {
            Debug.Log("No renderer found on wrap!");
        }
    }
}