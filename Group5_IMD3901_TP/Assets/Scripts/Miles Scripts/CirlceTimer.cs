using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CirlceTimer : MonoBehaviour
{

    private bool isActive = false;
    private float indicatorTimer;
    private float maxIndicatorTimer;
    public Image radialProgressBar;

    public float time; 

    private void Awake()
    {
       
    }

    public void ActivateCountdown(float countdownTime)
    {
        isActive = true; 
        maxIndicatorTimer = countdownTime;
        indicatorTimer = maxIndicatorTimer;
    }

    public void StopCountDown()
    {
        isActive = false; 
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Ver Four" || SceneManager.GetActiveScene().name == "Level 1" || SceneManager.GetActiveScene().name == "Level 2" || SceneManager.GetActiveScene().name == "Level 3")
        {
            ActivateCountdown(time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
           indicatorTimer -= Time.deltaTime;
            radialProgressBar.fillAmount = (indicatorTimer / maxIndicatorTimer);

            if (indicatorTimer <= 0)
            {
                StopCountDown();
            }
        }
    }
}
