using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text timeText;

    public float timeBetweenResets;

    private void Start()
    {
        this.slider.maxValue = timeBetweenResets;
        this.slider.value = timeBetweenResets;
    }

    // Update is called once per frame
    void Update()
    {
        this.slider.value -= Time.deltaTime;

        if(this.slider.value <= 0)
        {
            this.ResetSlider();
        }

        this.DisplayText();
    }

    void ResetSlider()
    {
        this.slider.value = timeBetweenResets;
    }

    void DisplayText()
    {
        float minutes = Mathf.FloorToInt(this.slider.value / 60);
        float seconds = Mathf.FloorToInt(this.slider.value % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
