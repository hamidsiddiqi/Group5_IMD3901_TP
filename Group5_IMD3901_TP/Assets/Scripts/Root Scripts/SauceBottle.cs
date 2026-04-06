using UnityEngine;

public class SauceBottle : MonoBehaviour
{
    public Color sauceColor = Color.red;
    public GameObject sauceSplatPrefab;
    public AudioSource squirtSound;

    public void ApplySauce(GameObject wrap, RaycastHit hit)
    {
        if (sauceSplatPrefab != null)
        {
            Vector3 spawnPos = hit.point + Vector3.up * 0.02f;
            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
            GameObject splat = Instantiate(sauceSplatPrefab, spawnPos, rotation);
            splat.transform.parent = wrap.transform;
            splat.GetComponent<SpriteRenderer>().color = sauceColor;

            if (squirtSound != null) squirtSound.Play();
        }
        else
        {
            Debug.Log("No sauce splat prefab assigned!");
        }
    }
}