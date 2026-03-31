using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class levelButton : MonoBehaviour
{
    public string sceneName;

    public void onClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
