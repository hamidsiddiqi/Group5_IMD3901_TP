using TMPro;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class TImer : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    public float time =10;

    public GameObject effect;
    public bool isActive;
    public CirlceTimer circTime;

    public void StartTimer(float time)
    {
        isActive = true;
        effect.SetActive(true);
        circTime.ActivateCountdown(time);
        // effect.transform.Find("RadialProgressBar").GetComponent<CircleImage>()ActivateCountdown(time);

        StartCoroutine(EndEffect(time));
    }

    IEnumerator EndEffect(float time)
    {
        yield return new WaitForSeconds(time);
        isActive = false;
        effect.SetActive(false);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText.SetText(time.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            timerText.SetText("Time's up!");
        }
        timerText.SetText(MathF.Ceiling(time).ToString());
    }

    public void TimerStart()
    {
        
    }
}
