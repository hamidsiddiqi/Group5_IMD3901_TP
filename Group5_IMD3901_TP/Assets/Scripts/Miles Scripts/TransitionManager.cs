using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{

    [SerializeField] private GameObject _startingSceneTransition;
    [SerializeField] private GameObject _EndingSceneTransition;

    private bool transitioning = false;

    private void startScene()
    {
        _startingSceneTransition.SetActive(true);

    }

    void DisableStartingSceneTransition()
    {
        _startingSceneTransition.SetActive(true);
        StartCoroutine(WaitSec());
    }

    private IEnumerator WaitSec()
    {
      
            yield return new WaitForSeconds(1.0f);
            _startingSceneTransition.SetActive(true);

    }

    private IEnumerator WaitSec2(string sceneNumber)
    {
      
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("Level "+sceneNumber);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().name == "Level 2" || SceneManager.GetActiveScene().name == "Level 3" && transitioning == false)
        {
           // Debug.Log("Transitioning");
            transitioning = true;
            _startingSceneTransition.SetActive(true);

            //DisableStartingSceneTransition();
            StartCoroutine(WaitSec());
            _EndingSceneTransition.SetActive(false);
        }


    }

    public void playGame(string SceneNum)
    {
        _EndingSceneTransition.SetActive(true);
        StartCoroutine(WaitSec2(SceneNum));
    }

}
